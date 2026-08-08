namespace Punto_Venta
{
    partial class frmDetalleCorte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalleCorte));
            this.pnlPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.pnlIzquierdo = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlGrids = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.dgvGeneral = new System.Windows.Forms.DataGridView();
            this.tabVentas = new System.Windows.Forms.TabPage();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.tabCancelaciones = new System.Windows.Forms.TabPage();
            this.dgvCancelaciones = new System.Windows.Forms.DataGridView();
            this.tabRetiros = new System.Windows.Forms.TabPage();
            this.dgvRetiros = new System.Windows.Forms.DataGridView();
            this.tabOtrosIngresos = new System.Windows.Forms.TabPage();
            this.dgvOtrosIngresos = new System.Windows.Forms.DataGridView();
            this.pnlHeaderMeseros = new System.Windows.Forms.Panel();
            this.lblTituloMeseros = new System.Windows.Forms.Label();
            this.dgvMeseros = new System.Windows.Forms.DataGridView();
            this.pnlDerecho = new System.Windows.Forms.Panel();
            this.pnlEncabezadoInfo = new System.Windows.Forms.Panel();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblTitFecha = new System.Windows.Forms.Label();
            this.pnlMétricas = new System.Windows.Forms.TableLayoutPanel();
            this.cardVentas = new System.Windows.Forms.Panel();
            this.lblVentasDetalle = new System.Windows.Forms.Label();
            this.lblTitVentas = new System.Windows.Forms.Label();
            this.cardCancelaciones = new System.Windows.Forms.Panel();
            this.lblTotalCancelaciones = new System.Windows.Forms.Label();
            this.lblCancDetalle = new System.Windows.Forms.Label();
            this.lblTitCancelaciones = new System.Windows.Forms.Label();
            this.cardRetiros = new System.Windows.Forms.Panel();
            this.lblRetiros = new System.Windows.Forms.Label();
            this.lblTitRetiros = new System.Windows.Forms.Label();
            this.cardOtrosIngresos = new System.Windows.Forms.Panel();
            this.lblTotalOtrosIngresos = new System.Windows.Forms.Label();
            this.lblOtrosIngresosDetalle = new System.Windows.Forms.Label();
            this.lblTitOtrosIngresos = new System.Windows.Forms.Label();
            this.cardPropina = new System.Windows.Forms.Panel();
            this.lblPropina = new System.Windows.Forms.Label();
            this.lblTitPropina = new System.Windows.Forms.Label();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblGranTotal = new System.Windows.Forms.Label();
            this.lblEfectivoEnCaja = new System.Windows.Forms.Label();
            this.lblTitTotal = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.lblMonto = new System.Windows.Forms.Label();
            this.pnlPrincipal.SuspendLayout();
            this.pnlIzquierdo.SuspendLayout();
            this.tabControlGrids.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).BeginInit();
            this.tabVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).BeginInit();
            this.tabCancelaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCancelaciones)).BeginInit();
            this.tabRetiros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).BeginInit();
            this.tabOtrosIngresos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtrosIngresos)).BeginInit();
            this.pnlHeaderMeseros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeseros)).BeginInit();
            this.pnlDerecho.SuspendLayout();
            this.pnlEncabezadoInfo.SuspendLayout();
            this.pnlMétricas.SuspendLayout();
            this.cardVentas.SuspendLayout();
            this.cardCancelaciones.SuspendLayout();
            this.cardRetiros.SuspendLayout();
            this.cardOtrosIngresos.SuspendLayout();
            this.cardPropina.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.ColumnCount = 2;
            this.pnlPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlPrincipal.Controls.Add(this.pnlIzquierdo, 0, 0);
            this.pnlPrincipal.Controls.Add(this.pnlDerecho, 1, 0);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Padding = new System.Windows.Forms.Padding(12);
            this.pnlPrincipal.RowCount = 1;
            this.pnlPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlPrincipal.Size = new System.Drawing.Size(1084, 720);
            this.pnlPrincipal.TabIndex = 0;
            // 
            // pnlIzquierdo
            // 
            this.pnlIzquierdo.ColumnCount = 1;
            this.pnlIzquierdo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlIzquierdo.Controls.Add(this.tabControlGrids, 0, 0);
            this.pnlIzquierdo.Controls.Add(this.pnlHeaderMeseros, 0, 1);
            this.pnlIzquierdo.Controls.Add(this.dgvMeseros, 0, 2);
            this.pnlIzquierdo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIzquierdo.Location = new System.Drawing.Point(15, 15);
            this.pnlIzquierdo.Name = "pnlIzquierdo";
            this.pnlIzquierdo.RowCount = 3;
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.pnlIzquierdo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlIzquierdo.Size = new System.Drawing.Size(630, 690);
            this.pnlIzquierdo.TabIndex = 0;
            // 
            // tabControlGrids
            // 
            this.tabControlGrids.Controls.Add(this.tabGeneral);
            this.tabControlGrids.Controls.Add(this.tabVentas);
            this.tabControlGrids.Controls.Add(this.tabCancelaciones);
            this.tabControlGrids.Controls.Add(this.tabRetiros);
            this.tabControlGrids.Controls.Add(this.tabOtrosIngresos);
            this.tabControlGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGrids.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.tabControlGrids.Location = new System.Drawing.Point(3, 3);
            this.tabControlGrids.Name = "tabControlGrids";
            this.tabControlGrids.SelectedIndex = 0;
            this.tabControlGrids.Size = new System.Drawing.Size(624, 387);
            this.tabControlGrids.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.dgvGeneral);
            this.tabGeneral.Location = new System.Drawing.Point(4, 26);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(616, 357);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Corte General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // dgvGeneral
            // 
            this.dgvGeneral.AllowUserToAddRows = false;
            this.dgvGeneral.AllowUserToDeleteRows = false;
            this.dgvGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGeneral.Location = new System.Drawing.Point(3, 3);
            this.dgvGeneral.Name = "dgvGeneral";
            this.dgvGeneral.ReadOnly = true;
            this.dgvGeneral.RowHeadersVisible = false;
            this.dgvGeneral.Size = new System.Drawing.Size(610, 351);
            this.dgvGeneral.TabIndex = 0;
            // 
            // tabVentas
            // 
            this.tabVentas.Controls.Add(this.dgvVentas);
            this.tabVentas.Location = new System.Drawing.Point(4, 26);
            this.tabVentas.Name = "tabVentas";
            this.tabVentas.Padding = new System.Windows.Forms.Padding(3);
            this.tabVentas.Size = new System.Drawing.Size(616, 357);
            this.tabVentas.TabIndex = 1;
            this.tabVentas.Text = "Ventas";
            this.tabVentas.UseVisualStyleBackColor = true;
            // 
            // dgvVentas
            // 
            this.dgvVentas.AllowUserToAddRows = false;
            this.dgvVentas.AllowUserToDeleteRows = false;
            this.dgvVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVentas.Location = new System.Drawing.Point(3, 3);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.RowHeadersVisible = false;
            this.dgvVentas.Size = new System.Drawing.Size(610, 351);
            this.dgvVentas.TabIndex = 0;
            // 
            // tabCancelaciones
            // 
            this.tabCancelaciones.Controls.Add(this.dgvCancelaciones);
            this.tabCancelaciones.Location = new System.Drawing.Point(4, 26);
            this.tabCancelaciones.Name = "tabCancelaciones";
            this.tabCancelaciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabCancelaciones.Size = new System.Drawing.Size(616, 357);
            this.tabCancelaciones.TabIndex = 2;
            this.tabCancelaciones.Text = "Cancelaciones";
            this.tabCancelaciones.UseVisualStyleBackColor = true;
            // 
            // dgvCancelaciones
            // 
            this.dgvCancelaciones.AllowUserToAddRows = false;
            this.dgvCancelaciones.AllowUserToDeleteRows = false;
            this.dgvCancelaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCancelaciones.Location = new System.Drawing.Point(3, 3);
            this.dgvCancelaciones.Name = "dgvCancelaciones";
            this.dgvCancelaciones.ReadOnly = true;
            this.dgvCancelaciones.RowHeadersVisible = false;
            this.dgvCancelaciones.Size = new System.Drawing.Size(610, 351);
            this.dgvCancelaciones.TabIndex = 0;
            // 
            // tabRetiros
            // 
            this.tabRetiros.Controls.Add(this.dgvRetiros);
            this.tabRetiros.Location = new System.Drawing.Point(4, 26);
            this.tabRetiros.Name = "tabRetiros";
            this.tabRetiros.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetiros.Size = new System.Drawing.Size(616, 357);
            this.tabRetiros.TabIndex = 3;
            this.tabRetiros.Text = "Retiros";
            this.tabRetiros.UseVisualStyleBackColor = true;
            // 
            // dgvRetiros
            // 
            this.dgvRetiros.AllowUserToAddRows = false;
            this.dgvRetiros.AllowUserToDeleteRows = false;
            this.dgvRetiros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRetiros.Location = new System.Drawing.Point(3, 3);
            this.dgvRetiros.Name = "dgvRetiros";
            this.dgvRetiros.ReadOnly = true;
            this.dgvRetiros.RowHeadersVisible = false;
            this.dgvRetiros.Size = new System.Drawing.Size(610, 351);
            this.dgvRetiros.TabIndex = 0;
            // 
            // tabOtrosIngresos
            // 
            this.tabOtrosIngresos.Controls.Add(this.dgvOtrosIngresos);
            this.tabOtrosIngresos.Location = new System.Drawing.Point(4, 26);
            this.tabOtrosIngresos.Name = "tabOtrosIngresos";
            this.tabOtrosIngresos.Padding = new System.Windows.Forms.Padding(3);
            this.tabOtrosIngresos.Size = new System.Drawing.Size(616, 357);
            this.tabOtrosIngresos.TabIndex = 4;
            this.tabOtrosIngresos.Text = "Depósitos / Apertura";
            this.tabOtrosIngresos.UseVisualStyleBackColor = true;
            // 
            // dgvOtrosIngresos
            // 
            this.dgvOtrosIngresos.AllowUserToAddRows = false;
            this.dgvOtrosIngresos.AllowUserToDeleteRows = false;
            this.dgvOtrosIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOtrosIngresos.Location = new System.Drawing.Point(3, 3);
            this.dgvOtrosIngresos.Name = "dgvOtrosIngresos";
            this.dgvOtrosIngresos.ReadOnly = true;
            this.dgvOtrosIngresos.RowHeadersVisible = false;
            this.dgvOtrosIngresos.Size = new System.Drawing.Size(610, 351);
            this.dgvOtrosIngresos.TabIndex = 0;
            // 
            // pnlHeaderMeseros
            // 
            this.pnlHeaderMeseros.Controls.Add(this.lblTituloMeseros);
            this.pnlHeaderMeseros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderMeseros.Location = new System.Drawing.Point(3, 396);
            this.pnlHeaderMeseros.Name = "pnlHeaderMeseros";
            this.pnlHeaderMeseros.Size = new System.Drawing.Size(624, 29);
            this.pnlHeaderMeseros.TabIndex = 1;
            // 
            // lblTituloMeseros
            // 
            this.lblTituloMeseros.AutoSize = true;
            this.lblTituloMeseros.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloMeseros.ForeColor = System.Drawing.Color.White;
            this.lblTituloMeseros.Location = new System.Drawing.Point(0, 4);
            this.lblTituloMeseros.Name = "lblTituloMeseros";
            this.lblTituloMeseros.Size = new System.Drawing.Size(141, 20);
            this.lblTituloMeseros.TabIndex = 0;
            this.lblTituloMeseros.Text = "Ventas por Mesero";
            // 
            // dgvMeseros
            // 
            this.dgvMeseros.AllowUserToAddRows = false;
            this.dgvMeseros.AllowUserToDeleteRows = false;
            this.dgvMeseros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMeseros.Location = new System.Drawing.Point(3, 431);
            this.dgvMeseros.Name = "dgvMeseros";
            this.dgvMeseros.ReadOnly = true;
            this.dgvMeseros.RowHeadersVisible = false;
            this.dgvMeseros.Size = new System.Drawing.Size(624, 256);
            this.dgvMeseros.TabIndex = 2;
            // 
            // pnlDerecho
            // 
            this.pnlDerecho.Controls.Add(this.pnlEncabezadoInfo);
            this.pnlDerecho.Controls.Add(this.pnlMétricas);
            this.pnlDerecho.Controls.Add(this.btnImprimir);
            this.pnlDerecho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDerecho.Location = new System.Drawing.Point(651, 15);
            this.pnlDerecho.Name = "pnlDerecho";
            this.pnlDerecho.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlDerecho.Size = new System.Drawing.Size(418, 690);
            this.pnlDerecho.TabIndex = 1;
            // 
            // pnlEncabezadoInfo
            // 
            this.pnlEncabezadoInfo.Controls.Add(this.lblFecha);
            this.pnlEncabezadoInfo.Controls.Add(this.lblTitFecha);
            this.pnlEncabezadoInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezadoInfo.Location = new System.Drawing.Point(10, 0);
            this.pnlEncabezadoInfo.Name = "pnlEncabezadoInfo";
            this.pnlEncabezadoInfo.Size = new System.Drawing.Size(408, 35);
            this.pnlEncabezadoInfo.TabIndex = 0;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.Location = new System.Drawing.Point(145, 8);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(133, 19);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "00/00/0000 00:00";
            // 
            // lblTitFecha
            // 
            this.lblTitFecha.AutoSize = true;
            this.lblTitFecha.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblTitFecha.ForeColor = System.Drawing.Color.LightGray;
            this.lblTitFecha.Location = new System.Drawing.Point(3, 8);
            this.lblTitFecha.Name = "lblTitFecha";
            this.lblTitFecha.Size = new System.Drawing.Size(142, 19);
            this.lblTitFecha.TabIndex = 0;
            this.lblTitFecha.Text = "FECHA DEL CORTE:";
            // 
            // pnlMétricas
            // 
            this.pnlMétricas.ColumnCount = 1;
            this.pnlMétricas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMétricas.Controls.Add(this.cardVentas, 0, 0);
            this.pnlMétricas.Controls.Add(this.cardCancelaciones, 0, 1);
            this.pnlMétricas.Controls.Add(this.cardRetiros, 0, 2);
            this.pnlMétricas.Controls.Add(this.cardOtrosIngresos, 0, 3);
            this.pnlMétricas.Controls.Add(this.cardPropina, 0, 4);
            this.pnlMétricas.Controls.Add(this.cardTotal, 0, 5);
            this.pnlMétricas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMétricas.Location = new System.Drawing.Point(10, 35);
            this.pnlMétricas.Name = "pnlMétricas";
            this.pnlMétricas.RowCount = 6;
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnlMétricas.Size = new System.Drawing.Size(408, 575);
            this.pnlMétricas.TabIndex = 1;
            // 
            // cardVentas
            // 
            this.cardVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.cardVentas.Controls.Add(this.lblVentasDetalle);
            this.cardVentas.Controls.Add(this.lblTitVentas);
            this.cardVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardVentas.Location = new System.Drawing.Point(0, 0);
            this.cardVentas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardVentas.Name = "cardVentas";
            this.cardVentas.Padding = new System.Windows.Forms.Padding(10);
            this.cardVentas.Size = new System.Drawing.Size(408, 89);
            this.cardVentas.TabIndex = 0;
            // 
            // lblVentasDetalle
            // 
            this.lblVentasDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVentasDetalle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVentasDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.lblVentasDetalle.Location = new System.Drawing.Point(10, 30);
            this.lblVentasDetalle.Name = "lblVentasDetalle";
            this.lblVentasDetalle.Size = new System.Drawing.Size(388, 49);
            this.lblVentasDetalle.TabIndex = 1;
            this.lblVentasDetalle.Text = "Efec: $0.00 | Tarj: $0.00 | Transf: $0.00";
            this.lblVentasDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitVentas
            // 
            this.lblTitVentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitVentas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitVentas.Location = new System.Drawing.Point(10, 10);
            this.lblTitVentas.Name = "lblTitVentas";
            this.lblTitVentas.Size = new System.Drawing.Size(388, 20);
            this.lblTitVentas.TabIndex = 0;
            this.lblTitVentas.Text = "💳 VENTAS (DESGLOSE)";
            // 
            // cardCancelaciones
            // 
            this.cardCancelaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.cardCancelaciones.Controls.Add(this.lblTotalCancelaciones);
            this.cardCancelaciones.Controls.Add(this.lblCancDetalle);
            this.cardCancelaciones.Controls.Add(this.lblTitCancelaciones);
            this.cardCancelaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCancelaciones.Location = new System.Drawing.Point(0, 95);
            this.cardCancelaciones.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardCancelaciones.Name = "cardCancelaciones";
            this.cardCancelaciones.Padding = new System.Windows.Forms.Padding(10);
            this.cardCancelaciones.Size = new System.Drawing.Size(408, 89);
            this.cardCancelaciones.TabIndex = 1;
            // 
            // lblTotalCancelaciones
            // 
            this.lblTotalCancelaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCancelaciones.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalCancelaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTotalCancelaciones.Location = new System.Drawing.Point(10, 50);
            this.lblTotalCancelaciones.Name = "lblTotalCancelaciones";
            this.lblTotalCancelaciones.Size = new System.Drawing.Size(388, 29);
            this.lblTotalCancelaciones.TabIndex = 2;
            this.lblTotalCancelaciones.Text = "Total Cancelaciones: $0.00";
            this.lblTotalCancelaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancDetalle
            // 
            this.lblCancDetalle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCancDetalle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCancDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.lblCancDetalle.Location = new System.Drawing.Point(10, 28);
            this.lblCancDetalle.Name = "lblCancDetalle";
            this.lblCancDetalle.Size = new System.Drawing.Size(388, 22);
            this.lblCancDetalle.TabIndex = 1;
            this.lblCancDetalle.Text = "Efec: $0.00 | Tarj: $0.00 | Transf: $0.00";
            this.lblCancDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitCancelaciones
            // 
            this.lblTitCancelaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitCancelaciones.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitCancelaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitCancelaciones.Location = new System.Drawing.Point(10, 10);
            this.lblTitCancelaciones.Name = "lblTitCancelaciones";
            this.lblTitCancelaciones.Size = new System.Drawing.Size(388, 18);
            this.lblTitCancelaciones.TabIndex = 0;
            this.lblTitCancelaciones.Text = "🚫 CANCELACIONES";
            // 
            // cardRetiros
            // 
            this.cardRetiros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.cardRetiros.Controls.Add(this.lblRetiros);
            this.cardRetiros.Controls.Add(this.lblTitRetiros);
            this.cardRetiros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRetiros.Location = new System.Drawing.Point(0, 190);
            this.cardRetiros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardRetiros.Name = "cardRetiros";
            this.cardRetiros.Padding = new System.Windows.Forms.Padding(10);
            this.cardRetiros.Size = new System.Drawing.Size(408, 89);
            this.cardRetiros.TabIndex = 2;
            // 
            // lblRetiros
            // 
            this.lblRetiros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRetiros.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRetiros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblRetiros.Location = new System.Drawing.Point(10, 30);
            this.lblRetiros.Name = "lblRetiros";
            this.lblRetiros.Size = new System.Drawing.Size(388, 49);
            this.lblRetiros.TabIndex = 1;
            this.lblRetiros.Text = "Total Retirado: $0.00";
            this.lblRetiros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitRetiros
            // 
            this.lblTitRetiros.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitRetiros.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitRetiros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblTitRetiros.Location = new System.Drawing.Point(10, 10);
            this.lblTitRetiros.Name = "lblTitRetiros";
            this.lblTitRetiros.Size = new System.Drawing.Size(388, 20);
            this.lblTitRetiros.TabIndex = 0;
            this.lblTitRetiros.Text = "📤 RETIROS DE EFECTIVO";
            // 
            // cardOtrosIngresos
            // 
            this.cardOtrosIngresos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.cardOtrosIngresos.Controls.Add(this.lblTotalOtrosIngresos);
            this.cardOtrosIngresos.Controls.Add(this.lblOtrosIngresosDetalle);
            this.cardOtrosIngresos.Controls.Add(this.lblTitOtrosIngresos);
            this.cardOtrosIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOtrosIngresos.Location = new System.Drawing.Point(0, 285);
            this.cardOtrosIngresos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardOtrosIngresos.Name = "cardOtrosIngresos";
            this.cardOtrosIngresos.Padding = new System.Windows.Forms.Padding(10);
            this.cardOtrosIngresos.Size = new System.Drawing.Size(408, 89);
            this.cardOtrosIngresos.TabIndex = 3;
            // 
            // lblTotalOtrosIngresos
            // 
            this.lblTotalOtrosIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOtrosIngresos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalOtrosIngresos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTotalOtrosIngresos.Location = new System.Drawing.Point(10, 50);
            this.lblTotalOtrosIngresos.Name = "lblTotalOtrosIngresos";
            this.lblTotalOtrosIngresos.Size = new System.Drawing.Size(388, 29);
            this.lblTotalOtrosIngresos.TabIndex = 2;
            this.lblTotalOtrosIngresos.Text = "Total Otros Ingresos: $0.00";
            this.lblTotalOtrosIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOtrosIngresosDetalle
            // 
            this.lblOtrosIngresosDetalle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOtrosIngresosDetalle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOtrosIngresosDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblOtrosIngresosDetalle.Location = new System.Drawing.Point(10, 28);
            this.lblOtrosIngresosDetalle.Name = "lblOtrosIngresosDetalle";
            this.lblOtrosIngresosDetalle.Size = new System.Drawing.Size(388, 22);
            this.lblOtrosIngresosDetalle.TabIndex = 1;
            this.lblOtrosIngresosDetalle.Text = "Apertura: $0.00 | Ingresos: $0.00";
            this.lblOtrosIngresosDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitOtrosIngresos
            // 
            this.lblTitOtrosIngresos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitOtrosIngresos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitOtrosIngresos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTitOtrosIngresos.Location = new System.Drawing.Point(10, 10);
            this.lblTitOtrosIngresos.Name = "lblTitOtrosIngresos";
            this.lblTitOtrosIngresos.Size = new System.Drawing.Size(388, 18);
            this.lblTitOtrosIngresos.TabIndex = 0;
            this.lblTitOtrosIngresos.Text = "📥 OTROS INGRESOS (APERTURA Y EFECTIVO)";
            // 
            // cardPropina
            // 
            this.cardPropina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(249)))), ((int)(((byte)(195)))));
            this.cardPropina.Controls.Add(this.lblPropina);
            this.cardPropina.Controls.Add(this.lblTitPropina);
            this.cardPropina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPropina.Location = new System.Drawing.Point(0, 380);
            this.cardPropina.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardPropina.Name = "cardPropina";
            this.cardPropina.Padding = new System.Windows.Forms.Padding(10);
            this.cardPropina.Size = new System.Drawing.Size(408, 89);
            this.cardPropina.TabIndex = 4;
            // 
            // lblPropina
            // 
            this.lblPropina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPropina.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPropina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(98)))), ((int)(((byte)(7)))));
            this.lblPropina.Location = new System.Drawing.Point(10, 30);
            this.lblPropina.Name = "lblPropina";
            this.lblPropina.Size = new System.Drawing.Size(388, 49);
            this.lblPropina.TabIndex = 1;
            this.lblPropina.Text = "Propinas: $0.00";
            this.lblPropina.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitPropina
            // 
            this.lblTitPropina.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitPropina.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitPropina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(77)))), ((int)(((byte)(14)))));
            this.lblTitPropina.Location = new System.Drawing.Point(10, 10);
            this.lblTitPropina.Name = "lblTitPropina";
            this.lblTitPropina.Size = new System.Drawing.Size(388, 20);
            this.lblTitPropina.TabIndex = 0;
            this.lblTitPropina.Text = "🪙 PROPINAS";
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.cardTotal.Controls.Add(this.lblGranTotal);
            this.cardTotal.Controls.Add(this.lblEfectivoEnCaja);
            this.cardTotal.Controls.Add(this.lblTitTotal);
            this.cardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotal.Location = new System.Drawing.Point(0, 475);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Padding = new System.Windows.Forms.Padding(10);
            this.cardTotal.Size = new System.Drawing.Size(408, 94);
            this.cardTotal.TabIndex = 5;
            // 
            // lblGranTotal
            // 
            this.lblGranTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGranTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblGranTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblGranTotal.Location = new System.Drawing.Point(10, 56);
            this.lblGranTotal.Name = "lblGranTotal";
            this.lblGranTotal.Size = new System.Drawing.Size(388, 28);
            this.lblGranTotal.TabIndex = 2;
            this.lblGranTotal.Text = "Total Venta General: $0.00";
            this.lblGranTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEfectivoEnCaja
            // 
            this.lblEfectivoEnCaja.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEfectivoEnCaja.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEfectivoEnCaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblEfectivoEnCaja.Location = new System.Drawing.Point(10, 28);
            this.lblEfectivoEnCaja.Name = "lblEfectivoEnCaja";
            this.lblEfectivoEnCaja.Size = new System.Drawing.Size(388, 28);
            this.lblEfectivoEnCaja.TabIndex = 1;
            this.lblEfectivoEnCaja.Text = "Efectivo Neto en Caja: $0.00";
            this.lblEfectivoEnCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitTotal
            // 
            this.lblTitTotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitTotal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblTitTotal.Location = new System.Drawing.Point(10, 10);
            this.lblTitTotal.Name = "lblTitTotal";
            this.lblTitTotal.Size = new System.Drawing.Size(388, 18);
            this.lblTitTotal.TabIndex = 0;
            this.lblTitTotal.Text = "💰 TOTALES GENERALES DEL CORTE";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(10, 620);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(408, 60);
            this.btnImprimir.TabIndex = 2;
            this.btnImprimir.Text = "🖨️ IMPRIMIR TICKET";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Visible = false;
            this.lblMonto.Location = new System.Drawing.Point(0, 0);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(0, 13);
            this.lblMonto.TabIndex = 3;
            // 
            // frmDetalleCorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1084, 720);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.pnlPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDetalleCorte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de Corte de Caja (Histórico)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDetalleCorte_Load);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlIzquierdo.ResumeLayout(false);
            this.tabControlGrids.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).EndInit();
            this.tabVentas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVentas)).EndInit();
            this.tabCancelaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCancelaciones)).EndInit();
            this.tabRetiros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetiros)).EndInit();
            this.tabOtrosIngresos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtrosIngresos)).EndInit();
            this.pnlHeaderMeseros.ResumeLayout(false);
            this.pnlHeaderMeseros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeseros)).EndInit();
            this.pnlDerecho.ResumeLayout(false);
            this.pnlEncabezadoInfo.ResumeLayout(false);
            this.pnlEncabezadoInfo.PerformLayout();
            this.pnlMétricas.ResumeLayout(false);
            this.cardVentas.ResumeLayout(false);
            this.cardCancelaciones.ResumeLayout(false);
            this.cardRetiros.ResumeLayout(false);
            this.cardOtrosIngresos.ResumeLayout(false);
            this.cardPropina.ResumeLayout(false);
            this.cardTotal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlPrincipal;
        private System.Windows.Forms.TableLayoutPanel pnlIzquierdo;
        private System.Windows.Forms.TabControl tabControlGrids;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.DataGridView dgvGeneral;
        private System.Windows.Forms.TabPage tabVentas;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.TabPage tabCancelaciones;
        private System.Windows.Forms.DataGridView dgvCancelaciones;
        private System.Windows.Forms.TabPage tabRetiros;
        private System.Windows.Forms.DataGridView dgvRetiros;
        private System.Windows.Forms.TabPage tabOtrosIngresos;
        private System.Windows.Forms.DataGridView dgvOtrosIngresos;
        private System.Windows.Forms.Panel pnlHeaderMeseros;
        private System.Windows.Forms.Label lblTituloMeseros;
        private System.Windows.Forms.DataGridView dgvMeseros;
        private System.Windows.Forms.Panel pnlDerecho;
        private System.Windows.Forms.Panel pnlEncabezadoInfo;
        private System.Windows.Forms.Label lblTitFecha;
        public System.Windows.Forms.Label lblFecha;
        public System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TableLayoutPanel pnlMétricas;
        private System.Windows.Forms.Panel cardVentas;
        private System.Windows.Forms.Label lblTitVentas;
        private System.Windows.Forms.Label lblVentasDetalle;
        private System.Windows.Forms.Panel cardCancelaciones;
        private System.Windows.Forms.Label lblTitCancelaciones;
        private System.Windows.Forms.Label lblCancDetalle;
        private System.Windows.Forms.Label lblTotalCancelaciones;
        private System.Windows.Forms.Panel cardRetiros;
        private System.Windows.Forms.Label lblTitRetiros;
        private System.Windows.Forms.Label lblRetiros;
        private System.Windows.Forms.Panel cardOtrosIngresos;
        private System.Windows.Forms.Label lblTitOtrosIngresos;
        private System.Windows.Forms.Label lblOtrosIngresosDetalle;
        private System.Windows.Forms.Label lblTotalOtrosIngresos;
        private System.Windows.Forms.Panel cardPropina;
        private System.Windows.Forms.Label lblTitPropina;
        private System.Windows.Forms.Label lblPropina;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblTitTotal;
        private System.Windows.Forms.Label lblEfectivoEnCaja;
        private System.Windows.Forms.Label lblGranTotal;
        private System.Windows.Forms.Button btnImprimir;
    }
}