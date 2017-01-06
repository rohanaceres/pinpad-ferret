using System;
using Ferret.View.CommandParser.Base;
using Ferret.View.Core.Base;
using Ferret.View.CommandParser.Options;
using MicroPos.Core;
using Pinpad.Sdk.Model;
using Ferret.View.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Castle.MicroKernel;

namespace Ferret.View.Core.Services
{
    internal sealed class PinpadScanService : IPinpadService
    {
        private ScanOptions scanOptions = new ScanOptions();
        public IOptions Options { get { return this.scanOptions; } } 

        // TODO: Implementar.
        public void Execute()
        {
            if (this.Validate() == false) { return; }

            ICollection<ICardPaymentAuthorizer> pinpadsToScan;

            if (this.TryGetPinpadsToScan(out pinpadsToScan) == false) { return; }

            foreach (ICardPaymentAuthorizer currentPinpad in pinpadsToScan)
            {
                this.ScanPinpad(currentPinpad);
            }

            pinpadsToScan.ShowKsns();
        }

        private bool TryGetPinpadsToScan(out ICollection<ICardPaymentAuthorizer> pinpadsToScan)
        {
            if (this.scanOptions.ScanAll == true)
            {
                pinpadsToScan = Ferret.Container.Resolve<ICollection<ICardPaymentAuthorizer>>();
            }
            else if (string.IsNullOrEmpty(this.scanOptions.PinpadToScanConnectionName) == false)
            {
                ICardPaymentAuthorizer pinpadToScan = Ferret.Container.Resolve<ICollection<ICardPaymentAuthorizer>>()
                    .Where(p => p.PinpadFacade.Communication.PortName == this.scanOptions.PinpadToScanConnectionName)
                    .FirstOrDefault();

                if (pinpadToScan == null)
                {
                    Console.WriteLine("Pinpad not found at port {0}.", this.scanOptions.PinpadToScanConnectionName);
                    pinpadsToScan = null;
                    return false;
                }

                pinpadsToScan = new Collection<ICardPaymentAuthorizer>();
                pinpadsToScan.Add(pinpadToScan);
            }
            else
            {
                Console.WriteLine("COM port name is missing.");
                pinpadsToScan = null;
                return false;
            }

            return true;
        }
        private void ScanPinpad (ICardPaymentAuthorizer pinpad)
        {
            Console.WriteLine("Scanning pinpad attached to {0}...", pinpad.PinpadFacade
                .Communication.PortName);

            for (int i = this.scanOptions.Ranges[0]; i < this.scanOptions.Ranges[1]; i++)
            {
                if (this.scanOptions.ShowProgress == true)
                {
                    pinpad.PinpadFacade.Display.ShowMessage("", i.ToString(),
                        DisplayPaddingType.Center);
                }
            }
        }
        private bool Validate()
        {
            if (this.scanOptions == null) { return false; }

            try
            {
                var availblePinpads = Ferret.Container
                    .Resolve<ICollection<ICardPaymentAuthorizer>>();
            }
            catch (ComponentNotFoundException)
            {
                Console.WriteLine("Connect to any pinpad first!");
                return false;
            }

            if (this.scanOptions.ScanAll == true)
            {
                this.scanOptions.Ranges = new int[] { 1, 99 };
            }
            else if (this.scanOptions.Ranges == null || this.scanOptions.Ranges.Length <= 0)
            {
                Console.WriteLine("Scanning range is missing.");
                return false;
            }

            return true;
        }
    }
}
