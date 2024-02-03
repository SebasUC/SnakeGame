using SnakeGame.Entidad;
using SnakeGame.Mapa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Frms
{
    public class frmJuego : Form
    {

        public frmJuego()
        {

            // Configurar Formulario
            this.Size = new Size(610, 630);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Suscribirse al evento de cerrar
            this.FormClosed += (sender, e) =>
            {
                Program.Juego.Terminado = true;
            };

            // Suscribirse al evento de pintar
            this.Paint += frmJuego_Paint;


            new Controlador(this);
        }

        private void frmJuego_Paint(object sender, PaintEventArgs e)
        {
            // Crear un objeto Graphics a partir del evento Paint
            Graphics g = e.Graphics;

            foreach (Casilla casilla in Program.Juego.Mundo.Casillas)
            {
                SolidBrush brush = new SolidBrush(casilla.Color);
                g.FillRectangle(brush, Utils.Escalar(casilla.Posicion.X), Utils.Escalar(casilla.Posicion.Y), Utils.TamañoCasilla, Utils.TamañoCasilla);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Program.Juego.AlPresionarTecla(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal void AgregarBox(PictureBox pictureBox)
        {
            EjecutarEnHiloPrincipal(() =>
            {
                this.Controls.Add(pictureBox);
            });
        }

        internal void EjecutarEnHiloPrincipal(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            } else
            {
                action.Invoke();
            }
        }
    }

    internal class Controlador
    {
        private frmJuego frm;
        private Dictionary<int, PictureBox> componentes;

        public Controlador(frmJuego frm)
        {
            this.frm = frm;
            this.componentes = new Dictionary<int, PictureBox>();

            Program.Juego.AlHacerTick += Tick;
            Program.Juego.Mundo.EventoAparicionEntidad += AlAparicerEntidad;
            Program.Juego.Mundo.EventoEliminacionEntidad += AlEliminarEntidad;
        }

        public void Tick()
        {
            foreach (AbsEntidad entidad in Program.Juego.Mundo.Entidades)
            {
                if (entidad.NecesitaActualizar)
                {
                    ActualizarEntidad(entidad);
                }
            }
        }

        private void AlAparicerEntidad(AbsEntidad sender, EventArgs args)
        {
            GenerarComponenteGrafico(sender);
        }

        private void AlEliminarEntidad(AbsEntidad sender, EventArgs args)
        {
            
        }

        public void ActualizarEntidad(AbsEntidad entidad)
        {
            entidad.NecesitaActualizar = false; // Marcar la entidad como actualizada
            if (this.componentes.ContainsKey(entidad.Id) && entidad is IRenderizable<Image> r)
            {
                //Console.WriteLine($"Actualizando entidad {entidad.Id}");

                this.frm.EjecutarEnHiloPrincipal(() =>
                {
                    PictureBox pictureBox = this.componentes[entidad.Id];
                    pictureBox.Location = new Point(Utils.Escalar(entidad.Posicion.X), Utils.Escalar(entidad.Posicion.Y));
                    pictureBox.Image = r.ObtenerImagen();

                    // Refrecar
                    pictureBox.Invalidate();
                    //Console.WriteLine("Componente actualizado: " + pictureBox.Location.X + " | " + entidad.Direccion);
                });
            }
        }

        private void GenerarComponenteGrafico(AbsEntidad entidad)
        {
            if (entidad is IRenderizable<Image> r)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(Utils.TamañoCasilla, Utils.TamañoCasilla),
                    Location = new Point(Utils.Escalar(entidad.Posicion.X), Utils.Escalar(entidad.Posicion.Y)),
                    Image = r.ObtenerImagen(),
                    SizeMode = PictureBoxSizeMode.Normal
                };

                componentes.Add(entidad.Id, pictureBox);

                frm.AgregarBox(pictureBox);
            }
        }
    }
}
