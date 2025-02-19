using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Punto_Venta
{
    public partial class frmPrincipal : Form
    {

        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd;
        public int id;
        public string usuario = "Administrador";
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmClientes comanda = new frmClientes();
            
            comanda.ShowDialog();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackgroundImage = Image.FromFile("C:\\Jaeger Soft\\w2.jpg");
                pictureBox1.Image = Image.FromFile("C:\\Jaeger Soft\\logo2.jpg");
            }
            catch
            {
            }
            try
            {
                pictureBox1.Image = Image.FromFile("C:\\Jaeger Soft\\logo2.png");
            }
            catch 
            {
            }
            conectar.Open();
            if (Conexion.lugar.Equals("TERRAZA"))
            {
                button9.Visible = false;
                button1.Visible = false;
                button7.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button11.Visible = false;
                button10.Visible = false;
                button14.Visible = false;
            }
            else if (lblUser.Text == "VENTAS")
            {
                button6.Visible = false;
                button11.Visible = false;
                button10.Visible = false;
                button14.Visible = false;
            }
            else if (lblUser.Text=="SUPERVISOR")
            {
                button6.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button14.Visible = false;
                button9.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmMesasOcupadas))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {
                
            }
            else
            {
                frmMesasOcupadas mesa = new frmMesasOcupadas();
                mesa.ShowDialog();
            } 
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estas seguro de salir?", "Alto!", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                
                e.Cancel = false;
                this.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                
            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmCambiarMesa))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmCambiarMesa mesa = new frmCambiarMesa();
                mesa.ShowDialog();
            } 
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmIngreso))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmIngreso mesa = new frmIngreso();
                mesa.usuario = usuario;
                mesa.ShowDialog();
            } 
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmEgresos))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmEgresos mesa = new frmEgresos();
                mesa.usuario = usuario;
                mesa.ShowDialog();
            } 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmInventario))
                {
                    frm.BringToFront();
                    abierto = true;
                }
            }
            if (abierto)
            {

            }
            else
            {
                if (lblUser.Text == "invitado")
                {
                    frmInventarioFisico fisico = new frmInventarioFisico();
                    fisico.invitado = false;
                    fisico.Show();
                }
                else
                {
                    frmInventario inventario = new frmInventario();
                    inventario.usuario = usuario;
                    inventario.Show();
                }
            } 
         
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmCorte corte = new frmCorte();
            corte.usuario = lblUser.Text;
            cmd = new OleDbCommand("select count(*) from temp;", conectar);
            int valor = int.Parse(cmd.ExecuteScalar().ToString());
            if (valor == 0)
            {

                corte.ShowDialog();
            }
            else
            {
                corte.ShowDialog();
                MessageBox.Show("AUN NO HA ACTUALIZADO EL INVENTARIO, FAVOR DE ACTUALIZAR", "ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmMesasOcupadas))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmMesasOcupadas mesa = new frmMesasOcupadas();
                mesa.ShowDialog();
            } 
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmActInventario))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmActInventario mesa = new frmActInventario();
                mesa.ShowDialog();
            } 
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmTipoDetallada))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (!abierto)
            {
                frmHistoCortes histo = new frmHistoCortes
                {
                    MinimizeBox = false
                };
                histo.Show();
            }
           
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmTipoDetallada))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmTipoDetallada det = new frmTipoDetallada
                {
                    MinimizeBox = false
                };
                det.usuario = lblUser.Text;
                
                det.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("http://192.168.0.15/projects/carta4.php"); 
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmPedido))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmPedido p = new frmPedido();
                p.Usuario = lblUser.Text;
                p.ShowDialog();
            } 
            

        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool abierto = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmUsuarios))
                {
                    abierto = true;
                    frm.BringToFront();
                }
            }
            if (abierto)
            {

            }
            else
            {
                frmUsuarios mesa = new frmUsuarios();
                mesa.Show();
            }             
        }


    }
}
