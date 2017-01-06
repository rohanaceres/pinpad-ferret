using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Option
{
    internal sealed class ClearOptions : IOptions
    {
        [Option("clean", Required = true)]
        public bool ClearConsole { get; set; }
    }
}
