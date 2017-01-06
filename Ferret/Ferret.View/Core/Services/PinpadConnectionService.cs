using Castle.MicroKernel.Registration;
using Ferret.View.CommandParser.Base;
using Ferret.View.CommandParser.Options;
using Ferret.View.Core.Base;
using MicroPos.Core;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ferret.View.Extensions;

namespace Ferret.View.Core.Services
{
    internal sealed class PinpadConnectionService : IPinpadService
    {
        public IOptions Options { get; private set; } = new ConnectionOptions();

        public void Execute()
        {
            ConnectionOptions connOptions = this.Options as ConnectionOptions;

            if (connOptions == null) { return; }

            try
            {
                ICollection<ICardPaymentAuthorizer> authorizers;

                if (connOptions.ConnectToAll == true)
                {
                    authorizers = DeviceProvider.GetAll(
                        "97721ADF94084FCDBE26AA28F01DD2F3", "https://tef.stone.com.br/",
                        "https://tms.stone.com.br");
                }
                else
                {
                    authorizers = new Collection<ICardPaymentAuthorizer>();

                    authorizers.Add(DeviceProvider.GetOneOrFirst(
                        "97721ADF94084FCDBE26AA28F01DD2F3", "https://tef.stone.com.br/",
                        "https://tms.stone.com.br"));
                }

                Ferret.Container.Register(
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
