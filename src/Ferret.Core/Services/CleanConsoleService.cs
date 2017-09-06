using Ferret.Core.CommandParser.Options;
using System;

namespace Ferret.Core.Services
{
    // TODO: Doc
    internal sealed class CleanConsoleService : CoreService<GenericOption>, IPinpadService
    {
        public override string CommandName => "clear";

        public override void ConcreteExecute()
        {
            Console.Clear();
        }
    }
}
