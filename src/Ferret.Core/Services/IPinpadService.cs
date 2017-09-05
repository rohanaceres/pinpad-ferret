using Ferret.Core.CommandParser.Options;

namespace Ferret.Core.Services
{
    /// <summary>
    /// Encapsulates an action to be called when a specific set of arguments is
    /// received.
    /// </summary>
    public interface IPinpadService
    {
        /// <summary>
        /// Command name.
        /// </summary>
        string CommandName { get; }
        /// <summary>
        /// Define the set of arguments that should be filled to perform the 
        /// <see cref="Execute"/> method.
        /// </summary>
        AbstractOption Options { get; }
        /// <summary>
        /// Action to be called when the set of arguments received match 
        /// <see cref="Options"/> specifications.
        /// </summary>
        void Execute();
        /// <summary>
        /// Verifies if the command line arguments match the service.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Whether the service matches the args received.</returns>
        bool IsServiceFromCommandLineArgs(string[] args);
    }
}
