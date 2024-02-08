using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Perfil
    {

        private static Perfil _instance;
        public static Perfil Instance { get {  return _instance == null ? (_instance = new Perfil()) : _instance; } }

        public string Nombre { get; set; } = "Jugador";
        public int PuntuacionMaxima { get; set; }

        private Perfil() { }
    }
}
