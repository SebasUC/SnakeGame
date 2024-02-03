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
        static async Task Main()
        {

            TimeSpan tiempoPorTick = TimeSpan.FromMilliseconds(100); // 10 ticks por segundo

            CancellationTokenSource origenToken = new CancellationTokenSource();

            CancellationToken token = origenToken.Token;

            Thread hiloLogico = new Thread(() =>
            {
                bool b = false;
                do
                {
                    Juego.TickPrincipal();
                    Thread.Sleep(tiempoPorTick); // Esperar antes del siguiente tick
                    if (!b)
                    {
                        Juego.ComenzarPartida();
                        b = true;
                    }
                } while (!Juego.Terminado);
            });
            hiloLogico.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmJuego());
        }
    }
}
