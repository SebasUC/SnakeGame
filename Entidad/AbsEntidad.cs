using SnakeGame.Mapa;
using SnakeGame.Posicion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Entidad
{
    public abstract class AbsEntidad
    {
        public int Id { get; set; }
        public abstract string Nombre { get; }
        public Mundo Mundo { get; set; }
        public Coordenada Posicion;
        public Direccion Direccion;
        
        public bool NecesitaActualizar = true;
        public bool Valida = false; // Si es false la entidad desaparece
        public bool Colisionable = false;

        public virtual void Moverse()
        {
            this.Posicion = this.Posicion.MoverHacia(Direccion);
            NecesitaActualizar = true;
        }
    }
}
