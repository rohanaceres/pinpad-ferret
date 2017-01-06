using System;
using Ferret.View.CommandParser.Base;
using Ferret.View.Core.Base;
using Ferret.View.CommandParser.Options;

namespace Ferret.View.Core.Services
{
    internal sealed class PinpadScanService : IPinpadService
    {
        public IOptions Options { get; private set; } = new ScanOptions();

        // TODO: Implementar.
        public void Execute()
        {
            Console.WriteLine("Scanning pinpad...");
        }
    }
}
