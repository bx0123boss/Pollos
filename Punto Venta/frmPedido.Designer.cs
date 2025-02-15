namespace Punto_Venta
{
    partial class frmPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPedido));
            this.LblTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BtnEntregar = new System.Windows.Forms.Button();
            this.DgvPedidoprevio = new System.Windows.Forms.DataGridView();
            this.lblFolio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbCliente = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.flowBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvMesas = new System.Windows.Forms.DataGridView();
            this.lblMesero = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMesa = new System.Windows.Forms.Label();
            this.CmbMesa = new System.Windows.Forms.ComboBox();
            this.flpCategorias = new System.Windows.Forms.FlowLayoutPanel();
            this.flpInventario = new System.Windows.Forms.FlowLayoutPanel();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblNombre = new System.Windows.Forms.Label();
            this.LblDomicilio = new System.Windows.Forms.Label();
            this.LblReferencia = new System.Windows.Forms.Label();
            this.LblTelefono = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblColonia = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbMesa = new System.Windows.Forms.RadioButton();
            this.rbDomicilo = new System.Windows.Forms.RadioButton();
            this.rbRapido = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Aidi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Canti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comanda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idExtra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPedidoprevio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTotal
            // 
            this.LblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblTotal.AutoSize = true;
            this.LblTotal.BackColor = System.Drawing.Color.Black;
            this.LblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.LblTotal.ForeColor = System.Drawing.Color.White;
            this.LblTotal.Location = new System.Drawing.Point(185, 509);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(119, 46);
            this.LblTotal.TabIndex = 47;
            this.LblTotal.Text = "00.00";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 509);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 46);
            this.label11.TabIndex = 45;
            this.label11.Text = "TOTAL: $";
            // 
            // BtnEntregar
            // 
            this.BtnEntregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEntregar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnEntregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.BtnEntregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnEntregar.Location = new System.Drawing.Point(905, 511);
            this.BtnEntregar.Name = "BtnEntregar";
            this.BtnEntregar.Size = new System.Drawing.Size(122, 60);
            this.BtnEntregar.TabIndex = 44;
            this.BtnEntregar.Text = "ENTREGAR";
            this.BtnEntregar.UseVisualStyleBackColor = false;
            this.BtnEntregar.Click += new System.EventHandler(this.BtnEntregar_Click);
            // 
            // DgvPedidoprevio
            // 
            this.DgvPedidoprevio.AllowUserToAddRows = false;
            this.DgvPedidoprevio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPedidoprevio.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.DgvPedidoprevio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DgvPedidoprevio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPedidoprevio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Aidi,
            this.Canti,
            this.Prod,
            this.Pre,
            this.Tot,
            this.Comentario,
            this.Comanda,
            this.idExtra});
            this.DgvPedidoprevio.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DgvPedidoprevio.Location = new System.Drawing.Point(613, 266);
            this.DgvPedidoprevio.Name = "DgvPedidoprevio";
            this.DgvPedidoprevio.ReadOnly = true;
            this.DgvPedidoprevio.Size = new System.Drawing.Size(414, 236);
            this.DgvPedidoprevio.TabIndex = 37;
            this.DgvPedidoprevio.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPedidoprevio_CellEndEdit);
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.BackColor = System.Drawing.Color.White;
            this.lblFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblFolio.ForeColor = System.Drawing.Color.Black;
            this.lblFolio.Location = new System.Drawing.Point(90, 196);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(64, 25);
            this.lblFolio.TabIndex = 56;
            this.lblFolio.Text = "label5";
            this.lblFolio.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 25);
            this.label6.TabIndex = 55;
            this.label6.Text = "Folio:";
            this.label6.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.White;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.checkBox1.Location = new System.Drawing.Point(52, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(228, 26);
            this.checkBox1.TabIndex = 63;
            this.checkBox1.Text = "Agregar a folio existente:";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.ForeColor = System.Drawing.Color.White;
            this.checkBox2.Location = new System.Drawing.Point(12, 7);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(105, 28);
            this.checkBox2.TabIndex = 66;
            this.checkBox2.Text = "Botones";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Visible = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(830, 511);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 60);
            this.button2.TabIndex = 59;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbCliente
            // 
            this.cbCliente.AutoSize = true;
            this.cbCliente.BackColor = System.Drawing.Color.Transparent;
            this.cbCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCliente.ForeColor = System.Drawing.Color.White;
            this.cbCliente.Location = new System.Drawing.Point(12, 232);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(152, 28);
            this.cbCliente.TabIndex = 70;
            this.cbCliente.Text = "Datos Cliente";
            this.cbCliente.UseVisualStyleBackColor = false;
            this.cbCliente.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(12, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 25);
            this.label12.TabIndex = 73;
            this.label12.Text = "Mesa:";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // flowBotones
            // 
            this.flowBotones.AutoScroll = true;
            this.flowBotones.Location = new System.Drawing.Point(3, 3);
            this.flowBotones.Name = "flowBotones";
            this.flowBotones.Size = new System.Drawing.Size(358, 139);
            this.flowBotones.TabIndex = 74;
            // 
            // dgvMesas
            // 
            this.dgvMesas.AllowUserToAddRows = false;
            this.dgvMesas.AllowUserToDeleteRows = false;
            this.dgvMesas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMesas.Location = new System.Drawing.Point(399, 92);
            this.dgvMesas.Name = "dgvMesas";
            this.dgvMesas.ReadOnly = true;
            this.dgvMesas.Size = new System.Drawing.Size(42, 20);
            this.dgvMesas.TabIndex = 75;
            this.dgvMesas.Visible = false;
            // 
            // lblMesero
            // 
            this.lblMesero.AutoSize = true;
            this.lblMesero.BackColor = System.Drawing.Color.White;
            this.lblMesero.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblMesero.ForeColor = System.Drawing.Color.Black;
            this.lblMesero.Location = new System.Drawing.Point(180, 196);
            this.lblMesero.Name = "lblMesero";
            this.lblMesero.Size = new System.Drawing.Size(0, 25);
            this.lblMesero.TabIndex = 77;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(180, 172);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 25);
            this.label15.TabIndex = 76;
            this.label15.Text = "Mesero:";
            // 
            // txtMesa
            // 
            this.txtMesa.AutoSize = true;
            this.txtMesa.BackColor = System.Drawing.Color.White;
            this.txtMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMesa.ForeColor = System.Drawing.Color.Black;
            this.txtMesa.Location = new System.Drawing.Point(31, 174);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.Size = new System.Drawing.Size(20, 24);
            this.txtMesa.TabIndex = 78;
            this.txtMesa.Text = "0";
            this.txtMesa.Visible = false;
            // 
            // CmbMesa
            // 
            this.CmbMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMesa.Enabled = false;
            this.CmbMesa.FormattingEnabled = true;
            this.CmbMesa.Location = new System.Drawing.Point(85, 174);
            this.CmbMesa.Name = "CmbMesa";
            this.CmbMesa.Size = new System.Drawing.Size(89, 21);
            this.CmbMesa.TabIndex = 79;
            // 
            // flpCategorias
            // 
            this.flpCategorias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCategorias.AutoScroll = true;
            this.flpCategorias.BackColor = System.Drawing.Color.Black;
            this.flpCategorias.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.flpCategorias.Location = new System.Drawing.Point(384, 8);
            this.flpCategorias.Name = "flpCategorias";
            this.flpCategorias.Size = new System.Drawing.Size(645, 247);
            this.flpCategorias.TabIndex = 1;
            // 
            // flpInventario
            // 
            this.flpInventario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flpInventario.AutoScroll = true;
            this.flpInventario.BackColor = System.Drawing.Color.Black;
            this.flpInventario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.flpInventario.Location = new System.Drawing.Point(12, 266);
            this.flpInventario.Name = "flpInventario";
            this.flpInventario.Size = new System.Drawing.Size(596, 237);
            this.flpInventario.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // Cant
            // 
            this.Cant.FillWeight = 25.84933F;
            this.Cant.HeaderText = "Cant";
            this.Cant.Name = "Cant";
            this.Cant.Width = 35;
            // 
            // Producto
            // 
            this.Producto.FillWeight = 105.3293F;
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.Width = 143;
            // 
            // Precio
            // 
            this.Precio.FillWeight = 45.4126F;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 61;
            // 
            // colorDialog1
            // 
            this.colorDialog1.SolidColorOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(38, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 55;
            this.label3.Text = "Nombre:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(20, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 56;
            this.label7.Text = "Domicilio:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "Referencia:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(24, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 58;
            this.label5.Text = "Teléfono:";
            // 
            // LblNombre
            // 
            this.LblNombre.AutoSize = true;
            this.LblNombre.BackColor = System.Drawing.Color.Black;
            this.LblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombre.ForeColor = System.Drawing.Color.White;
            this.LblNombre.Location = new System.Drawing.Point(126, 22);
            this.LblNombre.Name = "LblNombre";
            this.LblNombre.Size = new System.Drawing.Size(91, 20);
            this.LblNombre.TabIndex = 59;
            this.LblNombre.Text = "LblNombre";
            this.LblNombre.Visible = false;
            // 
            // LblDomicilio
            // 
            this.LblDomicilio.AutoSize = true;
            this.LblDomicilio.BackColor = System.Drawing.Color.Black;
            this.LblDomicilio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDomicilio.ForeColor = System.Drawing.Color.White;
            this.LblDomicilio.Location = new System.Drawing.Point(126, 42);
            this.LblDomicilio.Name = "LblDomicilio";
            this.LblDomicilio.Size = new System.Drawing.Size(102, 20);
            this.LblDomicilio.TabIndex = 60;
            this.LblDomicilio.Text = "LblDomicilio";
            this.LblDomicilio.Visible = false;
            // 
            // LblReferencia
            // 
            this.LblReferencia.AutoSize = true;
            this.LblReferencia.BackColor = System.Drawing.Color.Black;
            this.LblReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblReferencia.ForeColor = System.Drawing.Color.White;
            this.LblReferencia.Location = new System.Drawing.Point(126, 62);
            this.LblReferencia.Name = "LblReferencia";
            this.LblReferencia.Size = new System.Drawing.Size(113, 20);
            this.LblReferencia.TabIndex = 61;
            this.LblReferencia.Text = "LblReferencia";
            this.LblReferencia.Visible = false;
            // 
            // LblTelefono
            // 
            this.LblTelefono.AutoSize = true;
            this.LblTelefono.BackColor = System.Drawing.Color.Black;
            this.LblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTelefono.ForeColor = System.Drawing.Color.White;
            this.LblTelefono.Location = new System.Drawing.Point(126, 2);
            this.LblTelefono.Name = "LblTelefono";
            this.LblTelefono.Size = new System.Drawing.Size(96, 20);
            this.LblTelefono.TabIndex = 62;
            this.LblTelefono.Text = "LblTelefono";
            this.LblTelefono.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(36, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 64;
            this.label13.Text = "Colonia:";
            // 
            // lblColonia
            // 
            this.lblColonia.AutoSize = true;
            this.lblColonia.BackColor = System.Drawing.Color.Black;
            this.lblColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColonia.ForeColor = System.Drawing.Color.White;
            this.lblColonia.Location = new System.Drawing.Point(129, 82);
            this.lblColonia.Name = "lblColonia";
            this.lblColonia.Size = new System.Drawing.Size(18, 20);
            this.lblColonia.TabIndex = 65;
            this.lblColonia.Text = "0";
            this.lblColonia.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 166);
            this.tabControl1.TabIndex = 82;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.flowBotones);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mesas:";
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.lblColonia);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.LblTelefono);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.LblReferencia);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.LblDomicilio);
            this.tabPage2.Controls.Add(this.LblNombre);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 140);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "A domicilio";
            this.tabPage2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPage2_MouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(362, 140);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Llevar";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Font = new System.Drawing.Font("Sylfaen", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(3, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(222, 27);
            this.label14.TabIndex = 47;
            this.label14.Text = "(Se cobra de inmediato)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 33);
            this.label1.TabIndex = 46;
            this.label1.Text = "PEDIDO PARA LLEVAR";
            // 
            // rbMesa
            // 
            this.rbMesa.AutoSize = true;
            this.rbMesa.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.rbMesa.ForeColor = System.Drawing.SystemColors.Control;
            this.rbMesa.Location = new System.Drawing.Point(255, 24);
            this.rbMesa.Name = "rbMesa";
            this.rbMesa.Size = new System.Drawing.Size(88, 29);
            this.rbMesa.TabIndex = 27;
            this.rbMesa.TabStop = true;
            this.rbMesa.Text = "MESA";
            this.rbMesa.UseVisualStyleBackColor = false;
            this.rbMesa.CheckedChanged += new System.EventHandler(this.rbMesa_CheckedChanged);
            // 
            // rbDomicilo
            // 
            this.rbDomicilo.AutoSize = true;
            this.rbDomicilo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbDomicilo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDomicilo.ForeColor = System.Drawing.SystemColors.Control;
            this.rbDomicilo.Location = new System.Drawing.Point(119, 23);
            this.rbDomicilo.Name = "rbDomicilo";
            this.rbDomicilo.Size = new System.Drawing.Size(134, 29);
            this.rbDomicilo.TabIndex = 26;
            this.rbDomicilo.Text = "DOMICILIO";
            this.rbDomicilo.UseVisualStyleBackColor = false;
            this.rbDomicilo.CheckedChanged += new System.EventHandler(this.rbDomicilo_CheckedChanged);
            // 
            // rbRapido
            // 
            this.rbRapido.AutoSize = true;
            this.rbRapido.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rbRapido.Checked = true;
            this.rbRapido.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.rbRapido.ForeColor = System.Drawing.SystemColors.Control;
            this.rbRapido.Location = new System.Drawing.Point(12, 24);
            this.rbRapido.Name = "rbRapido";
            this.rbRapido.Size = new System.Drawing.Size(105, 29);
            this.rbRapido.TabIndex = 25;
            this.rbRapido.TabStop = true;
            this.rbRapido.Text = "RAPIDO";
            this.rbRapido.UseVisualStyleBackColor = false;
            this.rbRapido.CheckedChanged += new System.EventHandler(this.rbRapido_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Controls.Add(this.rbRapido);
            this.groupBox1.Controls.Add(this.rbDomicilo);
            this.groupBox1.Controls.Add(this.rbMesa);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 73);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DESTINO VENTA";
            this.groupBox1.Visible = false;
            // 
            // Aidi
            // 
            this.Aidi.HeaderText = "Id";
            this.Aidi.Name = "Aidi";
            this.Aidi.ReadOnly = true;
            // 
            // Canti
            // 
            this.Canti.HeaderText = "Cant";
            this.Canti.Name = "Canti";
            this.Canti.ReadOnly = true;
            this.Canti.Width = 50;
            // 
            // Prod
            // 
            this.Prod.HeaderText = "Producto";
            this.Prod.Name = "Prod";
            this.Prod.ReadOnly = true;
            // 
            // Pre
            // 
            this.Pre.HeaderText = "Precio";
            this.Pre.Name = "Pre";
            this.Pre.ReadOnly = true;
            this.Pre.Width = 70;
            // 
            // Tot
            // 
            this.Tot.HeaderText = "Total";
            this.Tot.Name = "Tot";
            this.Tot.ReadOnly = true;
            this.Tot.Width = 70;
            // 
            // Comentario
            // 
            this.Comentario.HeaderText = "Comentario";
            this.Comentario.Name = "Comentario";
            this.Comentario.ReadOnly = true;
            this.Comentario.Width = 50;
            // 
            // Comanda
            // 
            this.Comanda.HeaderText = "Comanda";
            this.Comanda.Name = "Comanda";
            this.Comanda.ReadOnly = true;
            // 
            // idExtra
            // 
            this.idExtra.HeaderText = "idExtra";
            this.idExtra.Name = "idExtra";
            this.idExtra.ReadOnly = true;
            // 
            // frmPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1041, 578);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.flpCategorias);
            this.Controls.Add(this.CmbMesa);
            this.Controls.Add(this.txtMesa);
            this.Controls.Add(this.lblMesero);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbCliente);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.DgvPedidoprevio);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblFolio);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.BtnEntregar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dgvMesas);
            this.Controls.Add(this.flpInventario);
            this.Name = "frmPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPedido_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPedido_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DgvPedidoprevio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BtnEntregar;
        public System.Windows.Forms.DataGridView DgvPedidoprevio;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbCliente;
        private System.Windows.Forms.Label label12;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowBotones;
        private System.Windows.Forms.DataGridView dgvMesas;
        private System.Windows.Forms.Label lblMesero;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label txtMesa;
        public System.Windows.Forms.ComboBox CmbMesa;
        private System.Windows.Forms.FlowLayoutPanel flpCategorias;
        private System.Windows.Forms.FlowLayoutPanel flpInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblNombre;
        private System.Windows.Forms.Label LblDomicilio;
        private System.Windows.Forms.Label LblReferencia;
        private System.Windows.Forms.Label LblTelefono;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblColonia;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbMesa;
        private System.Windows.Forms.RadioButton rbDomicilo;
        private System.Windows.Forms.RadioButton rbRapido;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Aidi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Canti;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comanda;
        private System.Windows.Forms.DataGridViewTextBoxColumn idExtra;
    }
}