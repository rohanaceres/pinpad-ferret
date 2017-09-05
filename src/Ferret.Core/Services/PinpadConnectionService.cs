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
    internal sealed class PinpadConnectionService : IPinpadService
    {
        public string CommandName => "connect";
        public const string StoneCode = "407709482";

        /// <summary>
        /// Define the set of arguments that should be filled to connect to one 
        /// or more pinpads.
        /// </summary>
        public AbstractOption Options { get; private set; } = new ConnectionOptions();

        /// <summary>
        /// Connects to one or more pinpads.
        /// Adds the pinpad(s) connected to the IoC.
        /// </summary>
        public void Execute()
        {
            ConnectionOptions connOptions = this.Options as ConnectionOptions;

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
                    authorizers = new Collection<ICardPaymentAuthorizer>();

                    authorizers.Add(DeviceProvider.ActivateAndGetOneOrFirst(PinpadConnectionService.StoneCode));
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

        public bool IsServiceFromCommandLineArgs(string[] args)
        {
            return args?[0] == this.CommandName;
        }
    }
}
