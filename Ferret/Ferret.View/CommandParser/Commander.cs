using Ferret.View.Core;
using Ferret.View.Core.Base;
using System;

namespace Ferret.View.CommandParser
{
    internal sealed class Commander
    {
        public void Execute(string[] args)
        {
            IPinpadService service = ServiceFactory.Create(args);

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
