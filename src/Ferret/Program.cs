using Ferret.Core.Services;
using Microtef.Platform.Desktop;
using System;
using System.Globalization;
using System.Threading;

namespace Ferret
{
    class Program
    {
        static void Main(string[] args)
        {
            DesktopInitializer.Initialize();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DoIt();
        }

        /// <summary>
        /// Begin to read commands.
        /// </summary>
        public static void DoIt()
        {
            bool finish = false;

            do
            {
                // Lê um comando do teclado:
                Console.Write("$ ferret ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) == true) { continue; }

                // Separar a linha lida em vários argumentos:
                string[] args = input.Split(' ');

                // Executa uma ação de acordo com os argumentos lidos da linha de comando:
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
            while (finish == false);
        }
    }
}
