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
using System.Data.SqlClient;

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
            //cargarFolios();
            //cargarRuta();
        }
        public void cargarMesas()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                // Consulta para obtener las mesas
                string query = @"SELECT A.IdMesa, A.Nombre, B.Usuario AS Mesero, A.CantidadPersonas, A.Impresion, A.IdMesero
                                    FROM MESAS A INNER JOIN  USUARIOS B ON A.IdMesero = B.IdUsuario
                                    WHERE A.Estatus = 'COCINA'
                                    ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Recorrer los resultados de la consulta
                    while (reader.Read())
                    {
                        // Crear un botón para la mesa
                        Button but = new Button();
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 0;
                        but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                        but.Size = new System.Drawing.Size(135, 80);
                        but.Text = reader["Nombre"].ToString(); // Asignar el nombre de la mesa al botón
                        but.Click += new EventHandler(this.Myevent); // Asignar evento Click
                        but.MouseHover += new EventHandler(this.Myevent2); // Asignar evento MouseHover
                        but.BackColor = Color.SkyBlue;
                        but.Tag = new { 
                                        Id = reader["IdMesa"].ToString(), 
                                        Nombre = reader["Nombre"].ToString(), 
                                        Impresion = reader["Impresion"].ToString(), 
                                        IdMesero = reader["IdMesero"].ToString(),
                                        Mesero = reader["Mesero"].ToString(),
                                        CantPersonas = reader["CantidadPersonas"].ToString(),
                        };

                        // Agregar el botón al FlowLayoutPanel
                        flowBotones.Controls.Add(but);
                    }
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
                but2.Visible = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Myevent(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var data = (dynamic)boton.Tag;
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
                frmCobros cobrar = new frmCobros();
                cobrar.lblID.Text = data.Id;
                cobrar.lblMesa.Text = (sender as Button).Text;
                cobrar.lblMesero.Text = data.Mesero;
                cobrar.idMesero = int.Parse(data.IdMesero);
                cobrar.print = data.Impresion == "True" ? "1" : "0";
                cobrar.lblPersonas.Text = data.CantPersonas;
                cobrar.Show();
                this.Close();
            
            }


        }

        private void Myevent2(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var data = (dynamic)boton.Tag;
            lblMesero.Text = data.Mesero;
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

        private void button4_Click(object sender, EventArgs e)
        {
            frmAgregarMesas add = new frmAgregarMesas();
            add.Show();
            this.Close();
        }
    }
}
