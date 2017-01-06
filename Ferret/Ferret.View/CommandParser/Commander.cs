using Ferret.View.Core;
using Ferret.View.Core.Base;
using System;

namespace Ferret.View.CommandParser
{
    /// <summary>
    /// Responsible for chose the right strategy to a specific command.
    /// </summary>
    internal sealed class Commander
    {
        /// <summary>
        /// It performs the rigth action based on the arguments received from
        /// the command prompt.
        /// </summary>
        /// <param name="args">Arguments from command prompt.</param>
        public void Execute(string[] args)
        {
            IPinpadService service = ServiceFactory.Create(args);

            if (service != null)
            {
                service.Execute();
            }
            else
            {
                // TODO: Adicionar arquivo help.
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
