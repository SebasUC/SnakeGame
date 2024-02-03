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
                // Movimiento de la serpiente
                this.serpiente.Moverse();
                this.ValidarColisiones();
            }

            if (AlHacerTick != null)
            {
                AlHacerTick();
            }
        }

        private void ValidarColisiones()
        {
            Casilla casillaActual = this.Mundo.ConsultarPosicion(this.serpiente.Posicion);
            if (!casillaActual.PermiteColisiones || Mundo.ConsultarEntidadesEn(casillaActual).Any((entidad) => entidad.Colisionable))
            {
                // Matar la serpiente
                this.MatarSerpiente();
            }
        }

        private void MatarSerpiente()
        {
            throw new NotImplementedException();
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

            this.serpiente.MirarHacia(direccion);
        }
    }
}
