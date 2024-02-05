using SnakeGame.Frms;
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

        public static Juego Juego { get; } = new Juego();


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            TimeSpan tiempoPorTick = TimeSpan.FromMilliseconds(150);

            Thread hiloLogico = new Thread(() =>
            {
                bool b = true;
                do
                {
                    Juego.TickPrincipal();
                    Thread.Sleep(tiempoPorTick); // Esperar antes del siguiente tick
                    if (!b)
                    {
                        Juego.ComenzarPartida();
                        b = true;
                    }
                } while (!Juego.HaTerminado());
            });
            hiloLogico.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmJuego());
        }
    }
}
