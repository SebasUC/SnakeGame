using SnakeGame.Personalizacion;
using SnakeGame.Posicion;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SnakeGame.Personalizacion.Skins;

namespace SnakeGame.Entidad.Jugador
{
    public class Cola : EntidadViva, IRenderizable<Image>
    {
        public override string Nombre => "Cola";
        public Skin Skin { get; set; }

        public Cola()
        {
        }

        public Image ObtenerImagen()
        {
            return Vivo ? Skin.Cuerpo : Skin.CuerpoMuerto;
        }

        internal void MoverCola(Coordenada coordenada)
        {
            this.Posicion = coordenada;
            this.NecesitaActualizar = true;
        }
    }
}
