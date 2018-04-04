using Castle.MicroKernel;
using Ferret.Core.CommandParser.Options;
using Ferret.Core.DependencyInjection;
using Ferret.Core.Ksn;
using Microtef.Core;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ferret.Core.Services
{
    // TODO: Doc
    internal sealed class PinpadScanService : CoreService<ScanOption>, IPinpadService
    {
        public override string CommandName => "scan";

        public override void ConcreteExecute()
        {
            if (this.Validate() == false) { return; }

            if (this.TryGetPinpadsToScan(out ICollection<ICardPaymentAuthorizer> pinpadsToScan) == false) { return; }

            List<Acquirer> acquirers = new List<Acquirer>();

            foreach (ICardPaymentAuthorizer currentPinpad in pinpadsToScan)
            {
                acquirers.AddRange(this.GetAcquirersFromPinpad(currentPinpad));
            }

            if (string.IsNullOrEmpty(this.Options.SpecificAcquirerName) == false)
            {
                AcquirerCode searchedAcquirer = this.Options.SpecificAcquirerName
                    .ToAcquirerCode();

                if (searchedAcquirer != AcquirerCode.Undefined)
                {
                    Console.WriteLine($"{this.Options.SpecificAcquirerName.ToUpper()} is supported!");

                    pinpadsToScan.ShowKsn(acquirers
                        .Where(a => a.AcquirerCode == searchedAcquirer)
                        .FirstOrDefault());
                }
                else
                {
                    Console.WriteLine($"{this.Options.SpecificAcquirerName} is NOT supported by this pinpad.");
                }
            }
            else
            {
                pinpadsToScan.ShowKsns(acquirers);
            }
        }

        internal List<Acquirer> GetAcquirersFromPinpad (ICardPaymentAuthorizer pinpad)
        {
            Console.WriteLine($"Scanning pinpad attached to {pinpad.PinpadFacade.Communication.ConnectionName}...");

            List<Acquirer> acquirers = new List<Acquirer>();

            pinpad.PinpadFacade.Display.ShowMessage(pinpad.PinpadMessages.ProcessingMessage);

            for (int i = this.Options.Ranges[0]; i < this.Options.Ranges[1]; i++)
            {
                if (this.Options.ShowProgress == true)
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

            pinpad.PinpadFacade.Display.ShowMessage(pinpad.PinpadMessages.MainLabel);

            return acquirers;
        }

        private bool TryGetPinpadsToScan(out ICollection<ICardPaymentAuthorizer> pinpadsToScan)
        {
            if (this.Options.ScanAll == true)
            {
                pinpadsToScan = IoC.Container.Resolve<ICollection<ICardPaymentAuthorizer>>();
            }
            else if (string.IsNullOrEmpty(this.Options.PinpadToScanConnectionName) == false)
            {
                ICardPaymentAuthorizer pinpadToScan = IoC.Container.Resolve<ICollection<ICardPaymentAuthorizer>>()
                    .Where(p => p.PinpadFacade.Communication.ConnectionName == this.Options.PinpadToScanConnectionName)
                    .FirstOrDefault();

                if (pinpadToScan == null)
                {
                    Console.WriteLine($"Pinpad not found at port {this.Options.PinpadToScanConnectionName}.");
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
                .ConnectionName;

            // Add KSNs:
            acquirer.Ksns.Add(CryptographyMode.DataEncryptionStandard, desKsn);
            acquirer.Ksns.Add(CryptographyMode.TripleDataEncryptionStandard, tdesKsn);

            // TODO: Mapear dados.
            return acquirer;
        }

        private bool Validate()
        {
            if (this.Options == null) { return false; }

            try
            {
                var availblePinpads = IoC.Container.Resolve<ICollection<ICardPaymentAuthorizer>>();
            }
            catch (ComponentNotFoundException)
            {
                Console.WriteLine("Connect to any pinpad first!");
                return false;
            }

            if (this.Options.ScanAll == true)
            {
                this.Options.Ranges = new int[] { 1, 99 };
            }
            else if (this.Options.Ranges == null || this.Options.Ranges.Length <= 0)
            {
                Console.WriteLine("Scanning range is missing.");
                return false;
            }

            return true;
        }
    }
}
