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
    public class Serpiente : EntidadViva, IRenderizable<Image>
    {
        private static readonly Dictionary<Direccion, Image> texturas = new Dictionary<Direccion, Image>()
        {
            { Direccion.Arriba, Resources.cabeza_arriba },
            { Direccion.Abajo, Resources.cabeza_abajo },
            { Direccion.Izquierda, Resources.cabeza_izquierda },
            { Direccion.Derecha, Resources.cabeza_derecha }
        };

        private static readonly Dictionary<Direccion, Image> texturasMuerto = new Dictionary<Direccion, Image>()
        {
            { Direccion.Arriba, Resources.cabeza_muerto_arriba },
            { Direccion.Abajo, Resources.cabeza_muerto },
            { Direccion.Izquierda, Resources.cabeza_muerto_izquierda },
            { Direccion.Derecha, Resources.cabeza_muerto_derecha }
        };

        public override string Nombre => "Serpiente";
        
        private List<Cola> partes = new List<Cola>();

        public Serpiente(Mundo mundo)
        {
            Colisionable = true;
            mundo.EventoAparicionEntidad += PostGeneracion;
        }

        public void Moverse()
        {
            // Algoritmo que lentamente mueve cada parte hacia esa dirección
            Coordenada ultimaPosicion = this.Posicion;
            foreach (Cola cola in partes)
            {
                Coordenada actual = cola.Posicion;
                cola.MoverCola(ultimaPosicion);
                ultimaPosicion = actual;
            }

            // Ahora se puede mover la cabeza
            this.Posicion = this.Posicion.MoverHacia(Direccion);
            NecesitaActualizar = true;
        }

        public void MirarHacia(Direccion direccion)
        {
            this.Direccion = direccion;
            foreach (Cola cola in partes)
            {
                cola.Direccion = this.Direccion;
            }
        }

        public override void Morir()
        {
            base.Morir();
            foreach (Cola cola in partes)
            {
                cola.Morir();
            }
        }

        private void PostGeneracion(AbsEntidad sender, EventArgs args)
        {
            // Comparar que sea la misma instancia de la Serpiente
            if (sender == this)
            {

                // Guardar la coordenada de la última cola
                Coordenada nuevaPos = (Coordenada)this.Posicion.Clone();
                Direccion atras = Direcciones.OpuestaDe(this.Direccion);

                // Generar cuerpo
                for (int i = 0; i < 10; i++)
                {
                    // Mover a la direccion contraria para mostrar que está por "detrás"
                    nuevaPos = nuevaPos.MoverHacia(atras);

                    // Generar la cola de la serpiente
                    Cola cola = (Cola)this.Mundo.GenerarEntidad(TipoEntidad.Cola, nuevaPos, this.Direccion);

                    this.partes.Add(cola);
                }
            }
        }

        public Image ObtenerImagen()
        {
            return Vivo ? texturas[this.Direccion] : texturasMuerto[this.Direccion];
        }
    }
}