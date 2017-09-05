using Castle.Windsor;
using Ferret.View.CommandParser;
using MicroPos.Platform.Desktop;
using System;

namespace Ferret.View
{
    /// <summary>
    /// Responsible for read commands and act accordingly.
    /// </summary>
    internal sealed class Ferret
    {
        /// <summary>
        /// Responsible for chose the right strategy to a specific command.
        /// </summary>
        public static Commander Commander { get; set; } = new Commander();
        /// <summary>
        /// Inversion of Control and Dependency Injection container.
        /// </summary>
        public static IWindsorContainer Container { get; set; } = new WindsorContainer();

        /// <summary>
        /// Initialize desktop assets to Microtef SDK.
        /// </summary>
        static Ferret()
        {
            DesktopInitializer.Initialize();
        }

        /// <summary>
        /// Begin to read commands.
        /// </summary>
        public static void DoIt ()
        {
            bool finish = false;

            do
            {
                // Lê um comando do teclado:
                Console.Write("$ ferret ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) == true) { continue; }

                // Separar a linha lida em vários argumentos:
                string []args = input.Split(' ');

                // Executa uma ação de acordo com os argumentos lidos da linha de comando:
                Ferret.Commander.Execute(args);
            }
            while (finish == false);
        }
    }
}
