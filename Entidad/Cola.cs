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
    public class Cola : EntidadViva, IRenderizable<Image>
    {
        public override string Nombre => "Cola";
        
        public Cola()
        {
        }

        public Image ObtenerImagen()
        {
            return Vivo ? Resources.cuerpo : Resources.cuerpo_muerto;
        }

        internal void MoverCola(Coordenada coordenada)
        {
            this.Posicion = coordenada;
            this.NecesitaActualizar = true;
        }
    }
}
