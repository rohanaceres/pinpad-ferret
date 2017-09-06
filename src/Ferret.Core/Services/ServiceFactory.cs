using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ferret.Core.Services
{
    /// <summary>
    /// Responsible to return the right instance of <see cref="IPinpadService"/>,
    /// accordingly to the arguments received.
    /// </summary>
    static public class ServiceFactory
    {
        /// <summary>
        /// Argument options. To add a new command to this property, just create
        /// a new class that implements <see cref="IPinpadService"/>.
        /// </summary>
        static private List<IPinpadService> AvailableServices { get; set; }

        /// <summary>
        /// Get's all available commands.
        /// </summary>
        static ServiceFactory()
        {
            // Selects all classes that inherit from IPinpadService and are not abstract:
            ServiceFactory.AvailableServices = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                             .Contains(typeof(IPinpadService)))
                .Where(t => t.GetTypeInfo().IsAbstract == false)
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
        static public IPinpadService Create(string[] args)
        {
            IPinpadService service = ServiceFactory.AvailableServices
                .Where(s =>
                {
                    return s.IsServiceFromCommandLineArgs(args) == true;
                })
                .FirstOrDefault();

            return service;
        }
    }
}
