using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Options
{
    internal sealed class ConnectionOptions : IOptions
    {
        [Option('c', "connect", Required = true, HelpText = "Command to connect to the pinpad.")]
        public bool Connect { get; set; }
        [Option("all", Required = false)]
        public bool ConnectToAll { get; set; }
    }
}