using MicroPos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MarkdownLog;
using Ferret.View.KsnStuff;
using Pinpad.Sdk.Model.TypeCode;

namespace Ferret.View.Extensions
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
                       PortName = s.PinpadFacade.Communication.PortName,
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
}
