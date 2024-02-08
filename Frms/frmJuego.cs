using SnakeGame.Entidad;
using SnakeGame.Mapa;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SnakeGame.Frms
{
    public class frmJuego : Form
    {
        private Label lblTitulo;
        private Panel pnlPantalla;

        private ControladorFrmJuego controlador;
        private Menu menu;

        private Label lblPuntuacion;
        private Label lblTrofeo;
        private Label lblJugar;
        private Label lblAjustes;
        private Label lblPerfil;
        private Label lblSalir;
        private Label lblCabeza;

        public frmJuego()
        {
            this.InitializeComponent();

            // Suscribirse al evento de cerrar
            this.FormClosed += (sender, e) =>
            {
                Program.Juego.TerminarPartida();
            };

            // Suscribirse al evento de puntuar
            Program.Juego.AlPuntuar += () =>
            {
                EjecutarEnHiloPrincipal(() =>
                {
                    this.lblPuntuacion.Text = $"{Program.Juego.Puntuacion}";
                });
            };

            // Construir controladores lógicos
            this.controlador = new ControladorFrmJuego(this);
            this.menu = new MenuPuntero()
            {
                Puntero = this.lblCabeza,
                Controles = new List<Control>() { lblJugar, lblPerfil, lblAjustes, lblSalir },
            };
            this.menu.Actualizar();
            this.menu.AlHacerClick += SeleccionarOpcion;

            //Cursor.Hide();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Program.Juego.EstadoActual == Estado.Esperando)
            {
                // Menú principal
                this.menu.EscucharAlTecleado(keyData);
            } else
            {
                Program.Juego.AlPresionarTecla(keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SeleccionarOpcion(Control control, int opcionSeleccionada)
        {
            if (control == lblJugar)
            {
                // Jugar
                this.menu.OcultarOpciones();
                this.pnlPantalla.Visible = true;
                this.pnlPantalla.Location = lblJugar.Location;

                this.lblPuntuacion.Text = "0";
                this.lblPuntuacion.Visible = true;
                this.lblTrofeo.Visible = true;

//                this.BackColor = Program.Juego.Mundo.Generador.BordeDos;

                Program.Juego.EstadoActual = Estado.Preparado;
            } else if (control == lblPerfil)
            {
                frmPerfil frm = new frmPerfil();
                frm.ShowDialog(this);

                // Si cambiaron el ícono, actualizarlo
                this.menu.OpcionSeleccionada = opcionSeleccionada;
                this.menu.Actualizar();
            } else if (control == lblAjustes)
            {
                frmOpciones frm = new frmOpciones();
                frm.ShowDialog(this);
            } else if (control == lblSalir)
            {
                this.Close();
            }
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblPuntuacion = new System.Windows.Forms.Label();
            this.lblTrofeo = new System.Windows.Forms.Label();
            this.lblJugar = new System.Windows.Forms.Label();
            this.lblAjustes = new System.Windows.Forms.Label();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.lblCabeza = new System.Windows.Forms.Label();
            this.lblSalir = new System.Windows.Forms.Label();
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
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Black;
            this.lblTitulo.Font = new System.Drawing.Font("AniMe Matrix - MB_EN", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblTitulo.Location = new System.Drawing.Point(196, 28);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(500, 78);
            this.lblTitulo.TabIndex = 5;
            this.lblTitulo.Text = "Snake  ";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPuntuacion
            // 
            this.lblPuntuacion.BackColor = System.Drawing.Color.Black;
            this.lblPuntuacion.Font = new System.Drawing.Font("Pusab", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuntuacion.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPuntuacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPuntuacion.Location = new System.Drawing.Point(231, 638);
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
            this.lblTrofeo.Location = new System.Drawing.Point(152, 638);
            this.lblTrofeo.Name = "lblTrofeo";
            this.lblTrofeo.Size = new System.Drawing.Size(73, 50);
            this.lblTrofeo.TabIndex = 10;
            this.lblTrofeo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrofeo.Visible = false;
            // 
            // lblJugar
            // 
            this.lblJugar.BackColor = System.Drawing.Color.Black;
            this.lblJugar.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJugar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblJugar.Location = new System.Drawing.Point(180, 120);
            this.lblJugar.Name = "lblJugar";
            this.lblJugar.Size = new System.Drawing.Size(500, 50);
            this.lblJugar.TabIndex = 4;
            this.lblJugar.Text = "Jugar";
            this.lblJugar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAjustes
            // 
            this.lblAjustes.BackColor = System.Drawing.Color.Black;
            this.lblAjustes.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAjustes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAjustes.Location = new System.Drawing.Point(180, 280);
            this.lblAjustes.Name = "lblAjustes";
            this.lblAjustes.Size = new System.Drawing.Size(500, 50);
            this.lblAjustes.TabIndex = 7;
            this.lblAjustes.Text = "Ajustes";
            this.lblAjustes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerfil
            // 
            this.lblPerfil.BackColor = System.Drawing.Color.Black;
            this.lblPerfil.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerfil.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPerfil.Location = new System.Drawing.Point(180, 200);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(500, 50);
            this.lblPerfil.TabIndex = 6;
            this.lblPerfil.Text = "Perfil";
            this.lblPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCabeza
            // 
            this.lblCabeza.BackColor = System.Drawing.Color.Black;
            this.lblCabeza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCabeza.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCabeza.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCabeza.Image = ((System.Drawing.Image)(resources.GetObject("lblCabeza.Image")));
            this.lblCabeza.Location = new System.Drawing.Point(280, 120);
            this.lblCabeza.Name = "lblCabeza";
            this.lblCabeza.Size = new System.Drawing.Size(50, 50);
            this.lblCabeza.TabIndex = 8;
            this.lblCabeza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSalir
            // 
            this.lblSalir.BackColor = System.Drawing.Color.Black;
            this.lblSalir.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalir.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSalir.Location = new System.Drawing.Point(180, 372);
            this.lblSalir.Name = "lblSalir";
            this.lblSalir.Size = new System.Drawing.Size(500, 50);
            this.lblSalir.TabIndex = 11;
            this.lblSalir.Text = "Salir";
            this.lblSalir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmJuego
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(884, 738);
            this.ControlBox = false;
            this.Controls.Add(this.lblTrofeo);
            this.Controls.Add(this.lblPuntuacion);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCabeza);
            this.Controls.Add(this.lblAjustes);
            this.Controls.Add(this.lblPerfil);
            this.Controls.Add(this.pnlPantalla);
            this.Controls.Add(this.lblJugar);
            this.Controls.Add(this.lblSalir);
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

    internal class ControladorFrmJuego
    {
        private frmJuego frm;
        private Dictionary<int, PictureBox> componentes;

        public ControladorFrmJuego(frmJuego frm)
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
