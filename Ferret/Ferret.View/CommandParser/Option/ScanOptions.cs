using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Options
{
    /// <summary>
    /// Set of options to scan one or more pinpads searching KSN information.
    /// This is useful to verify whether an acquirer is supported by the pinpad
    /// ore not.
    /// </summary>
    internal sealed class ScanOptions : IOptions
    {
        // TODO: Consertar o lance do scan.
        /// <summary>
        /// Option to specify a scanning proccess.
        /// </summary>
        [Option('s', "scan", Required = true, HelpText = "Command to scan encryption keys in pinpad memory.")]
        public bool Scan { get; set; }
        /// <summary>
        /// Option to specify that all pinpads connected will be scanned.
        /// </summary>
        [Option("all", Required = false)]
        public bool ScanAll { get; set; }
        /// <summary>
        /// Option to specify the COM port of a unique pinpad.
        /// In this case, this will be the only pinpad to scan.
        /// </summary>
        [Option('p', "port")]
        public string PinpadToScanConnectionName { get; set; }
        /// <summary>
        /// Specify the table range (inside pinpad memory) to scan.
        /// </summary>
        [OptionArray('r', "ranges", Required = false)]
        public int[] Ranges { get; set; }
        /// <summary>
        /// If scanning progress should be shown on pinpad screen.
        /// It will delay the process.
        /// </summary>
        [Option('l', "log", Required = false)]
        public bool ShowProgress { get; set; }
    }
}
