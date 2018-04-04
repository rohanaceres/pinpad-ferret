using Ferret.Core.CommandParser.Options;
using Ferret.Core.DependencyInjection;
using Microtef.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferret.Core.Services
{
    internal sealed class ExitService : CoreService<GenericOption>, IPinpadService
    {
        public override string CommandName => "exit";

        public override void ConcreteExecute()
        {
            List<ICardPaymentAuthorizer> pinpadsToScan = IoC.Container.Resolve<ICollection<ICardPaymentAuthorizer>>()
                .ToList();

            pinpadsToScan.ForEach(p =>
                p.PinpadFacade.Communication.ClosePinpadConnection(p.PinpadMessages.MainLabel)
            );

            Environment.Exit(0);
        }
    }
}
