using System;
using Ferret.View.CommandParser.Base;
using Ferret.View.Core.Base;
using Ferret.View.CommandParser.Option;

namespace Ferret.View.Core.Services
{
    internal sealed class AcquirerVerifier : IPinpadService
    {
        public IOptions Options { get; private set; } = new AcquirerVerifierOptions();

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
