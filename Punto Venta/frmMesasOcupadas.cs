using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace Punto_Venta
{
    public partial class frmMesasOcupadas : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public frmMesasOcupadas()
        {
            InitializeComponent();
        }

        private void frmMesasOcupadas_Load(object sender, EventArgs e)
        {
            cargarMesas();
            cargarFolios();
            cargarRuta();
        }
        public void cargarMesas()
        {

            ds = new DataSet();
            conectar.Open();
            da = new OleDbDataAdapter("select * from mesas order by Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvMesas.DataSource = ds.Tables["Id"];
            for (int i = 0; i < dgvMesas.RowCount; i++)
            {
                string id = dgvMesas[0, i].Value.ToString();
                Button but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                //but.BackColor = Color.Aqua;
                but.Size = new System.Drawing.Size(135, 80);
                but.Text = dgvMesas[1, i].Value.ToString();
                but.Click += new EventHandler(this.Myevent);
                but.MouseHover += new EventHandler(this.Myevent2);
                flowBotones.Controls.Add(but);
                cmd = new OleDbCommand("SELECT * FROM ArticulosMesa where Mesa='" + id + "';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    but.BackColor = Color.Red;
                }
                else
                {
                    but.BackColor = Color.SkyBlue;
                }
            }               
        }
        public void cargarFolios()
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from folios where Estatus='COCINA';", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string id = dataGridView1[0, i].Value.ToString();
                Button but2 = new Button();
                but2.FlatStyle = FlatStyle.Flat;
                but2.FlatAppearance.BorderSize = 0;
                but2.Font = new System.Drawing.Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                //but.BackColor = Color.Aqua;
                but2.Size = new System.Drawing.Size(135, 80);
                but2.Text = dataGridView1[0, i].Value.ToString();
                //but2.Click += new EventHandler(this.Myevent);
                but2.Click += new EventHandler(this.Myevent3);
                flowBotones.Controls.Add(but2);
                but2.BackColor = Color.Orange;

            }
        }

        public void cargarRuta()
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from folios where Estatus='RUTA';", conectar);
            da.Fill(ds, "Id");
            dataGridView2.DataSource = ds.Tables["Id"];
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                string id = dataGridView2[0, i].Value.ToString();
                Button but2 = new Button();
                but2.FlatStyle = FlatStyle.Flat;
                but2.FlatAppearance.BorderSize = 0;
                but2.Font = new System.Drawing.Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                //but.BackColor = Color.Aqua;
                but2.Size = new System.Drawing.Size(135, 80);
                but2.Text = dataGridView2[0, i].Value.ToString();
                //but2.Click += new EventHandler(this.Myevent);
                but2.Click += new EventHandler(this.Myevent4);
                flowBotones.Controls.Add(but2);
                but2.BackColor = Color.Sienna;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Myevent(object sender, EventArgs e)
        {
            //BOTON PULSADO ALV
            if ((sender as Button).BackColor == Color.SkyBlue)
            {

            }
            else
            {
                bool abierto = false;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType() == typeof(frmCobros))
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
                    cmd = new OleDbCommand("select id,IdMesero,Mesero,Print from Mesas where Nombre='" + (sender as Button).Text + "';", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        frmCobros cobrar = new frmCobros();
                        cobrar.lblID.Text = reader[0].ToString();
                        cobrar.lblMesa.Text = (sender as Button).Text;
                        cobrar.lblMesero.Text = reader[2].ToString();
                        try
                        {
                            cobrar.idMesero = Convert.ToInt32(reader[1].ToString());
                        }
                        catch (Exception ex)
                        {
                            cobrar.idMesero = 0;
                            cobrar.lblMesero.Text = "Administrador";
                        }

                        cobrar.print = reader[3].ToString();
                        cobrar.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        private void Myevent2(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("select Mesero from Mesas where Nombre='" + (sender as Button).Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblMesero.Text = reader[0].ToString();
            }
        }

        private void Myevent3(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("select idCliente from folios where Folio='" + (sender as Button).Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                frmEntregarCocina cobrar = new frmEntregarCocina();
                cobrar.idCliente = reader[0].ToString();
                cobrar.lblFolio2.Text = (sender as Button).Text;
                cobrar.lblFolio.Text = (sender as Button).Text;
                cobrar.ShowDialog();
                this.Close();
            }
        }

        private void Myevent4(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("select * from folios where Folio='" + (sender as Button).Text + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                frmEntregarRuta entrega = new frmEntregarRuta();
                entrega.idCliente = reader[3].ToString();
                entrega.lblFolio.Text = reader[0].ToString();
                entrega.lblVehiculo.Text = reader[4].ToString();
                entrega.lblChofer.Text = reader[5].ToString();
                entrega.lblCambio.Text = "$" + reader[6].ToString();
                entrega.cambio = Convert.ToDouble(reader[6].ToString());
                entrega.lblFecha.Text = reader[8].ToString();
                entrega.lblFechaRuta.Text = reader[9].ToString();
                entrega.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCambiarMesa me = new frmCambiarMesa();
            me.ShowDialog();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmArticulosCancelados ar = new frmArticulosCancelados();
            ar.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmComandaGeneral com = new frmComandaGeneral();
            com.ShowDialog();
        }
    }
}
