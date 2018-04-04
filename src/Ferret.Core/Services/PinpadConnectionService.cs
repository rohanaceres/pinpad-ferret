using Castle.MicroKernel.Registration;
using Ferret.Core.CommandParser.Options;
using Ferret.Core.DependencyInjection;
using Microtef.Core;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ferret.Core.Services
{
    /// <summary>
    /// Connects to one or more pinpads attached to the machine.
    /// </summary>
    internal sealed class PinpadConnectionService : CoreService<ConnectionOption>, IPinpadService
    {
        public const string StoneCode = "407709482";

        public override string CommandName => "connect";

        /// <summary>
        /// Connects to one or more pinpads.
        /// Adds the pinpad(s) connected to the IoC.
        /// </summary>
        public override void ConcreteExecute()
        {
            ConnectionOption connOptions = this.Options as ConnectionOption;

            if (connOptions == null) { return; }

            try
            {
                ICollection<ICardPaymentAuthorizer> authorizers;

                if (connOptions.ConnectToAll == true)
                {
                    authorizers = DeviceProvider.ActivateAndGetAll(PinpadConnectionService.StoneCode);
                }
                else
                {
                    authorizers = new Collection<ICardPaymentAuthorizer>
                    {
                        DeviceProvider.ActivateAndGetOneOrFirst(
                            PinpadConnectionService.StoneCode,
                            new DisplayableMessages
                            {
                                MainLabel = "dont be in a box",
                                CreditLabel = "credit",
                                DebitLabel = "debit",
                                ApprovedMessage = "approved :-)",
                                DeclinedMessage = "declined :-(",
                                ProcessingMessage = "wait...",
                                TransactionTypeMenuLabel = "trnx type",
                                InitializationMessage = "wait..."
                            })
                    };
                }

                IoC.Container.Register(
                    Component.For<ICollection<ICardPaymentAuthorizer>>()
                             .Instance(authorizers)
                );

                authorizers.ShowSummaryOnConsole();
            }
            catch (PinpadNotFoundException)
            {
                Console.WriteLine("None pinpad found.");
            }
        }
    }
}
