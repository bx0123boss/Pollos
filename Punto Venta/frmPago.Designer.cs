namespace Punto_Venta
{
    partial class frmPago
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.cmbPago = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.ColMetodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblRestante = new System.Windows.Forms.Label();
            this.txtRestante = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chkMixto = new System.Windows.Forms.CheckBox();

            // Paneles y Botonera Touch
            this.pnlTouch = new System.Windows.Forms.Panel();
            this.flpBilletes = new System.Windows.Forms.FlowLayoutPanel();
            this.btn50 = new System.Windows.Forms.Button();
            this.btn100 = new System.Windows.Forms.Button();
            this.btn200 = new System.Windows.Forms.Button();
            this.btn500 = new System.Windows.Forms.Button();
            this.btn1000 = new System.Windows.Forms.Button();
            this.btnExacto = new System.Windows.Forms.Button();
            this.ucNumpad1 = new Punto_Venta.UC_Numpad();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.pnlTouch.SuspendLayout();
            this.flpBilletes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "TOTAL:";
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtTotal.Location = new System.Drawing.Point(203, 12);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(182, 38);
            this.txtTotal.TabIndex = 7;
            // 
            // lblMetodo
            // 
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblMetodo.ForeColor = System.Drawing.Color.White;
            this.lblMetodo.Location = new System.Drawing.Point(12, 65);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(195, 26);
            this.lblMetodo.TabIndex = 19;
            this.lblMetodo.Text = "MÉTODO PAGO:";
            this.lblMetodo.Visible = false;
            // 
            // cmbPago
            // 
            this.cmbPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.cmbPago.FormattingEnabled = true;
            this.cmbPago.Location = new System.Drawing.Point(203, 63);
            this.cmbPago.Name = "cmbPago";
            this.cmbPago.Size = new System.Drawing.Size(319, 32);
            this.cmbPago.TabIndex = 0;
            this.cmbPago.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 29);
            this.label2.TabIndex = 10;
            this.label2.Text = "EFECTIVO:";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(203, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(167, 38);
            this.textBox2.TabIndex = 1;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.Location = new System.Drawing.Point(382, 109);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(140, 38);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "AGREGAR";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Visible = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            this.dgvPagos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMetodo,
            this.ColMonto});
            this.dgvPagos.Location = new System.Drawing.Point(17, 160);
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.RowHeadersVisible = false;
            this.dgvPagos.Size = new System.Drawing.Size(505, 140);
            this.dgvPagos.TabIndex = 22;
            this.dgvPagos.Visible = false;
            // 
            // ColMetodo
            // 
            this.ColMetodo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColMetodo.HeaderText = "MÉTODO DE PAGO";
            this.ColMetodo.Name = "ColMetodo";
            this.ColMetodo.ReadOnly = true;
            // 
            // ColMonto
            // 
            this.ColMonto.HeaderText = "MONTO";
            this.ColMonto.Name = "ColMonto";
            this.ColMonto.ReadOnly = true;
            this.ColMonto.Width = 150;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Location = new System.Drawing.Point(17, 315);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(150, 35);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar Pagos";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Visible = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblRestante
            // 
            this.lblRestante.AutoSize = true;
            this.lblRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblRestante.ForeColor = System.Drawing.Color.White;
            this.lblRestante.Location = new System.Drawing.Point(198, 318);
            this.lblRestante.Name = "lblRestante";
            this.lblRestante.Size = new System.Drawing.Size(143, 26);
            this.lblRestante.TabIndex = 24;
            this.lblRestante.Text = "RESTANTE:";
            this.lblRestante.Visible = false;
            // 
            // txtRestante
            // 
            this.txtRestante.Enabled = false;
            this.txtRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.txtRestante.Location = new System.Drawing.Point(346, 314);
            this.txtRestante.Name = "txtRestante";
            this.txtRestante.Size = new System.Drawing.Size(176, 35);
            this.txtRestante.TabIndex = 25;
            this.txtRestante.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(82, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "CAMBIO:";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.textBox3.Location = new System.Drawing.Point(203, 109);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(167, 35);
            this.textBox3.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(17, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 48);
            this.button1.TabIndex = 5;
            this.button1.Text = "Seguir Cobrando";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(322, 415);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(200, 48);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "ACEPTAR";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chkMixto
            // 
            this.chkMixto.AutoSize = true;
            this.chkMixto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.chkMixto.ForeColor = System.Drawing.Color.White;
            this.chkMixto.Location = new System.Drawing.Point(401, 23);
            this.chkMixto.Name = "chkMixto";
            this.chkMixto.Size = new System.Drawing.Size(112, 22);
            this.chkMixto.TabIndex = 6;
            this.chkMixto.Text = "Pago Mixto";
            this.chkMixto.UseVisualStyleBackColor = true;
            this.chkMixto.CheckedChanged += new System.EventHandler(this.chkMixto_CheckedChanged);
            // 
            // pnlTouch
            // 
            this.pnlTouch.Controls.Add(this.ucNumpad1);
            this.pnlTouch.Controls.Add(this.flpBilletes);
            this.pnlTouch.Location = new System.Drawing.Point(540, 12);
            this.pnlTouch.Name = "pnlTouch";
            this.pnlTouch.Size = new System.Drawing.Size(430, 450);
            this.pnlTouch.TabIndex = 26;
            // 
            // flpBilletes
            // 
            this.flpBilletes.Controls.Add(this.btn50);
            this.flpBilletes.Controls.Add(this.btn100);
            this.flpBilletes.Controls.Add(this.btn200);
            this.flpBilletes.Controls.Add(this.btn500);
            this.flpBilletes.Controls.Add(this.btn1000);
            this.flpBilletes.Controls.Add(this.btnExacto);
            this.flpBilletes.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpBilletes.Location = new System.Drawing.Point(0, 0);
            this.flpBilletes.Name = "flpBilletes";
            this.flpBilletes.Size = new System.Drawing.Size(430, 130);
            this.flpBilletes.TabIndex = 0;
            // 
            // btn50
            // 
            this.btn50.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn50.Location = new System.Drawing.Point(3, 3);
            this.btn50.Name = "btn50";
            this.btn50.Size = new System.Drawing.Size(135, 55);
            this.btn50.TabIndex = 0;
            this.btn50.Text = "$50";
            this.btn50.UseVisualStyleBackColor = true;
            this.btn50.Click += new System.EventHandler(this.btnBillete_Click);
            // 
            // btn100
            // 
            this.btn100.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn100.Location = new System.Drawing.Point(144, 3);
            this.btn100.Name = "btn100";
            this.btn100.Size = new System.Drawing.Size(135, 55);
            this.btn100.TabIndex = 1;
            this.btn100.Text = "$100";
            this.btn100.UseVisualStyleBackColor = true;
            this.btn100.Click += new System.EventHandler(this.btnBillete_Click);
            // 
            // btn200
            // 
            this.btn200.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn200.Location = new System.Drawing.Point(285, 3);
            this.btn200.Name = "btn200";
            this.btn200.Size = new System.Drawing.Size(135, 55);
            this.btn200.TabIndex = 2;
            this.btn200.Text = "$200";
            this.btn200.UseVisualStyleBackColor = true;
            this.btn200.Click += new System.EventHandler(this.btnBillete_Click);
            // 
            // btn500
            // 
            this.btn500.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn500.Location = new System.Drawing.Point(3, 64);
            this.btn500.Name = "btn500";
            this.btn500.Size = new System.Drawing.Size(135, 55);
            this.btn500.TabIndex = 3;
            this.btn500.Text = "$500";
            this.btn500.UseVisualStyleBackColor = true;
            this.btn500.Click += new System.EventHandler(this.btnBillete_Click);
            // 
            // btn1000
            // 
            this.btn1000.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn1000.Location = new System.Drawing.Point(144, 64);
            this.btn1000.Name = "btn1000";
            this.btn1000.Size = new System.Drawing.Size(135, 55);
            this.btn1000.TabIndex = 4;
            this.btn1000.Text = "$1000";
            this.btn1000.UseVisualStyleBackColor = true;
            this.btn1000.Click += new System.EventHandler(this.btnBillete_Click);
            // 
            // btnExacto
            // 
            this.btnExacto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnExacto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExacto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.btnExacto.Location = new System.Drawing.Point(285, 64);
            this.btnExacto.Name = "btnExacto";
            this.btnExacto.Size = new System.Drawing.Size(135, 55);
            this.btnExacto.TabIndex = 5;
            this.btnExacto.Text = "PAGO EXACTO";
            this.btnExacto.UseVisualStyleBackColor = false;
            this.btnExacto.Click += new System.EventHandler(this.btnExacto_Click);
            // 
            // ucNumpad1
            // 
            this.ucNumpad1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNumpad1.Location = new System.Drawing.Point(0, 130);
            this.ucNumpad1.Name = "ucNumpad1";
            this.ucNumpad1.Size = new System.Drawing.Size(430, 320);
            this.ucNumpad1.TabIndex = 1;
            this.ucNumpad1.TargetTextBox = null;
            // 
            // frmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(980, 480);
            this.Controls.Add(this.pnlTouch);
            this.Controls.Add(this.chkMixto);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtRestante);
            this.Controls.Add(this.lblRestante);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvPagos);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.cmbPago);
            this.Controls.Add(this.lblMetodo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pagos";
            this.Load += new System.EventHandler(this.frmPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.pnlTouch.ResumeLayout(false);
            this.flpBilletes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.ComboBox cmbPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvPagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMonto;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblRestante;
        private System.Windows.Forms.TextBox txtRestante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox chkMixto;

        // Controles Touch
        private System.Windows.Forms.Panel pnlTouch;
        private System.Windows.Forms.FlowLayoutPanel flpBilletes;
        private System.Windows.Forms.Button btn50;
        private System.Windows.Forms.Button btn100;
        private System.Windows.Forms.Button btn200;
        private System.Windows.Forms.Button btn500;
        private System.Windows.Forms.Button btn1000;
        private System.Windows.Forms.Button btnExacto;
        private UC_Numpad ucNumpad1;
    }
}