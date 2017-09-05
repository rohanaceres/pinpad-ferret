using CommandLine;
using Ferret.View.CommandParser.Base;

namespace Ferret.View.CommandParser.Option
{
    /// <summary>
    /// Set of options to clear the console.
    /// </summary>
    internal sealed class ClearOptions : IOptions
    {
        /// <summary>
        /// Option to specify that the console should be cleaned.
        /// </summary>
        [Option("clean", Required = true)]
        public bool ClearConsole { get; set; }
    }
}
