namespace Punto_Venta
{
    partial class frmCambio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCambio));
            this.lblID = new System.Windows.Forms.Label();
            this.lblID2 = new System.Windows.Forms.Label();
            this.dgvCambio = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDestino = new System.Windows.Forms.DataGridView();
            this.lblIndex1 = new System.Windows.Forms.Label();
            this.lblIndex2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDestino)).BeginInit();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(36, 541);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(35, 13);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "label1";
            this.lblID.Visible = false;
            // 
            // lblID2
            // 
            this.lblID2.AutoSize = true;
            this.lblID2.Location = new System.Drawing.Point(36, 554);
            this.lblID2.Name = "lblID2";
            this.lblID2.Size = new System.Drawing.Size(35, 13);
            this.lblID2.TabIndex = 1;
            this.lblID2.Text = "label1";
            this.lblID2.Visible = false;
            // 
            // dgvCambio
            // 
            this.dgvCambio.AllowUserToAddRows = false;
            this.dgvCambio.AllowUserToDeleteRows = false;
            this.dgvCambio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCambio.Location = new System.Drawing.Point(12, 146);
            this.dgvCambio.Name = "dgvCambio";
            this.dgvCambio.Size = new System.Drawing.Size(270, 367);
            this.dgvCambio.TabIndex = 2;
            this.dgvCambio.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(364, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cargando...";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvDestino
            // 
            this.dgvDestino.AllowUserToAddRows = false;
            this.dgvDestino.AllowUserToDeleteRows = false;
            this.dgvDestino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDestino.Location = new System.Drawing.Point(302, 146);
            this.dgvDestino.Name = "dgvDestino";
            this.dgvDestino.Size = new System.Drawing.Size(270, 367);
            this.dgvDestino.TabIndex = 5;
            this.dgvDestino.Visible = false;
            // 
            // lblIndex1
            // 
            this.lblIndex1.AutoSize = true;
            this.lblIndex1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndex1.Location = new System.Drawing.Point(388, 75);
            this.lblIndex1.Name = "lblIndex1";
            this.lblIndex1.Size = new System.Drawing.Size(84, 20);
            this.lblIndex1.TabIndex = 6;
            this.lblIndex1.Text = "lblIndex1";
            // 
            // lblIndex2
            // 
            this.lblIndex2.AutoSize = true;
            this.lblIndex2.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndex2.Location = new System.Drawing.Point(563, 75);
            this.lblIndex2.Name = "lblIndex2";
            this.lblIndex2.Size = new System.Drawing.Size(84, 20);
            this.lblIndex2.TabIndex = 7;
            this.lblIndex2.Text = "lblIndex2";
            // 
            // frmCambio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 276);
            this.Controls.Add(this.lblIndex2);
            this.Controls.Add(this.lblIndex1);
            this.Controls.Add(this.dgvDestino);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvCambio);
            this.Controls.Add(this.lblID2);
            this.Controls.Add(this.lblID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCambio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Mesa";
            this.Load += new System.EventHandler(this.frmCambio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDestino)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCambio;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label lblID;
        public System.Windows.Forms.Label lblID2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDestino;
        public System.Windows.Forms.Label lblIndex1;
        public System.Windows.Forms.Label lblIndex2;
    }
}