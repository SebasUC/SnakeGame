using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Entidad;
using SnakeGame.Mapa;
using SnakeGame.Personalizacion;
using SnakeGame.Posicion;

namespace SnakeGame
{
    public class Juego
    {
        private Direccion? proximaDireccion;
        private int ultimoTickCrecimiento;

        public delegate void EventoTickHandler();
        public event EventoTickHandler AlHacerTick;

        public delegate void EventoPuntuacionHandler();
        public event EventoPuntuacionHandler AlPuntuar;

        public delegate void EventoComenzarHandler();
        public event EventoComenzarHandler AlComenzar;

        public Estado EstadoActual;
        public Skins.Skin SkinSeleccionada { get; set; }
        public Mundo Mundo { get; set; }
        public bool LimitesActivos { get; set; } = true;

        public int TickActual { get; set; }
        public Serpiente Jugador { get; set; }
        public Fruta ComidaActual { get; set; }

        private int _puntuacion;
        public int Puntuacion
        {
            get
            {
                return _puntuacion;
            }
            set
            {
                _puntuacion = value;
                // Llamar al evento de puntuación
                if (AlPuntuar != null)
                {
                    AlPuntuar();
                }
            }
        }

        public Juego() {
            this.EstadoActual = Estado.Esperando;
        }

        public void TickPrincipal()
        {
            if (this.EstadoActual == Estado.Preparado)
            {
                // Aparecer arena y serpiente
                ComenzarPartida();
            } else if (this.EstadoActual == Estado.Iniciado && this.proximaDireccion != null)
            {
                // Permitir que el juego comience
                this.EstadoActual = Estado.Jugando;
            } else if (this.Jugador != null && this.Jugador.Valida)
            {
                if (this.Jugador.Vivo && this.EstadoActual == Estado.Jugando)
                {
                    if (this.proximaDireccion != null && proximaDireccion != this.Jugador.Direccion)
                    {
                        this.Jugador.MirarHacia((Direccion)this.proximaDireccion);
                        this.proximaDireccion = null;
                    }
                    this.Jugador.Moverse();
                    this.ValidarComida();

                    if (!this.ComidaActual.Valida && TickActual >= (ultimoTickCrecimiento + 2))
                    {
                        AparecerFruta();
                    }

                    TickActual++;
                }
            }

            if (AlHacerTick != null)
            {
                AlHacerTick();
            }
        }

        private void ValidarComida()
        {
            if (!this.Mundo.EstaDentroDelMapa(this.Jugador.Posicion.X, this.Jugador.Posicion.Y)) return;

            Casilla casilla = this.Mundo.ConsultarPosicion(this.Jugador.Posicion);

            // Filtrar las entidades para obtener solo las de tipo Fruta
            IEnumerable<Fruta> frutas = Mundo.ConsultarEntidadesEn(casilla).OfType<Fruta>();

            if (frutas.Count() > 0)
            {
                // Consumir y eliminar cada fruta
                frutas.ToList().ForEach(fruta =>
                {
                    Jugador.ConsumirFruta(fruta);
                    Mundo.EliminarEntidad(fruta);
                    Puntuacion++;
                });
            }
        }

        private void AparecerFruta()
        {
            Random random = new Random();
            
            // No tomar en cuenta los bordes
            int posX, posY;

            if (ComidaActual == null)
            {
                // La primera siempre aparecerá aquí
                posX = 11;
                posY = 9;
            } else
            {
                // Hasta que encuentre una posición donde no haya ninguna entidad dentro del área jugable
                do
                {
                    posX = random.Next(this.Mundo.TamañoX - 2) + 1;
                    posY = random.Next(this.Mundo.TamañoY - 2) + 1;
                } while (this.Mundo.ConsultarEntidadesEn(posX, posY).Count > 0);
            }

            this.ComidaActual = (Fruta)Mundo.GenerarEntidad(TipoEntidad.Comida, new Coordenada(posX, posY), Direccion.Derecha);
            this.ultimoTickCrecimiento = TickActual;
        }

        public void ComenzarPartida()
        {

            this.Jugador = (Serpiente)Mundo.GenerarEntidad(TipoEntidad.Serpiente, new Coordenada(5, 9), Direccion.Derecha, (j) => { ((Serpiente)j).Skin = SkinSeleccionada; });
            this.AparecerFruta();

            this.EstadoActual = Estado.Iniciado;

            if (AlComenzar != null)
            {
                AlComenzar();
            }
        }

        public void AlFinalizar()
        {
            if (Jugador != null)
            {
                Mundo.EliminarEntidad(this.Jugador);
                this.Jugador.Partes.ForEach(cola => Mundo.EliminarEntidad(cola));
            }
            if (ComidaActual != null) Mundo.EliminarEntidad(this.ComidaActual);

            if (Puntuacion > Perfil.Instance.PuntuacionMaxima)
            {
                Perfil.Instance.PuntuacionMaxima = Puntuacion;
            }
            Puntuacion = 0;
        }

        public void PrepararSiguienteRonda()
        {
            this.Mundo.ActualizarColores();
            ComenzarPartida();
        }

        public void AlPresionarTecla(Keys tecla)
        {
            Direccion direccion = this.Jugador.Direccion;
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

            if (direccion != Direcciones.OpuestaDe(this.Jugador.Direccion))
            {

                // Mover solo si se mueve hacia una dirección que no sea contraria o la misma que la actual

                // NO se ejecuta la instrucción "this.serpiente.MirarHacia(direccion);" porque permitiría mirar hacia atrás en algunos casos
                // Se espera al próximo tick
                this.proximaDireccion = direccion;
            }
        }

        public bool HaTerminado()
        {
            return EstadoActual == Estado.Terminado;
        }
    }

    public enum Estado
    {
        Esperando,
        Preparado,
        Iniciado,
        Jugando,
        Pausa,
        Terminado
    }
}
