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
using Ferret.View.KsnStuff;
using Pinpad.Sdk.Model.TypeCode;
using Ferret.View.KsnStuff.TypeCode;

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

            List<Acquirer> acquirers = new List<Acquirer>();

            foreach (ICardPaymentAuthorizer currentPinpad in pinpadsToScan)
            {
                acquirers.AddRange(this.GetAcquirersFromPinpad(currentPinpad));
            }

            if (string.IsNullOrEmpty(this.scanOptions.SpecificAcquirerName) == false)
            {
                AcquirerCode searchedAcquirer = this.scanOptions.SpecificAcquirerName
                    .ToAcquirerCode();

                if (searchedAcquirer != AcquirerCode.Undefined)
                {
                    pinpadsToScan.ShowKsn(acquirers
                        .Where(a => a.AcquirerCode == searchedAcquirer)
                        .FirstOrDefault());
                }
            }
            else
            {
                pinpadsToScan.ShowKsns(acquirers);
            }
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
        internal List<Acquirer> GetAcquirersFromPinpad (ICardPaymentAuthorizer pinpad)
        {
            Console.WriteLine("Scanning pinpad attached to {0}...", pinpad.PinpadFacade
                .Communication.PortName);

            List<Acquirer> acquirers = new List<Acquirer>();

            for (int i = this.scanOptions.Ranges[0]; i < this.scanOptions.Ranges[1]; i++)
            {
                if (this.scanOptions.ShowProgress == true)
                {
                    pinpad.PinpadFacade.Display.ShowMessage("", i.ToString(),
                        DisplayPaddingType.Center);
                }

                Acquirer newAcquirer = this.GetAcquirer(pinpad, i);

                if (newAcquirer != null)
                {
                    acquirers.Add(newAcquirer);
                }
            }
            
            return acquirers;
        }
        private Acquirer GetAcquirer (ICardPaymentAuthorizer pinpad, int index)
        {
            // Send GDU:
            string desKsn = pinpad.PinpadFacade.Infos.GetDukptSerialNumber(index, 
                CryptographyMode.DataEncryptionStandard);

            string tdesKsn = pinpad.PinpadFacade.Infos.GetDukptSerialNumber(index,
                CryptographyMode.TripleDataEncryptionStandard);
            
            // If there's no KSN in the index searched, that acquirer is not
            // available for the pinpad:
            if (string.IsNullOrEmpty(desKsn) == true && 
                string.IsNullOrEmpty(tdesKsn) == true)
            {
                return null;
            }

            // Create acquirer:
            Acquirer acquirer = new Acquirer();
            acquirer.AcquirerCode = (AcquirerCode)index;
            acquirer.Id = index;
            acquirer.Ksns = new Dictionary<CryptographyMode, string>();
            acquirer.PinpadSerialNumber = pinpad.PinpadFacade.Communication
                .PortName;

            // Add KSNs:
            acquirer.Ksns.Add(CryptographyMode.DataEncryptionStandard, desKsn);
            acquirer.Ksns.Add(CryptographyMode.TripleDataEncryptionStandard, tdesKsn);

            // TODO: Mapear dados.
            return acquirer;
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
