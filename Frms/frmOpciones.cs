using SnakeGame.Mapa;
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
    public partial class frmOpciones : Form
    {
        private MenuOpciones menu;

        public frmOpciones()
        {
            InitializeComponent();

            // Si la partida está en progreso, utilizar el fondo del mapa
            if (Program.Juego.EstadoActual == Estado.Jugando || Program.Juego.EstadoActual == Estado.Pausa)
            {
                this.BackColor = Program.Juego.Mundo.Generador.BordeDos;
            }

            this.menu = new MenuOpciones()
            {
                frm = this,
                lblLimites = this.lblLimites,
                lblClasico = this.lblClasico,
                lblInvernal = this.lblInvernal,
                lblDesierto = this.lblDesierto,
                lblCerrar = this.lblRegresar,
                Controles = new List<Control>() { lblLimites, lblClasico, lblInvernal, lblDesierto, lblRegresar }
            };
            this.menu.Actualizar();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.menu.EscucharAlTecleado(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    internal class MenuOpciones : Menu
    {
        public frmOpciones frm { get; set; }
        public Label lblLimites { get; set; }
        public Label lblClasico { get; set; }
        public Label lblInvernal { get; set; }
        public Label lblDesierto { get; set; }
        public Label lblCerrar { get; set; }

        private Label mapaSeleccionado;

        public MenuOpciones()
        {
            AlHacerHover += Hover;
            AlHacerClick += Click;
        }

        private void Hover(Control nuevoControl, Control antiguoControl)
        {
            if (antiguoControl != null && antiguoControl.ForeColor != Color.Black)
            {
                antiguoControl.ForeColor = Color.White;
            }

            if (nuevoControl.ForeColor != Color.Black)
            {
                nuevoControl.ForeColor = Color.Silver;
            }
        }

        private void Click(Control sender, int opcionActual)
        {
            if (sender == lblLimites)
            {
                Program.Juego.LimitesActivos = !Program.Juego.LimitesActivos;
            }

            if (sender == lblClasico)
            {
                Program.Juego.Mundo.Generador = Generadores.GenClasico;
            } else if (sender == lblInvernal)
            {
                Program.Juego.Mundo.Generador = Generadores.GenNevado;
            } else if (sender == lblDesierto)
            {
                Program.Juego.Mundo.Generador = Generadores.GenDesierto;
            } else if (sender == lblCerrar)
            {
                this.frm.Close();
            }

            Actualizar();
        }

        public override void Actualizar()
        {
            if (mapaSeleccionado != null)
            {
                mapaSeleccionado.ForeColor = Color.White;
            }

            if (Program.Juego.Mundo.Generador == Generadores.GenClasico)
            {
                mapaSeleccionado = lblClasico;
            } else if (Program.Juego.Mundo.Generador == Generadores.GenDesierto)
            {
                mapaSeleccionado = lblDesierto;
            } else
            {
                mapaSeleccionado = lblInvernal;
            }

            mapaSeleccionado.ForeColor = Color.Black;

            lblLimites.Text = $"Límites del mapa ({(Program.Juego.LimitesActivos ? "Activado" : "Desactivado")})";

            base.Actualizar();
        }
    }
}
