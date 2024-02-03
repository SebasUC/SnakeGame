using SnakeGame.Entidad;
using SnakeGame.Posicion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Mapa
{
    public class Casilla
    {
        public Color Color { get; set; }
        public bool PermiteColisiones { get; set; }
        public Coordenada Posicion { get; set; }

        private AbsEntidad entidadPresente;

        public Casilla(Color color, Coordenada pos, bool permiteColisiones = true)
        {
            this.Color = color;
            this.Posicion = pos;
            this.PermiteColisiones = permiteColisiones;
        }
    }
}
