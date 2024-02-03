using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal static class Utils
    {
        public static int TamañoCasilla = 25;
        public static int Escalar(int numero)
        {
            return numero * TamañoCasilla; // Escalar número a la resolución del programa
        }
    }
}
