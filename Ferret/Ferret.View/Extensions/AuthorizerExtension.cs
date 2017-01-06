using MicroPos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MarkdownLog;

namespace Ferret.View.Extensions
{
    internal static class AuthorizerExtension
    {
        public static void ShowOnConsole(this ICollection<ICardPaymentAuthorizer> pinpads)
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
    }
}
