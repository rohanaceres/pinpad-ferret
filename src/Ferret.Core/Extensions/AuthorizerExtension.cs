using Ferret.Core.Ksn;
using MarkdownLog;
using Microtef.Core;
using Pinpad.Sdk.Model.TypeCode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferret.Core
{
    /// <summary>
    /// Stuff easely do with <see cref="ICardPaymentAuthorizer"/>.
    /// </summary>
    internal static class AuthorizerExtension
    {
        /// <summary>
        /// Show a brief description to each pinpad.
        /// </summary>
        /// <param name="pinpads">Pinpads to log on console.</param>
        public static void ShowSummaryOnConsole(this ICollection<ICardPaymentAuthorizer> pinpads)
        {
            Console.WriteLine(
                   pinpads.Select(s => new
                   {
                       PortName = s.PinpadFacade.Communication.ConnectionName,
                       Manufacturer = s.PinpadFacade.Infos.ManufacturerName.Replace(" ", ""),
                       SerialNumber = s.PinpadFacade.Infos.SerialNumber.Replace(" ", "")
                   })
                .ToMarkdownTable());
        }
        /// <summary>
        /// Log acquirers and KSN information on console.
        /// </summary>
        /// <param name="pinpads">Pinpads to log on console.</param>
        /// <param name="acquirers">KSN information obtained from ABECS GDU command.</param>
        public static void ShowKsns(this ICollection<ICardPaymentAuthorizer> pinpads,
            List<Acquirer> acquirers)
        {
            if (acquirers == null || acquirers.Count <= 0)
            {
                Console.WriteLine("Acquirers not found!");
            }
            else
            {
                Console.WriteLine(
                        acquirers.Select(a => new
                        {
                            SerialNumber = a.PinpadSerialNumber,
                            Index = a.Id,
                            AcquirerName = a.Name,
                            KsnDES = a.Ksns[CryptographyMode.DataEncryptionStandard],
                            Ksn3DES = a.Ksns[CryptographyMode.TripleDataEncryptionStandard]
                        })
                    .ToMarkdownTable());
            }
        }
        public static void ShowKsn(this ICollection<ICardPaymentAuthorizer> pinpads,
            Acquirer acquirer)
        {
            List<Acquirer> acquirerList = new List<Acquirer>();
            acquirerList.Add(acquirer);

            pinpads.ShowKsns(acquirerList);
        }
    }
}
