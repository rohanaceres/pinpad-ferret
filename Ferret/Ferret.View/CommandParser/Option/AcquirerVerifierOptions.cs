using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Option
{
    /// <summary>
    /// Verify if a specific aquirer exist on pinpad memory.
    /// </summary>
    internal sealed class AcquirerVerifierOptions : IOptions
    {
        /// <summary>
        /// Name of the acquirer to search.
        /// </summary>
        [Option("hasAcquirer", Required = true)]
        public string HasAcquirer { get; set; }
    }
}
