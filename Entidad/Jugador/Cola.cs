using SnakeGame.Posicion;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Entidad.Jugador
{
    public class Cola : AbsEntidad, IRenderizable<Image>
    {
        private static readonly Dictionary<Direccion, Image> texturas = new Dictionary<Direccion, Image>()
        {
            { Direccion.Arriba, Resources.cabeza_arriba },
            { Direccion.Abajo, Resources.cabeza_abajo },
            { Direccion.Izquierda, Resources.cabeza_izquierda },
            { Direccion.Derecha, Resources.cabeza_derecha }
        };

        public override string Nombre => "Cola";
        
        public Cola()
        {
            Colisionable = true;
        }

        public Image ObtenerImagen()
        {
            return texturas[this.Direccion];
        }
    }
}
