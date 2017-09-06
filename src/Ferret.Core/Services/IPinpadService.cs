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
        /// Action to be called when the set of arguments received match 
        /// <see cref="Options"/> specifications.
        /// </summary>
        /// <returns>Whether the execution was successful.</returns>
        bool Execute(string [] args);
        /// <summary>
        /// Verifies if the command line arguments match the service.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Whether the service matches the args received.</returns>
        bool IsServiceFromCommandLineArgs(string[] args);
    }
}
