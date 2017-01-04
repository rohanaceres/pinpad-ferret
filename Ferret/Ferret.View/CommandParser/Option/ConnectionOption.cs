using CommandLine;

namespace Ferret.View.CommandParser.Options
{
    internal sealed class ConnectionOptions
    {
        [Option(Commands.ConnectShortcut, Commands.Connect, Required = true, HelpText = "Command to connect to the pinpad.")]
        public bool Connect { get; set; }
        [Option(Commands.All, Required = false)]
        public bool ConnectToAll { get; set; }
    }
}
