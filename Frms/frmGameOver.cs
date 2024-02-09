using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace SnakeGame.Frms
{
    public partial class frmGameOver : Form
    {
        private MenuGameOver menu;
        internal frmJuego frmOriginal;

        public frmGameOver(frmJuego parent)
        {
            InitializeComponent();
            this.BackColor = Program.Juego.Mundo.Generador.BordeUno;
            this.pnlTop.BackColor = Program.Juego.Mundo.Generador.BordeDos;

            this.menu = new MenuGameOver()
            {
                frm = this,
                Puntero = lblCabeza,
                lblReintentar = this.lblReintentar,
                lblPerfil = this.lblPerfil,
                lblAjustes = this.lblAjustes,
                lblSalir = this.lblSalir,
                Controles = new List<Control>() { lblReintentar, lblPerfil, lblAjustes, lblSalir  }
            };
            this.menu.Actualizar();
            this.frmOriginal = parent;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            this.menu.EscucharAlTecleado(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    internal class MenuGameOver : MenuPuntero
    {
        public frmGameOver frm { get; set; }
        public Label lblReintentar { get; set; }
        public Label lblPerfil { get; set; }
        public Label lblAjustes { get; set; }
        public Label lblSalir { get; set; }

        public MenuGameOver()
        {
            AlHacerClick += Click;
        }

        private void Click(Control sender, int opcionActual)
        {
            if (sender == lblReintentar)
            {
                this.frm.Close();
                Program.Juego.PrepararSiguienteRonda();
            } else if (sender == lblAjustes)
            {
                frmOpciones frmOpciones = new frmOpciones();
                frmOpciones.ShowDialog(this.frm);
            } else if (sender == lblPerfil)
            {
                frmPerfil frmPerfil = new frmPerfil();
                frmPerfil.ShowDialog(this.frm);
            } else if (sender == lblSalir)
            {
                Program.Juego.EstadoActual = Estado.Esperando;
                frm.Close();
                frm.frmOriginal.InicializarMenuPrincipal();
            }
        }
    }
}
