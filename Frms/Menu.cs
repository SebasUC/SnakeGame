using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame.Frms
{
    internal class Menu
    {
        public Control ComponenteActual { get; set; }

        private List<Control> _controles;
        public List<Control> Controles
        {
            get { return _controles; }

            internal set
            {
                this._controles = value;
                this.ConfigurarControles();
            }
        }
        public int OpcionSeleccionada { get; internal set; }

        
        public delegate void EventoClick(Control sender, int opcionActual);
        public EventoClick AlHacerClick { get; set; }

        public delegate void EventoHover(Control nuevoControl, Control antiguoControl);
        public EventoHover AlHacerHover { get; set; }

        public void ConfigurarControles()
        {

            for (int i = 0; i < Controles.Count; i++)
            {
                Control control = Controles[i];
                int indiceActual = i; // Guardar en una variable, ya que después i terminará en el valor final

                control.Click += (ctr, a) =>
                {
                    // Básicamente hace que la selección se coloque en esta opción y haga click
                    this.OpcionSeleccionada = Controles.IndexOf(control);
                    this.Actualizar();
                    EscucharAlTecleado(Keys.Enter);
                };

                control.MouseHover += (ctr, a) =>
                {
                    OpcionSeleccionada = indiceActual;
                    Actualizar();
                };
            }
        }

        public virtual void EscucharAlTecleado(Keys key)
        {
            if (OpcionSeleccionada < (this.Controles.Count - 1) && (key == Keys.S || key == Keys.Down || key == Keys.D || key == Keys.Right))
            {
                // Puede ir hacia abajo
                OpcionSeleccionada++;
                Actualizar();
            } else if (OpcionSeleccionada > 0 && (key == Keys.W || key == Keys.Up || key == Keys.A || key == Keys.Left))
            {
                // Puede ir hacia arriba
                OpcionSeleccionada--;
                Actualizar();
            } else if (key == Keys.Enter)
            {
                // Seleccionar opción
                SeleccionarOpcion();
            }
        }

        public virtual void Actualizar()
        {
            if (AlHacerHover != null) {
                // Llamar al evento que actualiza visualmente el componente "seleccionado" actual
                AlHacerHover(Controles[OpcionSeleccionada], ComponenteActual);
            }
            ComponenteActual = Controles[OpcionSeleccionada];

            ControladorSonido.Tocar(Sonido.Move, false);
        }

        protected virtual void SeleccionarOpcion()
        {
            ControladorSonido.Tocar(Sonido.Pow, false);
            if (AlHacerClick != null)
            {
                AlHacerClick(this.Controles[this.OpcionSeleccionada], OpcionSeleccionada);
            }
        }

        public virtual void OcultarOpciones()
        {
            this.Controles.ForEach(opcion => { opcion.Visible = false; });
        }
    }
}
