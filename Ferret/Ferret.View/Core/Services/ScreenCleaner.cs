using System;
using Ferret.View.CommandParser.Base;
using Ferret.View.Core.Base;
using Ferret.View.CommandParser.Option;

namespace Ferret.View.Core.Services
{
    internal sealed class ScreenCleaner : IPinpadService
    {
        public IOptions Options { get; private set; } = new ClearOptions();

        public void Execute()
        {
            Console.Clear();
        }
    }
}
