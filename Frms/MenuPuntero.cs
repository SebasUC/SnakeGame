using SnakeGame.Personalizacion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame.Frms
{
    internal class MenuPuntero : Menu
    {
        public Label Puntero {  get; internal set; }

        public override void Actualizar()
        {
            this.Puntero.Location = new Point(this.Controles[OpcionSeleccionada].Location.X + 50, this.Controles[OpcionSeleccionada].Location.Y);
            this.Puntero.Image = Program.Juego.SkinSeleccionada.TexturasCabeza[Posicion.Direccion.Abajo];
            base.Actualizar();
        }

        public override void MostrarOpciones()
        {
            this.Puntero.Visible = true;
            base.MostrarOpciones();
        }

        public override void OcultarOpciones()
        {
            this.Puntero.Visible = false;
            base.OcultarOpciones();
        }
    }
}
