using SnakeGame.Frms;
using SnakeGame.Mapa;
using SnakeGame.Personalizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    internal static class Program
    {

        public static Juego Juego { get; private set; }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        internal static void Main()
        {
            Juego = new Juego()
            {
                SkinSeleccionada = Skins.SkinAzul,
                Mundo = new Mundo()
                {
                    Generador = Generadores.GenClasico
                }
            };

            TimeSpan tiempoPorTick = TimeSpan.FromMilliseconds(100);

            Thread hiloLogico = new Thread(() =>
            {
                do
                {
                    Juego.TickPrincipal();
                    Thread.Sleep(tiempoPorTick); // Esperar antes del siguiente tick
                } while (!Juego.HaTerminado());
            });
            hiloLogico.Priority = ThreadPriority.Highest;
            hiloLogico.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmJuego());
        }
    }
}
