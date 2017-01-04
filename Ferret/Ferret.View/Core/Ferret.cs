using System;

namespace Ferret.View
{
    internal sealed class Ferret
    {
        public static void DoIt ()
        {
            bool finish = false;

            do
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) == true) { continue; }

                string []args = input.Split(' ');

                // TODO: Implementar Strategy para executar um comando de acordo com
                // Os parâmetros lidos.
            }
            while (finish == false);
        }
    }
}
