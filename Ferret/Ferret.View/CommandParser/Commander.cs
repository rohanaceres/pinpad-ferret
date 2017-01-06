using Ferret.View.CommandParser.Base;
using Ferret.View.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ferret.View.CommandParser
{
    internal sealed class Commander : ICommand
    {
        public List<IPinpadService> AvailableOptions { get; private set; }

        public Commander()
        {
            // Seleciona todas as classes que herdam de IPinpadService:
            this.AvailableOptions = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IPinpadService)))
                .Select(currentType => Activator.CreateInstance(currentType) as IPinpadService)
                .ToList();
        }

        public void Execute(string[] args)
        {
            IPinpadService service = this.AvailableOptions
                .Where(s => CommandLine.Parser.Default.ParseArguments(args, s.Options) == true)
                .FirstOrDefault();

            if (service != null)
            {
                service.Execute();
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
    }
}
