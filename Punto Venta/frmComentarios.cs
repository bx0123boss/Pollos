using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmComentarios : Form
    {
        public string Comentario { get; set; }
        public frmComentarios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comentario();
        }
        public void comentario()
        {
            if (txtPass.Text == "")
            {

            }
            else
            {
                Comentario = txtPass.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;                                
            }
        }

        private void frmComentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                comentario();
            }
        }
    }
}
