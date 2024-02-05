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
        private Label lblJugar;
        private Label lblTitulo;
        private Label lblPerfil;
        private Label lblAjustes;
        private Label lblCabeza;
        private Panel pnlPantalla;

        private List<Label> opciones;
        private Label lblPuntuacion;
        private Label lblTrofeo;
        private int opcionActual;

        public frmJuego()
        {
            new Controlador(this);
            this.InitializeComponent();

            // Suscribirse al evento de cerrar
            this.FormClosed += (sender, e) =>
            {
                Program.Juego.TerminarPartida();
            };

            Program.Juego.AlPuntuar += () =>
            {
                EjecutarEnHiloPrincipal(() =>
                {
                    this.lblPuntuacion.Text = $"{Program.Juego.Puntuacion}";
                });
            };


            opciones = new List<Label>() { lblJugar, lblPerfil, lblAjustes };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Program.Juego.EstadoActual == Estado.Esperando)
            {
                // Menú principal
                if (opcionActual < 2 && (keyData == Keys.S || keyData == Keys.Down))
                {
                    // Puede ir hacia abajo
                    opcionActual++;
                    ActualizarPosicionCabeza();
                }

                if (opcionActual > 0 && (keyData == Keys.W || keyData == Keys.Up))
                {
                    // Puede ir hacia arriba
                    opcionActual--;
                    ActualizarPosicionCabeza();
                }

                if (keyData == Keys.Enter)
                {
                    // Seleccionar opción
                    SeleccionarOpcion();
                }
            } else
            {
                Program.Juego.AlPresionarTecla(keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SeleccionarOpcion()
        {
            if (this.opcionActual == 0)
            {
                // Jugar
                this.OcultarMenuPrincipal();
                this.pnlPantalla.Visible = true;
                this.pnlPantalla.Location = this.opciones[0].Location;

                this.lblPuntuacion.Text = "0";
                this.lblPuntuacion.Visible = true;
                this.lblTrofeo.Visible = true;

                Program.Juego.EstadoActual = Estado.Preparado;
            }

            ControladorSonido.Tocar(Sonido.Pow, false);
        }

        private void OcultarMenuPrincipal()
        {
            this.lblCabeza.Visible = false;
            this.opciones.ForEach(opcion => { opcion.Visible = false; });
        }

        private void ActualizarPosicionCabeza()
        {
            this.lblCabeza.Location = new Point(lblCabeza.Location.X, this.opciones[opcionActual].Location.Y);
            ControladorSonido.Tocar(Sonido.Move, false);
        }

        internal void AgregarBox(PictureBox pictureBox)
        {
            EjecutarEnHiloPrincipal(() =>
            {
                this.pnlPantalla.Controls.Add(pictureBox);
            });
        }

        internal void EliminarBox(PictureBox pictureBox)
        {
            EjecutarEnHiloPrincipal(() =>
            {
                this.pnlPantalla.Controls.Remove(pictureBox);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJuego));
            this.pnlPantalla = new System.Windows.Forms.Panel();
            this.lblJugar = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.lblAjustes = new System.Windows.Forms.Label();
            this.lblCabeza = new System.Windows.Forms.Label();
            this.lblPuntuacion = new System.Windows.Forms.Label();
            this.lblTrofeo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlPantalla
            // 
            this.pnlPantalla.Location = new System.Drawing.Point(1000, 1000);
            this.pnlPantalla.Name = "pnlPantalla";
            this.pnlPantalla.Size = new System.Drawing.Size(500, 500);
            this.pnlPantalla.TabIndex = 0;
            this.pnlPantalla.Paint += new System.Windows.Forms.PaintEventHandler(this.pintarMapa);
            // 
            // lblJugar
            // 
            this.lblJugar.BackColor = System.Drawing.Color.Black;
            this.lblJugar.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJugar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblJugar.Location = new System.Drawing.Point(196, 140);
            this.lblJugar.Name = "lblJugar";
            this.lblJugar.Size = new System.Drawing.Size(500, 50);
            this.lblJugar.TabIndex = 4;
            this.lblJugar.Text = "Jugar";
            this.lblJugar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Black;
            this.lblTitulo.Font = new System.Drawing.Font("AniMe Matrix - MB_EN", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblTitulo.Location = new System.Drawing.Point(196, 28);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(500, 78);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "Snake";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerfil
            // 
            this.lblPerfil.BackColor = System.Drawing.Color.Black;
            this.lblPerfil.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerfil.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPerfil.Location = new System.Drawing.Point(196, 195);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(500, 50);
            this.lblPerfil.TabIndex = 6;
            this.lblPerfil.Text = "Perfil";
            this.lblPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAjustes
            // 
            this.lblAjustes.BackColor = System.Drawing.Color.Black;
            this.lblAjustes.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAjustes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAjustes.Location = new System.Drawing.Point(196, 250);
            this.lblAjustes.Name = "lblAjustes";
            this.lblAjustes.Size = new System.Drawing.Size(500, 50);
            this.lblAjustes.TabIndex = 7;
            this.lblAjustes.Text = "Ajustes";
            this.lblAjustes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCabeza
            // 
            this.lblCabeza.BackColor = System.Drawing.Color.Black;
            this.lblCabeza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCabeza.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCabeza.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCabeza.Image = ((System.Drawing.Image)(resources.GetObject("lblCabeza.Image")));
            this.lblCabeza.Location = new System.Drawing.Point(294, 140);
            this.lblCabeza.Name = "lblCabeza";
            this.lblCabeza.Size = new System.Drawing.Size(50, 50);
            this.lblCabeza.TabIndex = 8;
            this.lblCabeza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPuntuacion
            // 
            this.lblPuntuacion.BackColor = System.Drawing.Color.Black;
            this.lblPuntuacion.Font = new System.Drawing.Font("Pusab", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuntuacion.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPuntuacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPuntuacion.Location = new System.Drawing.Point(91, 140);
            this.lblPuntuacion.Name = "lblPuntuacion";
            this.lblPuntuacion.Size = new System.Drawing.Size(99, 50);
            this.lblPuntuacion.TabIndex = 9;
            this.lblPuntuacion.Text = "0";
            this.lblPuntuacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPuntuacion.Visible = false;
            // 
            // lblTrofeo
            // 
            this.lblTrofeo.BackColor = System.Drawing.Color.Black;
            this.lblTrofeo.Font = new System.Drawing.Font("Pusab", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrofeo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTrofeo.Image = ((System.Drawing.Image)(resources.GetObject("lblTrofeo.Image")));
            this.lblTrofeo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrofeo.Location = new System.Drawing.Point(12, 140);
            this.lblTrofeo.Name = "lblTrofeo";
            this.lblTrofeo.Size = new System.Drawing.Size(73, 50);
            this.lblTrofeo.TabIndex = 10;
            this.lblTrofeo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrofeo.Visible = false;
            // 
            // frmJuego
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(876, 738);
            this.Controls.Add(this.lblTrofeo);
            this.Controls.Add(this.lblPuntuacion);
            this.Controls.Add(this.lblCabeza);
            this.Controls.Add(this.lblAjustes);
            this.Controls.Add(this.lblPerfil);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblJugar);
            this.Controls.Add(this.pnlPantalla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmJuego";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snake Game";
            this.Load += new System.EventHandler(this.frmJuego_Load);
            this.ResumeLayout(false);

        }

        private void pintarMapa(object sender, PaintEventArgs e)
        {

            // Crear un objeto Graphics a partir del evento Paint
            Graphics g = e.Graphics;

            foreach (Casilla casilla in Program.Juego.Mundo.Casillas)
            {
                SolidBrush brush = new SolidBrush(casilla.Color);
                g.FillRectangle(brush, Utils.Escalar(casilla.Posicion.X), Utils.Escalar(casilla.Posicion.Y), Utils.TamañoCasilla, Utils.TamañoCasilla);
            }
        }

        private void frmJuego_Load(object sender, EventArgs e)
        {
            ControladorSonido.Tocar(Sonido.Music, true);
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

        private void AlEliminarEntidad(AbsEntidad entidad, EventArgs args)
        {
            if (this.componentes.ContainsKey(entidad.Id) && entidad is IRenderizable<Image> r)
            {
                this.frm.EjecutarEnHiloPrincipal(() =>
                {
                    PictureBox pictureBox = this.componentes[entidad.Id];
                    pictureBox.Invalidate();
                    frm.EliminarBox(pictureBox);
                });
            }
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
