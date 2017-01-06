using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Options
{
    internal sealed class ScanOptions : IOptions
    {
        [Option('s', "scan", Required = true, HelpText = "Command to scan encryption keys in pinpad memory.")]
        public bool Scan { get; set; }
        [Option("all", Required = false)]
        public bool ScanAll { get; set; }
        [Option('p', "port")]
        public string PinpadToScanConnectionName { get; set; }
        [OptionArray('r', "ranges", Required = false)]
        public int[] Ranges { get; set; }
        [Option('l', "log", Required = false)]
        public bool ShowProgress { get; set; }
    }
}
