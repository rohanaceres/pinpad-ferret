using Ferret.View.CommandParser.Base;

namespace Ferret.View.Core.Base
{
    internal interface IPinpadService
    {
        IOptions Options { get; }
        void Execute();
    }
}
