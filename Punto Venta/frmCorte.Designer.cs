namespace Punto_Venta
{
    partial class frmCorte
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorte));
            this.pnlPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierdo = new System.Windows.Forms.TableLayoutPanel();
            this.lblTituloCorte = new System.Windows.Forms.Label();
            this.dgvCorte = new System.Windows.Forms.DataGridView();
            this.pnlHeaderMeseros = new System.Windows.Forms.Panel();
            this.lblTituloMeseros = new System.Windows.Forms.Label();
            this.btnDetalleMesero = new System.Windows.Forms.Button();
            this.dgvMeseros = new System.Windows.Forms.DataGridView();
            this.pnlDerecho = new System.Windows.Forms.Panel();
            this.pnlMétricas = new System.Windows.Forms.TableLayoutPanel();
            this.cardTarjeta = new System.Windows.Forms.Panel();
            this.lblCredito = new System.Windows.Forms.Label();
            this.lblTitTarjeta = new System.Windows.Forms.Label();
            this.cardEntradas = new System.Windows.Forms.Panel();
            this.lblEntrada = new System.Windows.Forms.Label();
            this.lblTitEntradas = new System.Windows.Forms.Label();
            this.cardSalidas = new System.Windows.Forms.Panel();
            this.lblSalida = new System.Windows.Forms.Label();
            this.lblTitSalidas = new System.Windows.Forms.Label();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblCorte = new System.Windows.Forms.Label();
            this.lblTitTotal = new System.Windows.Forms.Label();
            this.btnCorte = new System.Windows.Forms.Button();
            this.pnlPrincipal.SuspendLayout();
            this.pnlIzquierdo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorte)).BeginInit();
            this.pnlHeaderMeseros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeseros)).BeginInit();
            this.pnlDerecho.SuspendLayout();
            this.pnlMétricas.SuspendLayout();
            this.cardTarjeta.SuspendLayout();
            this.cardEntradas.SuspendLayout();
            this.cardSalidas.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.ColumnCount = 2;
            this.pnlPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.pnlPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.pnlPrincipal.Controls.Add(this.pnlIzquierdo, 0, 0);
            this.pnlPrincipal.Controls.Add(this.pnlDerecho, 1, 0);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Padding = new System.Windows.Forms.Padding(12);
            this.pnlPrincipal.RowCount = 1;
            this.pnlPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlPrincipal.Size = new System.Drawing.Size(1084, 700);
            this.pnlPrincipal.TabIndex = 0;
            // 
            // pnlIzquierdo
            // 
            this.pnlIzquierdo.ColumnCount = 1;
            this.pnlIzquierdo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlIzquierdo.Controls.Add(this.lblTituloCorte, 0, 0);
            this.pnlIzquierdo.Controls.Add(this.dgvCorte, 0, 1);
            this.pnlIzquierdo.Controls.Add(this.pnlHeaderMeseros, 0, 2);
            this.pnlIzquierdo.Controls.Add(this.dgvMeseros, 0, 3);
            this.pnlIzquierdo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIzquierdo.Location = new System.Drawing.Point(15, 15);
            this.pnlIzquierdo.Name = "pnlIzquierdo";
            this.pnlIzquierdo.RowCount = 4;
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlIzquierdo.Size = new System.Drawing.Size(651, 670);
            this.pnlIzquierdo.TabIndex = 0;
            // 
            // lblTituloCorte
            // 
            this.lblTituloCorte.AutoSize = true;
            this.lblTituloCorte.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTituloCorte.ForeColor = System.Drawing.Color.White;
            this.lblTituloCorte.Location = new System.Drawing.Point(3, 0);
            this.lblTituloCorte.Name = "lblTituloCorte";
            this.lblTituloCorte.Size = new System.Drawing.Size(209, 25);
            this.lblTituloCorte.TabIndex = 0;
            this.lblTituloCorte.Text = "Movimientos del Turno";
            // 
            // dgvCorte
            // 
            this.dgvCorte.AllowUserToAddRows = false;
            this.dgvCorte.AllowUserToDeleteRows = false;
            this.dgvCorte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCorte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCorte.Location = new System.Drawing.Point(3, 38);
            this.dgvCorte.Name = "dgvCorte";
            this.dgvCorte.ReadOnly = true;
            this.dgvCorte.RowHeadersVisible = false;
            this.dgvCorte.Size = new System.Drawing.Size(645, 348);
            this.dgvCorte.TabIndex = 1;
            // 
            // pnlHeaderMeseros
            // 
            this.pnlHeaderMeseros.Controls.Add(this.lblTituloMeseros);
            this.pnlHeaderMeseros.Controls.Add(this.btnDetalleMesero);
            this.pnlHeaderMeseros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderMeseros.Location = new System.Drawing.Point(3, 392);
            this.pnlHeaderMeseros.Name = "pnlHeaderMeseros";
            this.pnlHeaderMeseros.Size = new System.Drawing.Size(645, 39);
            this.pnlHeaderMeseros.TabIndex = 2;
            // 
            // lblTituloMeseros
            // 
            this.lblTituloMeseros.AutoSize = true;
            this.lblTituloMeseros.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTituloMeseros.ForeColor = System.Drawing.Color.White;
            this.lblTituloMeseros.Location = new System.Drawing.Point(0, 8);
            this.lblTituloMeseros.Name = "lblTituloMeseros";
            this.lblTituloMeseros.Size = new System.Drawing.Size(171, 25);
            this.lblTituloMeseros.TabIndex = 0;
            this.lblTituloMeseros.Text = "Ventas por Mesero";
            // 
            // btnDetalleMesero
            // 
            this.btnDetalleMesero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetalleMesero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnDetalleMesero.FlatAppearance.BorderSize = 0;
            this.btnDetalleMesero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalleMesero.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDetalleMesero.ForeColor = System.Drawing.Color.White;
            this.btnDetalleMesero.Location = new System.Drawing.Point(515, 3);
            this.btnDetalleMesero.Name = "btnDetalleMesero";
            this.btnDetalleMesero.Size = new System.Drawing.Size(130, 32);
            this.btnDetalleMesero.TabIndex = 1;
            this.btnDetalleMesero.Text = "Ver Detalle";
            this.btnDetalleMesero.UseVisualStyleBackColor = false;
            this.btnDetalleMesero.Click += new System.EventHandler(this.btnDetalleMesero_Click);
            // 
            // dgvMeseros
            // 
            this.dgvMeseros.AllowUserToAddRows = false;
            this.dgvMeseros.AllowUserToDeleteRows = false;
            this.dgvMeseros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMeseros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMeseros.Location = new System.Drawing.Point(3, 437);
            this.dgvMeseros.Name = "dgvMeseros";
            this.dgvMeseros.ReadOnly = true;
            this.dgvMeseros.RowHeadersVisible = false;
            this.dgvMeseros.Size = new System.Drawing.Size(645, 230);
            this.dgvMeseros.TabIndex = 3;
            // 
            // pnlDerecho
            // 
            this.pnlDerecho.Controls.Add(this.pnlMétricas);
            this.pnlDerecho.Controls.Add(this.btnCorte);
            this.pnlDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDerecho.Location = new System.Drawing.Point(672, 15);
            this.pnlDerecho.Name = "pnlDerecho";
            this.pnlDerecho.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlDerecho.Size = new System.Drawing.Size(397, 670);
            this.pnlDerecho.TabIndex = 1;
            // 
            // pnlMétricas
            // 
            this.pnlMétricas.ColumnCount = 1;
            this.pnlMétricas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMétricas.Controls.Add(this.cardTarjeta, 0, 0);
            this.pnlMétricas.Controls.Add(this.cardEntradas, 0, 1);
            this.pnlMétricas.Controls.Add(this.cardSalidas, 0, 2);
            this.pnlMétricas.Controls.Add(this.cardTotal, 0, 3);
            this.pnlMétricas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMétricas.Location = new System.Drawing.Point(10, 0);
            this.pnlMétricas.Name = "pnlMétricas";
            this.pnlMétricas.RowCount = 4;
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlMétricas.Size = new System.Drawing.Size(387, 480);
            this.pnlMétricas.TabIndex = 0;
            // 
            // cardTarjeta
            // 
            this.cardTarjeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.cardTarjeta.Controls.Add(this.lblCredito);
            this.cardTarjeta.Controls.Add(this.lblTitTarjeta);
            this.cardTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTarjeta.Location = new System.Drawing.Point(0, 0);
            this.cardTarjeta.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardTarjeta.Name = "cardTarjeta";
            this.cardTarjeta.Padding = new System.Windows.Forms.Padding(15);
            this.cardTarjeta.Size = new System.Drawing.Size(387, 108);
            this.cardTarjeta.TabIndex = 0;
            // 
            // lblCredito
            // 
            this.lblCredito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCredito.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCredito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblCredito.Location = new System.Drawing.Point(15, 38);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(357, 55);
            this.lblCredito.TabIndex = 1;
            this.lblCredito.Text = "$0.00";
            this.lblCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitTarjeta
            // 
            this.lblTitTarjeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitTarjeta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitTarjeta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitTarjeta.Location = new System.Drawing.Point(15, 15);
            this.lblTitTarjeta.Name = "lblTitTarjeta";
            this.lblTitTarjeta.Size = new System.Drawing.Size(357, 23);
            this.lblTitTarjeta.TabIndex = 0;
            this.lblTitTarjeta.Text = "💳 VENTAS TARJETA";
            // 
            // cardEntradas
            // 
            this.cardEntradas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.cardEntradas.Controls.Add(this.lblEntrada);
            this.cardEntradas.Controls.Add(this.lblTitEntradas);
            this.cardEntradas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardEntradas.Location = new System.Drawing.Point(0, 120);
            this.cardEntradas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardEntradas.Name = "cardEntradas";
            this.cardEntradas.Padding = new System.Windows.Forms.Padding(15);
            this.cardEntradas.Size = new System.Drawing.Size(387, 108);
            this.cardEntradas.TabIndex = 1;
            // 
            // lblEntrada
            // 
            this.lblEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntrada.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblEntrada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblEntrada.Location = new System.Drawing.Point(15, 38);
            this.lblEntrada.Name = "lblEntrada";
            this.lblEntrada.Size = new System.Drawing.Size(357, 55);
            this.lblEntrada.TabIndex = 1;
            this.lblEntrada.Text = "$0.00";
            this.lblEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitEntradas
            // 
            this.lblTitEntradas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitEntradas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitEntradas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTitEntradas.Location = new System.Drawing.Point(15, 15);
            this.lblTitEntradas.Name = "lblTitEntradas";
            this.lblTitEntradas.Size = new System.Drawing.Size(357, 23);
            this.lblTitEntradas.TabIndex = 0;
            this.lblTitEntradas.Text = "📥 ENTRADAS EFECTIVO";
            // 
            // cardSalidas
            // 
            this.cardSalidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.cardSalidas.Controls.Add(this.lblSalida);
            this.cardSalidas.Controls.Add(this.lblTitSalidas);
            this.cardSalidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardSalidas.Location = new System.Drawing.Point(0, 240);
            this.cardSalidas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardSalidas.Name = "cardSalidas";
            this.cardSalidas.Padding = new System.Windows.Forms.Padding(15);
            this.cardSalidas.Size = new System.Drawing.Size(387, 108);
            this.cardSalidas.TabIndex = 2;
            // 
            // lblSalida
            // 
            this.lblSalida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalida.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblSalida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblSalida.Location = new System.Drawing.Point(15, 38);
            this.lblSalida.Name = "lblSalida";
            this.lblSalida.Size = new System.Drawing.Size(357, 55);
            this.lblSalida.TabIndex = 1;
            this.lblSalida.Text = "$0.00";
            this.lblSalida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitSalidas
            // 
            this.lblTitSalidas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitSalidas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitSalidas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitSalidas.Location = new System.Drawing.Point(15, 15);
            this.lblTitSalidas.Name = "lblTitSalidas";
            this.lblTitSalidas.Size = new System.Drawing.Size(357, 23);
            this.lblTitSalidas.TabIndex = 0;
            this.lblTitSalidas.Text = "📤 SALIDAS CAJA";
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.cardTotal.Controls.Add(this.lblCorte);
            this.cardTotal.Controls.Add(this.lblTitTotal);
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Location = new System.Drawing.Point(0, 360);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Padding = new System.Windows.Forms.Padding(15);
            this.cardTotal.Size = new System.Drawing.Size(387, 108);
            this.cardTotal.TabIndex = 3;
            // 
            // lblCorte
            // 
            this.lblCorte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCorte.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblCorte.Location = new System.Drawing.Point(15, 38);
            this.lblCorte.Name = "lblCorte";
            this.lblCorte.Size = new System.Drawing.Size(357, 55);
            this.lblCorte.TabIndex = 1;
            this.lblCorte.Text = "$0.00";
            this.lblCorte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitTotal
            // 
            this.lblTitTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblTitTotal.Location = new System.Drawing.Point(15, 15);
            this.lblTitTotal.Name = "lblTitTotal";
            this.lblTitTotal.Size = new System.Drawing.Size(357, 23);
            this.lblTitTotal.TabIndex = 0;
            this.lblTitTotal.Text = "💰 EFECTIVO NETO EN CAJA";
            // 
            // btnCorte
            // 
            this.btnCorte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCorte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCorte.FlatAppearance.BorderSize = 0;
            this.btnCorte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorte.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnCorte.ForeColor = System.Drawing.Color.White;
            this.btnCorte.Location = new System.Drawing.Point(10, 580);
            this.btnCorte.Name = "btnCorte";
            this.btnCorte.Size = new System.Drawing.Size(387, 80);
            this.btnCorte.TabIndex = 1;
            this.btnCorte.Text = "🔒 REALIZAR CORTE";
            this.btnCorte.UseVisualStyleBackColor = false;
            this.btnCorte.Click += new System.EventHandler(this.btnCorte_Click);
            // 
            // frmCorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1084, 700);
            this.Controls.Add(this.pnlPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCorte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cierre de Caja y Balance de Turno";
            this.Load += new System.EventHandler(this.frmCorte_Load);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlIzquierdo.ResumeLayout(false);
            this.pnlIzquierdo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorte)).EndInit();
            this.pnlHeaderMeseros.ResumeLayout(false);
            this.pnlHeaderMeseros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeseros)).EndInit();
            this.pnlDerecho.ResumeLayout(false);
            this.pnlMétricas.ResumeLayout(false);
            this.cardTarjeta.ResumeLayout(false);
            this.cardEntradas.ResumeLayout(false);
            this.cardSalidas.ResumeLayout(false);
            this.cardTotal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlPrincipal;
        private System.Windows.Forms.TableLayoutPanel pnlIzquierdo;
        private System.Windows.Forms.Label lblTituloCorte;
        private System.Windows.Forms.DataGridView dgvCorte;
        private System.Windows.Forms.Panel pnlHeaderMeseros;
        private System.Windows.Forms.Label lblTituloMeseros;
        private System.Windows.Forms.Button btnDetalleMesero;
        private System.Windows.Forms.DataGridView dgvMeseros;
        private System.Windows.Forms.Panel pnlDerecho;
        private System.Windows.Forms.TableLayoutPanel pnlMétricas;
        private System.Windows.Forms.Panel cardTarjeta;
        private System.Windows.Forms.Label lblTitTarjeta;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Panel cardEntradas;
        private System.Windows.Forms.Label lblTitEntradas;
        private System.Windows.Forms.Label lblEntrada;
        private System.Windows.Forms.Panel cardSalidas;
        private System.Windows.Forms.Label lblTitSalidas;
        private System.Windows.Forms.Label lblSalida;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblTitTotal;
        private System.Windows.Forms.Label lblCorte;
        private System.Windows.Forms.Button btnCorte;
    }
}