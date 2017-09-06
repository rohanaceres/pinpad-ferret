using System;
using Ferret.Core.CommandParser.Options;

namespace Ferret.Core.Services
{
    internal sealed class ExitService : CoreService<GenericOption>, IPinpadService
    {
        public override string CommandName => "exit";

        public override void ConcreteExecute()
        {
            Environment.Exit(0);
        }
    }
}
