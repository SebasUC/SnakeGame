using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Entidad;
using SnakeGame.Mapa;
using SnakeGame.Posicion;

namespace SnakeGame
{
    public class Juego
    {
        public delegate void EventoTickHandler();
        public event EventoTickHandler AlHacerTick;

        public bool Terminado;
        public Mundo Mundo = new Mundo();
        public Serpiente serpiente { get; set; }

        public Juego() {
            this.Terminado = false;
        }

        public void TickPrincipal()
        {
            if (this.serpiente != null && this.serpiente.Valida)
            {
                if (this.serpiente.Vivo)
                {
                    // Movimiento de la serpiente
                    this.serpiente.Moverse();
                    this.ValidarColisiones();
                }
            }

            if (AlHacerTick != null)
            {
                AlHacerTick();
            }
        }

        private void ValidarColisiones()
        {
            Coordenada siguientePosicion = this.serpiente.Posicion.MoverHacia(this.serpiente.Direccion);

            if (!this.Mundo.EstaDentroDelMapa(siguientePosicion.X, siguientePosicion.Y)) return;

            Casilla siguienteCasilla = this.Mundo.ConsultarPosicion(siguientePosicion);
            if (!siguienteCasilla.PermiteColisiones || Mundo.ConsultarEntidadesEn(siguienteCasilla).Any((entidad) => !entidad.Colisionable))
            {
                // Matar la serpiente
                this.MatarSerpiente();
            }
        }

        private void MatarSerpiente()
        {
            this.serpiente.Morir();
        }

        public void ComenzarPartida()
        {
            Console.WriteLine("Comenzando partida...");

            this.serpiente = (Serpiente) Mundo.GenerarEntidad(TipoEntidad.Serpiente, new Coordenada(5, 11), Direccion.Derecha);
            
            Console.WriteLine("Partida comenzada.");
        }

        public void AlPresionarTecla(Keys tecla)
        {
            Direccion direccion = Direccion.Arriba;
            switch (tecla)
            {
                case Keys.W:
                case Keys.Up:
                    direccion = Direccion.Arriba;
                    break;
                case Keys.S:
                case Keys.Down:
                    direccion = Direccion.Abajo;
                    break;
                case Keys.A:
                case Keys.Left:
                    direccion = Direccion.Izquierda;
                    break;
                case Keys.D:
                case Keys.Right:
                    direccion = Direccion.Derecha;
                    break;
            }

            if (direccion != Direcciones.OpuestaDe(this.serpiente.Direccion) && direccion != this.serpiente.Direccion)
            {
                // Mover solo si se mueve hacia una dirección que no sea contraria o la misma que la actual
                this.serpiente.MirarHacia(direccion);
            }
        }
    }
}
