using NAudio.Wave;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    public static class ControladorSonido
    {

        public static async void Tocar(Sonido sonido, bool loop)
        {
            await Task.Run(() =>
            {
                RawSourceWaveStream importer = new RawSourceWaveStream(ObtenerArchivo(sonido), new WaveFormat());
                WaveOutEvent soundFx = new WaveOutEvent();

                soundFx.Init(importer);
                soundFx.Play();
                soundFx.PlaybackStopped += (sender, args) =>
                {
                    // Liberar recursos
                    soundFx.Dispose();
                    importer.Dispose();

                    if (loop)
                    {
                        Tocar(sonido, true);
                    }
                };
            });
        }

        private static System.IO.Stream ObtenerArchivo(Sonido sonido)
        {
            switch (sonido)
            {
                case Sonido.Music: return Resources.music;
                case Sonido.Unpause: return Resources.unpause;
                case Sonido.Pow: return Resources.pow;
                case Sonido.Food: return Resources.food;
                case Sonido.Move: return Resources.move;
                default:
                    return null;
            }
        }
    }

    public enum Sonido
    {
        Music,
        Unpause,
        Pause,
        Pow,
        Food,
        Move
    }
}
