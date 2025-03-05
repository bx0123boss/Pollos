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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPedido));
            this.LblTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BtnEntregar = new System.Windows.Forms.Button();
            this.DgvPedidoprevio = new System.Windows.Forms.DataGridView();
            this.Aidi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comanda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idExtra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.lblMesero = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
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
            this.lblMesa = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvPedidoprevio)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblTotal
            // 
            this.LblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblTotal.AutoSize = true;
            this.LblTotal.BackColor = System.Drawing.Color.Black;
            this.LblTotal.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.ForeColor = System.Drawing.Color.White;
            this.LblTotal.Location = new System.Drawing.Point(152, 552);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(86, 35);
            this.LblTotal.TabIndex = 47;
            this.LblTotal.Text = "00.00";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 550);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 35);
            this.label11.TabIndex = 45;
            this.label11.Text = "TOTAL: ";
            // 
            // BtnEntregar
            // 
            this.BtnEntregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEntregar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnEntregar.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEntregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnEntregar.Location = new System.Drawing.Point(995, 552);
            this.BtnEntregar.Name = "BtnEntregar";
            this.BtnEntregar.Size = new System.Drawing.Size(213, 60);
            this.BtnEntregar.TabIndex = 44;
            this.BtnEntregar.Text = "ENTREGAR";
            this.BtnEntregar.UseVisualStyleBackColor = false;
            this.BtnEntregar.Click += new System.EventHandler(this.BtnEntregar_Click);
            // 
            // DgvPedidoprevio
            // 
            this.DgvPedidoprevio.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.DgvPedidoprevio.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvPedidoprevio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvPedidoprevio.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.DgvPedidoprevio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvPedidoprevio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvPedidoprevio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvPedidoprevio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Aidi,
            this.Cantidad,
            this.Prod,
            this.Pre,
            this.Tot,
            this.btnEliminar,
            this.Comentario,
            this.Comanda,
            this.idExtra});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvPedidoprevio.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvPedidoprevio.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DgvPedidoprevio.Location = new System.Drawing.Point(612, 266);
            this.DgvPedidoprevio.Name = "DgvPedidoprevio";
            this.DgvPedidoprevio.ReadOnly = true;
            this.DgvPedidoprevio.Size = new System.Drawing.Size(596, 277);
            this.DgvPedidoprevio.TabIndex = 37;
            this.DgvPedidoprevio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPedidoprevio_CellClick);
            // 
            // Aidi
            // 
            this.Aidi.HeaderText = "Id";
            this.Aidi.Name = "Aidi";
            this.Aidi.ReadOnly = true;
            this.Aidi.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cant";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 50;
            // 
            // Prod
            // 
            this.Prod.HeaderText = "Producto";
            this.Prod.Name = "Prod";
            this.Prod.ReadOnly = true;
            this.Prod.Width = 125;
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
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "Eliminar";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEliminar.Width = 90;
            // 
            // Comentario
            // 
            this.Comentario.HeaderText = "Comentario";
            this.Comentario.Name = "Comentario";
            this.Comentario.ReadOnly = true;
            this.Comentario.Width = 150;
            // 
            // Comanda
            // 
            this.Comanda.HeaderText = "Comanda";
            this.Comanda.Name = "Comanda";
            this.Comanda.ReadOnly = true;
            this.Comanda.Visible = false;
            // 
            // idExtra
            // 
            this.idExtra.HeaderText = "idExtra";
            this.idExtra.Name = "idExtra";
            this.idExtra.ReadOnly = true;
            this.idExtra.Visible = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(920, 552);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 60);
            this.button2.TabIndex = 59;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // lblMesero
            // 
            this.lblMesero.AutoSize = true;
            this.lblMesero.BackColor = System.Drawing.Color.White;
            this.lblMesero.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblMesero.ForeColor = System.Drawing.Color.Black;
            this.lblMesero.Location = new System.Drawing.Point(88, 68);
            this.lblMesero.Name = "lblMesero";
            this.lblMesero.Size = new System.Drawing.Size(23, 25);
            this.lblMesero.TabIndex = 77;
            this.lblMesero.Text = "0";
            this.lblMesero.Click += new System.EventHandler(this.lblMesero_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(6, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(84, 25);
            this.label15.TabIndex = 76;
            this.label15.Text = "Mesero:";
            // 
            // CmbMesa
            // 
            this.CmbMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbMesa.FormattingEnabled = true;
            this.CmbMesa.Location = new System.Drawing.Point(79, 41);
            this.CmbMesa.Name = "CmbMesa";
            this.CmbMesa.Size = new System.Drawing.Size(199, 21);
            this.CmbMesa.TabIndex = 79;
            // 
            // flpCategorias
            // 
            this.flpCategorias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCategorias.AutoScroll = true;
            this.flpCategorias.BackColor = System.Drawing.Color.Black;
            this.flpCategorias.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.flpCategorias.Location = new System.Drawing.Point(388, 8);
            this.flpCategorias.Name = "flpCategorias";
            this.flpCategorias.Size = new System.Drawing.Size(826, 247);
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
            this.flpInventario.Size = new System.Drawing.Size(594, 278);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 166);
            this.tabControl1.TabIndex = 82;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.lblMesa);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.lblMesero);
            this.tabPage1.Controls.Add(this.CmbMesa);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mesas:";
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.BackColor = System.Drawing.Color.White;
            this.lblMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblMesa.ForeColor = System.Drawing.Color.Black;
            this.lblMesa.Location = new System.Drawing.Point(74, 38);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(23, 25);
            this.lblMesa.TabIndex = 82;
            this.lblMesa.Text = "0";
            this.lblMesa.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 25);
            this.label4.TabIndex = 81;
            this.label4.Text = "Mesa:";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.BackColor = System.Drawing.Color.Transparent;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox3.ForeColor = System.Drawing.Color.White;
            this.checkBox3.Location = new System.Drawing.Point(11, 7);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(145, 28);
            this.checkBox3.TabIndex = 80;
            this.checkBox3.Text = "Mesa Nueva";
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
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
            // frmPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1222, 619);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.flpCategorias);
            this.Controls.Add(this.DgvPedidoprevio);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.BtnEntregar);
            this.Controls.Add(this.flpInventario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido";
            this.Load += new System.EventHandler(this.frmPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvPedidoprevio)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BtnEntregar;
        public System.Windows.Forms.DataGridView DgvPedidoprevio;
        private System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label lblMesero;
        private System.Windows.Forms.Label label15;
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
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Aidi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Prod;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tot;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comanda;
        private System.Windows.Forms.DataGridViewTextBoxColumn idExtra;
    }
}