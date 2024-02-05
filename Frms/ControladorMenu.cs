using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SnakeGame.Frms
{
    internal class ControladorMenu
    {
        public List<Label> Etiquetas { get; internal set; } = new List<Label>();
        public int OpcionSeleccionada { get; private set; }
        public Action<Label> AlHacerClick {  get; set; }

        public ControladorMenu AgregarOpcion(Label label) {
            this.Etiquetas.Add(label);
            return this;
        }

        public void EscucharAlTecleado(Keys key)
        {
            // Menú principal
            if (OpcionSeleccionada < this.Etiquetas.Count && (key == Keys.S || key == Keys.Down))
            {
                // Puede ir hacia abajo
                OpcionSeleccionada++;
                ActualizarPosicionPuntero();
            } else if (OpcionSeleccionada > 0 && (key == Keys.W || key == Keys.Up))
            {
                // Puede ir hacia arriba
                OpcionSeleccionada--;
                ActualizarPosicionPuntero();
            } else if (key == Keys.Enter)
            {
                // Seleccionar opción
                SeleccionarOpcion();
            }
        }

        private void ActualizarPosicionPuntero()
        {
            throw new NotImplementedException();
        }

        private void SeleccionarOpcion()
        {
            AlHacerClick?.Invoke(this.Etiquetas[this.OpcionSeleccionada]);
        }
    }
}
