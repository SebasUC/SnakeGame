using SnakeGame.Entidad.Jugador;
using SnakeGame.Mapa;
using SnakeGame.Personalizacion;
using SnakeGame.Posicion;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static SnakeGame.Personalizacion.Skins;

namespace SnakeGame.Entidad
{
    public class Serpiente : EntidadViva, IRenderizable<Image>
    {
        public override string Nombre => "Serpiente";
        public Skin Skin { get; set; }
        private List<Cola> partes = new List<Cola>();

        public Serpiente(Mundo mundo)
        {
            Colisionable = true;
            mundo.EventoAparicionEntidad += PostGeneracion;
        }

        public void Moverse()
        {
            if (this.Vivo)
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
                if (this.ValidarColisiones())
                {
                    Coordenada siguiente = this.Posicion.MoverHacia(Direccion);

                    if (!Program.Juego.LimitesActivos) {
                        // Si se permite salirse del mapa y la siguiente casilla se saldrá

                        if (siguiente.Y >= (Mundo.TamañoY - 1))
                        {
                            // Si se sale por debajo del mapa, aparecer por arriba del mapa
                            siguiente.Y = 1;
                        }

                        if (siguiente.Y <= 0)
                        {
                            // Si se sale por arriba del mapa, aparecer por debajo del mapa
                            siguiente.Y = Mundo.TamañoY - 2;
                        }

                        if (siguiente.X >= (Mundo.TamañoX - 1))
                        {
                            // Si se sale por la derecha del mapa, aparecer por la izquierda del mapa
                            siguiente.X = 1;
                        }

                        if (siguiente.X <= 0)
                        {
                            // Si se sale por la izquierda del mapa, aparecer por la derecha del mapa
                            siguiente.X = Mundo.TamañoX - 2;
                        }


                    }
                    this.Posicion = siguiente;

                    NecesitaActualizar = true;
                }
            }
        }

        public bool ValidarColisiones()
        {
            Coordenada siguientePosicion = this.Posicion.MoverHacia(this.Direccion);

            if (this.Mundo.EstaDentroDelMapa(siguientePosicion.X, siguientePosicion.Y)) {
                Casilla siguienteCasilla = this.Mundo.ConsultarPosicion(siguientePosicion);

                // Si los limites están activos y la casilla no permite colisiones o hay una entidad colisionable morirá
                if ((!siguienteCasilla.PermiteColisiones && Program.Juego.LimitesActivos) || Mundo.ConsultarEntidadesEn(siguienteCasilla).Any((entidad) => !entidad.Colisionable))
                {
                    // Matar la serpiente
                    this.Morir();
                    return false;
                }
            }

            return true;
        }

        public void MirarHacia(Direccion direccion)
        {
            this.Direccion = direccion;
            foreach (Cola cola in partes)
            {
                cola.Direccion = this.Direccion;
            }

            ControladorSonido.Tocar(Sonido.Move, false);
        }

        public override void Morir()
        {
            base.Morir();
            foreach (Cola cola in partes)
            {
                cola.Morir();
            }

            ControladorSonido.Tocar(Sonido.Pow, false);
        }

        public void ConsumirFruta(Fruta fruta)
        {
            this.Crecer();
            ControladorSonido.Tocar(Sonido.Food, false);
        }

        public void Crecer()
        {
            Direccion contrariaALaUltimaCola = Direcciones.OpuestaDe(this.partes[this.partes.Count - 1].Direccion);
            Coordenada detras = this.Posicion.MoverHacia(contrariaALaUltimaCola); // Detrás

            Cola cola = (Cola)this.Mundo.GenerarEntidad(TipoEntidad.Cola, detras, this.Direccion, (j) => { ((Cola)j).Skin = this.Skin; });
            this.partes.Add(cola);
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
                for (int i = 0; i < 3; i++)
                {
                    // Mover a la direccion contraria para mostrar que está por "detrás"
                    nuevaPos = nuevaPos.MoverHacia(atras);

                    // Generar la cola de la serpiente
                    Cola cola = (Cola)this.Mundo.GenerarEntidad(TipoEntidad.Cola, nuevaPos, this.Direccion, (j) => { ((Cola)j).Skin = this.Skin; });

                    this.partes.Add(cola);
                }
            }
        }

        public Image ObtenerImagen()
        {
            return Vivo ? Skin.TexturasCabeza[this.Direccion] : Skin.TexturasCabezaMuerta[this.Direccion];
        }
    }
}