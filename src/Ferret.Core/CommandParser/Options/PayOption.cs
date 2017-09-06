using CommandLine;

namespace Ferret.Core.CommandParser.Options
{
    internal sealed class PayOption : AbstractOption
    {
        [Option("showTransactions")]
        public bool ShowTransactions { get; set; }
        [Option("amount")]
        public double Amount { get; set; }
        [Option("itk")]
        public string InitiatorTransactionKey { get; set; }
    }
}
