using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmMesasOcupadas : Form
    {
        public frmMesasOcupadas()
        {
            InitializeComponent();
        }

        private void frmMesasOcupadas_Load(object sender, EventArgs e)
        {
            cargarMesas();
        }
        public void cargarMesas()
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                string query = @"SELECT A.IdMesa, A.Nombre, B.Usuario AS Mesero, A.CantidadPersonas, A.Impresion, A.IdMesero
                                    FROM MESAS A INNER JOIN  USUARIOS B ON A.IdMesero = B.IdUsuario
                                    WHERE A.Estatus = 'COCINA' AND IdCliente IS NULL
                                    ORDER BY Nombre";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Button but = new Button();
                        but.FlatStyle = FlatStyle.Flat;
                        but.FlatAppearance.BorderSize = 0;
                        but.Font = new System.Drawing.Font(new FontFamily("Calibri"), 16, FontStyle.Bold);
                        but.Size = new System.Drawing.Size(135, 80);
                        but.Text = reader["Nombre"].ToString(); 
                        but.Click += new EventHandler(this.Myevent); 
                        but.MouseHover += new EventHandler(this.Myevent2); 
                        but.BackColor = Color.SkyBlue;
                        but.Tag = new
                        {
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

                query = @"SELECT A.IdMesa, A.Nombre, B.Usuario AS Mesero, A.CantidadPersonas, A.Impresion, A.IdMesero, A.IdCliente
                                    FROM MESAS A INNER JOIN  USUARIOS B ON A.IdMesero = B.IdUsuario
                                    WHERE A.Estatus = 'COCINA' AND IdCliente IS NOT NULL
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
                        but.BackColor = Color.YellowGreen;
                        but.Tag = new
                        {
                            Id = reader["IdMesa"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Impresion = reader["Impresion"].ToString(),
                            IdMesero = reader["IdMesero"].ToString(),
                            Mesero = reader["Mesero"].ToString(),
                            IdCliente =int.Parse(reader["IdCliente"].ToString()),
                        };

                        // Agregar el botón al FlowLayoutPanel
                        flowBotones.Controls.Add(but);
                    }
                }
            }

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
                if (boton.BackColor == Color.SkyBlue)
                    cobrar.lblPersonas.Text = data.CantPersonas;
                else
                    cobrar.idCliente = data.IdCliente;
                this.Close();
                cobrar.ShowDialog();
               
            
            }


        }

        private void Myevent2(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            var data = (dynamic)boton.Tag;
            lblMesero.Text = data.Mesero;
        }

    }
}
