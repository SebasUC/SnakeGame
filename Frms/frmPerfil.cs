using SnakeGame.Personalizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Frms
{
    public partial class frmPerfil : Form
    {
        private MenuPerfil menu;

        public frmPerfil()
        {
            InitializeComponent();

            // Si la partida está en progreso, utilizar el fondo del mapa
            if (Program.Juego.EstadoActual == Estado.Jugando || Program.Juego.EstadoActual == Estado.Pausa)
            {
                this.BackColor = Program.Juego.Mundo.Generador.BordeDos;
            }

            this.menu = new MenuPerfil()
            {
                frm = this,
                lblOpcionNombre = lblNombre,
                lblTextoNombre = lblEscribirNick,
                lblAzul = lblCabezaAzul,
                lblVerde = lblCabezaVerde,
                lblAmarillo = lblCabezaAmarilla,
                lblCerrar = lblRegresar,
                Controles = new List<Control>() { lblNombre, lblCabezaAzul, lblCabezaVerde, lblCabezaAmarilla, lblRegresar }
            };
            this.menu.Actualizar();

            lblEscribirNick.Text = Perfil.Instance.Nombre;
            lblPuntuacionMaxima.Text = $"Puntuación máxima: {Perfil.Instance.PuntuacionMaxima}";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.menu.EscucharAlTecleado(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    internal class MenuPerfil : Menu
    {
        public frmPerfil frm;
        public Label lblOpcionNombre { get; set; }
        public Label lblTextoNombre { get; set; }
        public Label lblAzul { get; set; }
        public Label lblVerde { get; set; }
        public Label lblAmarillo { get; set; }
        public Label lblCerrar { get; set; }
        public bool EscuchandoNombre { get; set; }

        private Label actualIconoSeleccionado;
        private bool sinDigitar;

        public MenuPerfil()
        {
            AlHacerClick += Click;
            AlHacerHover += Hover;
        }

        public override void Actualizar()
        {
            // Si ya hay alguien seleccionado anteriormente, restablecer color antes de buscar el nuevo seleccionado
            if (actualIconoSeleccionado != null)
            {
                actualIconoSeleccionado.BackColor = Color.Black;
            }

            if (Program.Juego.SkinSeleccionada == Skins.SkinVerde)
            {
                actualIconoSeleccionado = lblVerde;
            }
            else if (Program.Juego.SkinSeleccionada == Skins.SkinAzul)
            {
                actualIconoSeleccionado = lblAzul;
            }
            else
            {
                actualIconoSeleccionado = lblAmarillo;
            }

            actualIconoSeleccionado.BackColor = Color.OrangeRed;

            base.Actualizar();
        }

        public override void EscucharAlTecleado(Keys key)
        {
            if (this.EscuchandoNombre)
            {
                if (!sinDigitar && lblTextoNombre.Text != string.Empty && (key == Keys.Escape || key == Keys.Enter))
                {
                    // Confirmar
                    Perfil.Instance.Nombre = lblTextoNombre.Text;
                    // Restablecer color
                    lblTextoNombre.ForeColor = Color.Silver;
                    
                    this.EscuchandoNombre = false;
                    ControladorSonido.Tocar(Sonido.Food, false);
                } else
                {

                    if (sinDigitar)
                    {
                        // Quitar texto de "previsualización"
                        lblTextoNombre.Text = "";
                    }

                    if (lblTextoNombre.Text.Length > 0 && (key == Keys.Back || key == Keys.Delete))
                    {
                        // Borrar
                        lblTextoNombre.Text = lblTextoNombre.Text.Substring(0, lblTextoNombre.Text.Length - 1);
                    }
                    else if (char.IsLetterOrDigit(((char)key)) && lblTextoNombre.Text.Length < 16)
                    {
                        // Escribir
                        lblTextoNombre.Text += ((char)key);
                    }

                    sinDigitar = false;
                    ControladorSonido.Tocar(Sonido.Move, false);
                }
            } else
            {
                base.EscucharAlTecleado(key);
            }
        }

        private void Hover(Control nuevoControl, Control antiguoControl)
        {
            // Restablecer el fondo del control anterior
            if (EsIcono(antiguoControl) && antiguoControl != actualIconoSeleccionado)
            {
                antiguoControl.BackColor = Color.Black;
            }
            else if (antiguoControl != null)
            {
                antiguoControl.ForeColor = Color.White;
            }

            // Establecer un color para indicar que es la opción actual
            if (EsIcono(nuevoControl) && nuevoControl != actualIconoSeleccionado)
            {
                nuevoControl.BackColor = Color.Silver;
            }
            else
            {
                nuevoControl.ForeColor = Color.Silver;
            }

            ControladorSonido.Tocar(Sonido.Move, false);
        }

        private void Click(Control sender, int opcionSeleccionada)
        {
            if (!EscuchandoNombre)
            {
                if (sender == lblOpcionNombre)
                {
                    this.lblTextoNombre.Text = "Escribir nombre";
                    this.lblTextoNombre.ForeColor = Color.Yellow;
                    this.EscuchandoNombre = true;
                    this.sinDigitar = true;
                }
                else if (sender == lblAzul)
                {
                    Program.Juego.SkinSeleccionada = Skins.SkinAzul;
                }
                else if (sender == lblVerde)
                {
                    Program.Juego.SkinSeleccionada = Skins.SkinVerde;
                }
                else if (sender == lblAmarillo)
                {
                    Program.Juego.SkinSeleccionada = Skins.SkinAmarillo;
                }
                else if (sender == lblCerrar)
                {
                    frm.Close();
                }

                this.Actualizar();
                ControladorSonido.Tocar(Sonido.Pow, false);
            }
        }

        private bool EsIcono(Control control)
        {
            return control == lblAzul || control == lblVerde || control == lblAmarillo;
        }
    }
}
