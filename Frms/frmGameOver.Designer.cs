namespace SnakeGame.Frms
{
    partial class frmGameOver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGameOver));
            this.lblReintentar = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCabeza = new System.Windows.Forms.Label();
            this.lblAjustes = new System.Windows.Forms.Label();
            this.lblSalir = new System.Windows.Forms.Label();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReintentar
            // 
            this.lblReintentar.BackColor = System.Drawing.Color.Transparent;
            this.lblReintentar.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReintentar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblReintentar.Location = new System.Drawing.Point(0, 90);
            this.lblReintentar.Name = "lblReintentar";
            this.lblReintentar.Size = new System.Drawing.Size(550, 50);
            this.lblReintentar.TabIndex = 9;
            this.lblReintentar.Text = "Reintentar";
            this.lblReintentar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.lblTitulo);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(550, 90);
            this.pnlTop.TabIndex = 11;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("AniMe Matrix - MB_EN", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Silver;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(550, 90);
            this.lblTitulo.TabIndex = 19;
            this.lblTitulo.Text = "GAME OVER";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCabeza
            // 
            this.lblCabeza.BackColor = System.Drawing.Color.Transparent;
            this.lblCabeza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCabeza.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCabeza.ForeColor = System.Drawing.Color.Transparent;
            this.lblCabeza.Image = ((System.Drawing.Image)(resources.GetObject("lblCabeza.Image")));
            this.lblCabeza.Location = new System.Drawing.Point(58, 90);
            this.lblCabeza.Name = "lblCabeza";
            this.lblCabeza.Size = new System.Drawing.Size(50, 50);
            this.lblCabeza.TabIndex = 10;
            this.lblCabeza.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAjustes
            // 
            this.lblAjustes.BackColor = System.Drawing.Color.Transparent;
            this.lblAjustes.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAjustes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAjustes.Location = new System.Drawing.Point(0, 190);
            this.lblAjustes.Name = "lblAjustes";
            this.lblAjustes.Size = new System.Drawing.Size(550, 50);
            this.lblAjustes.TabIndex = 12;
            this.lblAjustes.Text = "Ajustes";
            this.lblAjustes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSalir
            // 
            this.lblSalir.BackColor = System.Drawing.Color.Transparent;
            this.lblSalir.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalir.ForeColor = System.Drawing.SystemColors.Control;
            this.lblSalir.Location = new System.Drawing.Point(-7, 240);
            this.lblSalir.Name = "lblSalir";
            this.lblSalir.Size = new System.Drawing.Size(550, 50);
            this.lblSalir.TabIndex = 13;
            this.lblSalir.Text = "Salir";
            this.lblSalir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPerfil
            // 
            this.lblPerfil.BackColor = System.Drawing.Color.Transparent;
            this.lblPerfil.Font = new System.Drawing.Font("Pusab", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerfil.ForeColor = System.Drawing.SystemColors.Control;
            this.lblPerfil.Location = new System.Drawing.Point(-7, 140);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(550, 50);
            this.lblPerfil.TabIndex = 14;
            this.lblPerfil.Text = "Perfil";
            this.lblPerfil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmGameOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(530, 530);
            this.ControlBox = false;
            this.Controls.Add(this.lblCabeza);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.lblReintentar);
            this.Controls.Add(this.lblAjustes);
            this.Controls.Add(this.lblSalir);
            this.Controls.Add(this.lblPerfil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGameOver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGameOver";
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCabeza;
        private System.Windows.Forms.Label lblReintentar;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAjustes;
        private System.Windows.Forms.Label lblSalir;
        private System.Windows.Forms.Label lblPerfil;
    }
}