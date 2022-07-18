namespace Punto_Venta
{
    partial class frmComandaGeneral
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnEntregarComanda = new System.Windows.Forms.Button();
            this.BtnCancelarComanda = new System.Windows.Forms.Button();
            this.dgvComanda = new System.Windows.Forms.DataGridView();
            this.dgvRuta = new System.Windows.Forms.DataGridView();
            this.dgvCocina = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComanda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRuta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCocina)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Stencil", 15F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 346);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 24);
            this.label2.TabIndex = 26;
            this.label2.Text = "** EN MESA **";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Stencil", 15F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "** EN COCINA **";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Stencil", 15F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(13, 370);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 24);
            this.label9.TabIndex = 24;
            this.label9.Text = "** EN RUTA **";
            // 
            // BtnEntregarComanda
            // 
            this.BtnEntregarComanda.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnEntregarComanda.Font = new System.Drawing.Font("Stencil", 15F);
            this.BtnEntregarComanda.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnEntregarComanda.Location = new System.Drawing.Point(363, 350);
            this.BtnEntregarComanda.Name = "BtnEntregarComanda";
            this.BtnEntregarComanda.Size = new System.Drawing.Size(207, 46);
            this.BtnEntregarComanda.TabIndex = 23;
            this.BtnEntregarComanda.Text = "Cobrar";
            this.BtnEntregarComanda.UseVisualStyleBackColor = false;
            this.BtnEntregarComanda.Click += new System.EventHandler(this.BtnEntregarComanda_Click);
            // 
            // BtnCancelarComanda
            // 
            this.BtnCancelarComanda.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnCancelarComanda.Font = new System.Drawing.Font("Stencil", 15F);
            this.BtnCancelarComanda.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnCancelarComanda.Location = new System.Drawing.Point(576, 350);
            this.BtnCancelarComanda.Name = "BtnCancelarComanda";
            this.BtnCancelarComanda.Size = new System.Drawing.Size(225, 46);
            this.BtnCancelarComanda.TabIndex = 22;
            this.BtnCancelarComanda.Text = "Cancelar";
            this.BtnCancelarComanda.UseVisualStyleBackColor = false;
            this.BtnCancelarComanda.Click += new System.EventHandler(this.BtnCancelarComanda_Click);
            // 
            // dgvComanda
            // 
            this.dgvComanda.AllowUserToAddRows = false;
            this.dgvComanda.AllowUserToDeleteRows = false;
            this.dgvComanda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvComanda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComanda.Location = new System.Drawing.Point(30, 397);
            this.dgvComanda.Name = "dgvComanda";
            this.dgvComanda.ReadOnly = true;
            this.dgvComanda.Size = new System.Drawing.Size(807, 292);
            this.dgvComanda.TabIndex = 21;
            this.dgvComanda.Visible = false;
            // 
            // dgvRuta
            // 
            this.dgvRuta.AllowUserToAddRows = false;
            this.dgvRuta.AllowUserToDeleteRows = false;
            this.dgvRuta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRuta.Location = new System.Drawing.Point(12, 397);
            this.dgvRuta.Name = "dgvRuta";
            this.dgvRuta.ReadOnly = true;
            this.dgvRuta.Size = new System.Drawing.Size(1290, 292);
            this.dgvRuta.TabIndex = 20;
            // 
            // dgvCocina
            // 
            this.dgvCocina.AllowUserToAddRows = false;
            this.dgvCocina.AllowUserToDeleteRows = false;
            this.dgvCocina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCocina.Location = new System.Drawing.Point(12, 49);
            this.dgvCocina.Name = "dgvCocina";
            this.dgvCocina.ReadOnly = true;
            this.dgvCocina.Size = new System.Drawing.Size(1290, 298);
            this.dgvCocina.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Font = new System.Drawing.Font("Stencil", 15F);
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(840, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 41);
            this.button1.TabIndex = 27;
            this.button1.Text = "ENTREGAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Font = new System.Drawing.Font("Stencil", 15F);
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(594, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 41);
            this.button2.TabIndex = 28;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.Font = new System.Drawing.Font("Stencil", 15F);
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(594, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 41);
            this.button3.TabIndex = 30;
            this.button3.Text = "Cancelar";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button4.Font = new System.Drawing.Font("Stencil", 15F);
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(363, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(225, 41);
            this.button4.TabIndex = 29;
            this.button4.Text = "VER ORDEN";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmComandaGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1314, 733);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BtnEntregarComanda);
            this.Controls.Add(this.BtnCancelarComanda);
            this.Controls.Add(this.dgvRuta);
            this.Controls.Add(this.dgvCocina);
            this.Controls.Add(this.dgvComanda);
            this.Name = "frmComandaGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmComandaGeneral";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmComandaGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmComandaGeneral_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmComandaGeneral_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComanda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRuta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCocina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnEntregarComanda;
        private System.Windows.Forms.Button BtnCancelarComanda;
        private System.Windows.Forms.DataGridView dgvComanda;
        private System.Windows.Forms.DataGridView dgvRuta;
        private System.Windows.Forms.DataGridView dgvCocina;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}