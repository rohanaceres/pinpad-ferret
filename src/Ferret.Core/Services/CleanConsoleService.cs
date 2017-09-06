using Ferret.Core.CommandParser.Options;
using System;

namespace Ferret.Core.Services
{
    // TODO: Doc
    internal sealed class CleanConsoleService : IPinpadService
    {
        public string CommandName => "clear";

        public AbstractOption Options => new GenericOption();

        public void Execute()
        {
            Console.Clear();
        }

        public bool IsServiceFromCommandLineArgs(string[] args)
        {
            return args?[0].ToUpper() == this.CommandName.ToUpper();
        }
    }
}
