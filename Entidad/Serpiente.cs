using SnakeGame.Entidad.Jugador;
using SnakeGame.Mapa;
using SnakeGame.Posicion;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Entidad
{
    public class Serpiente : AbsEntidad
    {
        private static readonly Dictionary<Direccion, Image> texturas = new Dictionary<Direccion, Image>()
        {
            { Direccion.Arriba, Resources.cabeza_arriba },
            { Direccion.Abajo, Resources.cabeza_abajo },
            { Direccion.Izquierda, Resources.cabeza_izquierda },
            { Direccion.Derecha, Resources.cabeza_derecha }
        };

        public override string Nombre => "Serpiente";
        
        private List<Cola> partes = new List<Cola>();

        public Serpiente(Mundo mundo) 
        {
            mundo.EventoAparicionEntidad += PostGeneracion;
        }

        public override void Moverse()
        {
            base.Moverse();

            // Algoritmo que lentamente mueve cada parte hacia esa dirección
            foreach (Cola cola in partes)
            {
                cola.Moverse();
                Thread.Sleep(10);
            }
        }

        public void MirarHacia(Direccion direccion)
        {
            this.Direccion = direccion;
            foreach (Cola cola in partes)
            {
                cola.Direccion = this.Direccion;
                cola.Moverse();
            }
        }

        private void PostGeneracion(AbsEntidad sender, EventArgs args)
        {
            // Comparar que sea la misma instancia de la Serpiente
            if (sender == this)
            {

                // Guardar la coordenada de la última cola
                Coordenada nuevaPos = (Coordenada)this.Posicion.Clone();

                // Generar cuerpo
                for (int i = 0; i < 2; i++)
                {
                    // Mover a la direccion contraria para mostrar que está por "detrás"
                    nuevaPos = nuevaPos.DireccionOpuestaDe(this.Direccion);

                    // Generar la cola de la serpiente
                    Cola cola = (Cola)this.Mundo.GenerarEntidad(TipoEntidad.Cola, nuevaPos, this.Direccion);

                    this.partes.Add(cola);
                }
            }
        }
    }
}