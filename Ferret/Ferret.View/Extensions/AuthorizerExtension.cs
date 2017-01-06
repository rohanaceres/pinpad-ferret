using MicroPos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MarkdownLog;
using Ferret.View.KsnStuff;

namespace Ferret.View.Extensions
{
    internal static class AuthorizerExtension
    {
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
        public static void ShowKsns(this ICollection<ICardPaymentAuthorizer> pinpads,
            List<Acquirer> acquirers)
        {
            // TODO: Mostrar dados do KSN.
            Console.WriteLine(
                   pinpads.Select(s => new
                   {
                       PortName = s.PinpadFacade.Communication.PortName,
                       SerialNumber = s.PinpadFacade.Infos.SerialNumber.Replace(" ", "")
                   })
                   .ToMarkdownTable());
        }
    }
}
