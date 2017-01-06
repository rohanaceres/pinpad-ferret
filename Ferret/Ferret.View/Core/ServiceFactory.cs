using Ferret.View.Core.Base;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace Ferret.View.Core
{
    internal sealed class ServiceFactory
    {
        private static List<IPinpadService> AvailableOptions { get; set; }

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

        public static IPinpadService Create (string[] args)
        {
            IPinpadService service = ServiceFactory.AvailableOptions
                .Where(s => CommandLine.Parser.Default.ParseArguments(args, s.Options) == true)
                .FirstOrDefault();

            return service;
        }
    }
}
