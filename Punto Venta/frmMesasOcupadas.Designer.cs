namespace Punto_Venta
{
    partial class frmMesasOcupadas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMesasOcupadas));
            this.button200 = new System.Windows.Forms.Button();
            this.flowBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMesero = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button200
            // 
            this.button200.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button200.AutoEllipsis = true;
            this.button200.BackColor = System.Drawing.Color.SkyBlue;
            this.button200.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button200.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button200.Location = new System.Drawing.Point(804, 12);
            this.button200.Name = "button200";
            this.button200.Size = new System.Drawing.Size(134, 49);
            this.button200.TabIndex = 55;
            this.button200.Text = "MESA";
            this.button200.UseVisualStyleBackColor = false;
            this.button200.Visible = false;
            // 
            // flowBotones
            // 
            this.flowBotones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowBotones.AutoScroll = true;
            this.flowBotones.Location = new System.Drawing.Point(12, 12);
            this.flowBotones.Name = "flowBotones";
            this.flowBotones.Size = new System.Drawing.Size(786, 558);
            this.flowBotones.TabIndex = 57;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Orange;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(804, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 49);
            this.button1.TabIndex = 62;
            this.button1.Text = "DOMICILIO";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // lblMesero
            // 
            this.lblMesero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesero.AutoSize = true;
            this.lblMesero.BackColor = System.Drawing.Color.Transparent;
            this.lblMesero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesero.ForeColor = System.Drawing.Color.White;
            this.lblMesero.Location = new System.Drawing.Point(806, 137);
            this.lblMesero.Name = "lblMesero";
            this.lblMesero.Size = new System.Drawing.Size(0, 18);
            this.lblMesero.TabIndex = 60;
            // 
            // label37
            // 
            this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(804, 119);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 18);
            this.label37.TabIndex = 59;
            this.label37.Text = "Mesero:";
            // 
            // frmMesasOcupadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(950, 582);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMesero);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.flowBotones);
            this.Controls.Add(this.button200);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMesasOcupadas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesas Ocupadas";
            this.Load += new System.EventHandler(this.frmMesasOcupadas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button200;
        private System.Windows.Forms.FlowLayoutPanel flowBotones;
        public System.Windows.Forms.Label lblMesero;
        public System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button button1;
    }
}