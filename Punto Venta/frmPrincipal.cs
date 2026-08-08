using JaegerSoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPrincipal : Form
    {
        public int id;
        bool IsServidorActivo = false;
        private Process _procesoWeb;
        public string NombreUsuario = "";
        public string idUsuario = "";
        public string usuario = "Administrador";
        public frmPrincipal()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            IsServidorActivo = ArrancarServidorWeb();
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
                string bgPath = @"C:\Jaeger Soft\w2.jpg";
                if (File.Exists(bgPath)) { this.BackgroundImage = Image.FromFile(bgPath); }

                string logoPath = @"C:\Jaeger Soft\logo.png";
                if (File.Exists(logoPath)) { pictureBox1.Image = Image.FromFile(logoPath); }
            }
            catch (Exception) { }
            button4.Visible = Sesion.TienePermiso("MOD_ENTRADAS");
            button5.Visible = Sesion.TienePermiso("MOD_SALIDAS");
            button6.Visible = Sesion.TienePermiso("INVENTARIO");
            button7.Visible = Sesion.TienePermiso("MOD_CORTES");
            button10.Visible = Sesion.TienePermiso("HISTORIAL_CORTES");
            button11.Visible = Sesion.TienePermiso("MOD_REPORTES");
            button14.Visible = Sesion.TienePermiso("MOD_USUARIOS");
            button3.Visible = Sesion.TienePermiso("MOD_CONFIGURACION");
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
            if (!abierto)
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
                DetenerServidorWeb();
                e.Cancel = false;
                this.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                
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
            if (!abierto)
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
            if (!abierto)
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
            if (!abierto)
            {
                frmInventario inventario = new frmInventario();
                inventario.usuario = usuario;
                inventario.ShowDialog();
            }
         
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmCorte corte = new frmCorte();
            corte.usuario = lblUser.Text;
            corte.ShowDialog();
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
            if (!abierto)
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
                histo.ShowDialog();
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
            if (!abierto)
            {
                frmTipoDetallada det = new frmTipoDetallada
                {
                    MinimizeBox = false
                };
                det.usuario = lblUser.Text;

                det.ShowDialog();
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
            if (!abierto)
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
            if (!abierto)
            {
                frmUsuarios mesa = new frmUsuarios();
                mesa.ShowDialog();
            }        
        }


        private bool ArrancarServidorWeb()
        {
            try
            {
                string rutaWebExe = @"C:\Jaeger Soft\ModuloWebFastFood\FastFoodWeb.exe";
                if (!File.Exists(rutaWebExe))
                {
                    MessageBox.Show("Error iniciando servidor web");
                    return false;
                }

                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = rutaWebExe;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.CreateNoWindow = true;
                info.UseShellExecute = false;
                info.WorkingDirectory = Path.GetDirectoryName(rutaWebExe);

                _procesoWeb = Process.Start(info);

                if (_procesoWeb != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error iniciando web: " + ex.Message);
                return false;
            }
        }

        public void DetenerServidorWeb()
        {
            try
            {
                if (_procesoWeb != null && !_procesoWeb.HasExited)
                {
                    _procesoWeb.Kill();
                    _procesoWeb.WaitForExit(1000);
                }
            }
            catch { }

            try
            {
                foreach (var process in System.Diagnostics.Process.GetProcessesByName("PuntoVentaWeb"))
                {
                    process.Kill();
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!FormularioEstaAbierto(typeof(frmConfiguracionTicket)))
            {
                frmConfiguracionTicket config = new frmConfiguracionTicket();
                config.Show();
            }
        }
        private bool FormularioEstaAbierto(Type tipoFormulario)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == tipoFormulario)
                {
                    MessageBox.Show("Este módulo ya se encuentra abierto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm.BringToFront();
                    return true;
                }
            }
            return false;
        }

    }
}
