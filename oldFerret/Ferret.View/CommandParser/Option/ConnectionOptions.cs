using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Options
{
    /// <summary>
    /// Set of options to connect to one or more pinpads.
    /// </summary>
    internal sealed class ConnectionOptions : IOptions
    {
        /// <summary>
        /// Option to specify that a connection will be performed.
        /// </summary>
        [Option('c', "connect", Required = true, HelpText = "Command to connect to the pinpad.")]
        public bool Connect { get; set; }
        /// <summary>
        /// Option to specify that all COM ports of the machine will be searched.
        /// </summary>
        [Option("all", Required = false, DefaultValue = false)]
        public bool ConnectToAll { get; set; }
    }
}