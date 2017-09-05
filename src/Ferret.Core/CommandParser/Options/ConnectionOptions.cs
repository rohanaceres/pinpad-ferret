using CommandLine;

namespace Ferret.Core.CommandParser.Options
{
    /// <summary>
    /// Set of options to connect to one or more pinpads.
    /// </summary>
    internal sealed class ConnectionOptions : AbstractOption
    {
        /// <summary>
        /// Option to specify that all COM ports of the machine will be searched.
        /// </summary>
        [Option("all", Required = false, DefaultValue = false)]
        public bool ConnectToAll { get; set; }
    }
}