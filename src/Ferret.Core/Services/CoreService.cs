using System;
using Ferret.Core.CommandParser.Options;

namespace Ferret.Core.Services
{
    // TODO: Doc
    public abstract class CoreService<TOption> : IPinpadService
        where TOption : new()
    {
        abstract public string CommandName { get; }
        /// <summary>
        /// Define the set of arguments that should be filled to perform the 
        /// <see cref="Execute"/> method.
        /// </summary>
        public TOption Options { get; protected set; }
            = new TOption();

        public bool Execute(string [] args)
        {
            try
            {
                if (CommandLine.Parser.Default.ParseArguments(args, this.Options) == false)
                {
                    Console.WriteLine("Invalid arguments.");
                    return false;
                }

                this.ConcreteExecute();
            }
            catch (Exception)
            {
                Console.WriteLine("An unexpected error occurred.");
            }
            finally
            {
                this.Options = new TOption();
            }

            return true;
        }
        abstract public void ConcreteExecute();

        public bool IsServiceFromCommandLineArgs(string[] args)
        {
            return args?[0].ToUpper() == this.CommandName.ToUpper();
        }
    }
}
