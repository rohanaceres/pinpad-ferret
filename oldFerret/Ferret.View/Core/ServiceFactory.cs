using Ferret.View.Core.Base;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace Ferret.View.Core
{
    /// <summary>
    /// Responsible to return the right instance of <see cref="IPinpadService"/>,
    /// accordingly to the arguments received.
    /// </summary>
    internal sealed class ServiceFactory
    {
        /// <summary>
        /// Argument options. To add a new command to this property, just create
        /// a new class that implements <see cref="IPinpadService"/>.
        /// </summary>
        private static List<IPinpadService> AvailableOptions { get; set; }

        /// <summary>
        /// Get's all available commands.
        /// </summary>
        static ServiceFactory()
        {
            // Seleciona todas as classes que herdam de IPinpadService:
            ServiceFactory.AvailableOptions = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IPinpadService)))
                .Select(currentType => Activator.CreateInstance(currentType) as IPinpadService)
                .ToList();
        }

        /// <summary>
        /// Create a new instance of <see cref="IPinpadService"/> based on the
        /// arguments received. 
        /// </summary>
        /// <param name="args">Arguments read from command prompt.</param>
        /// <returns>Arguments corresponding service, or null if arguments are
        /// unknown.</returns>
        public static IPinpadService Create (string[] args)
        {
            IPinpadService service = ServiceFactory.AvailableOptions
                .Where(s => CommandLine.Parser.Default.ParseArguments(args, s.Options) == true)
                .FirstOrDefault();

            return service;
        }
    }
}
