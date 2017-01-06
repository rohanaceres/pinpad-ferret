using Ferret.View.CommandParser.Base;

namespace Ferret.View.Core.Base
{
    /// <summary>
    /// Encapsulates an action to be called when a specific set of arguments is
    /// received.
    /// </summary>
    internal interface IPinpadService
    {
        /// <summary>
        /// Define the set of arguments that should be filled to perform the 
        /// <see cref="Execute"/> method.
        /// </summary>
        IOptions Options { get; }
        /// <summary>
        /// Action to be called when the set of arguments received match 
        /// <see cref="Options"/> specifications.
        /// </summary>
        void Execute();
    }
}
