using Ferret.View.CommandParser;
using Ferret.View.CommandParser.Base;
using System;

namespace Ferret.View
{
    internal sealed class Ferret
    {
        public static ICommand Commander { get; set; } = new Commander();

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
