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
using LibPrintTicket;
using System.Globalization;
using System.Data.SqlClient;


namespace Punto_Venta
{
    public partial class frmVentaDetallada : Form
    {
        public string idMesero;
        public double total, utilidad;
        public string usuario = "";
        public string IdMesa;
        public string idCliente ="0";

        public frmVentaDetallada()
        {
            InitializeComponent();
            this.MinimumSize = new Size(750, 650);
        }

        private void frmVentaDetallada_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT A.IdArticulosFolio, A.Cantidad, B.Nombre, B.Precio, A.Total, A.Comentario 
                                FROM ArticulosFolio A
                                INNER JOIN INVENTARIO B ON A.IdInventario = B.IdInventario
                                WHERE IdFolio = @Folio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Folio", lblFolio.Text);
                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                }
                if (idCliente != "0")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes WHERE IdCliente= @IdCliente;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                lblNombre.Text = sqlDataReader["Nombre"].ToString();
                                string telefono = sqlDataReader["Telefono"].ToString();
                                string telefonoFormateado = $"({telefono.Substring(0, 3)}) {telefono.Substring(3, 3)}-{telefono.Substring(6, 4)}";
                                lblTelefono.Text = telefonoFormateado;
                                lblDirección.Text = sqlDataReader["Direccion"].ToString();
                                lblColonia.Text = sqlDataReader["Colonia"].ToString();
                                gbClientes.Visible = true;
                            }
                        }

                    }
                }
            }
            dataGridView1.Columns[0].Visible = false;
            lblMonto.Text = $"{total:C}";
            lblUtilidad.Text = $"{utilidad:C}";

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.MaxChar = 34;
            ticket.FontSize = 9;
            ticket.HeaderImage = Image.FromFile("C:\\Jaeger Soft\\logo.jpg");
            ticket.AddHeaderLine("*******  NOTA DE CONSUMO  *******");
            ticket.AddHeaderLine("FOLIO DE VENTA: " + lblFolio.Text);
            ticket.AddSubHeaderLine("FECHA: " + lblFecha.Text);
            ticket.AddSubHeaderLine("FECHA REIMPRESION: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                double lol = Convert.ToDouble(dataGridView1[5, i].Value.ToString());
                string producto;

                producto = dataGridView1[3, i].Value.ToString();


                string item = dataGridView1[2, i].Value.ToString();

                ticket.AddItem(String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", item), producto, "$" + String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", lol));


            }

            ticket.AddTotal("TOTAL", String.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", lblMonto.Text));
            ticket.AddFooterLine("  ¡GRACIAS POR SU PREFERENCIA!");
            ticket.PrintTicket("print");
            //ticket.PrintTicket("print");
            //ticket.PrintTicket("EPSON TM-T20II Receipt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            cmd = new OleDbCommand("INSERT INTO corte (concepto, total,fecha,FormaPago) VALUES ('CANCELACION DE FOLIO: " + lblFolio.Text + ", por: "+usuario+"',-" + total + ",'" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','CANCELADO');", conectar);
            cmd.ExecuteNonQuery();
            
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                string id="";
                cmd = new OleDbCommand("select ID1 from temp where Cantidad='" + dataGridView1[2, i].Value.ToString() + "' and producto='"+dataGridView1[3, i].Value.ToString()+"';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id= reader[0].ToString();
                }
                cmd = new OleDbCommand("delete from temp where ID1=" + id + ";", conectar);
                cmd.ExecuteNonQuery();
                cmd = new OleDbCommand("INSERT INTO ArticulosCancelados(Cantidad, Producto, Comentario, Mesa, Fecha, Mesero, Cancelo) VALUES ('" + dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString() + "','" + dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString() + "','','"+lblFolio.Text+"','" + (DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()) + "','" + lblMesero.Text + "','" + usuario + "');", conectar);
                cmd.ExecuteNonQuery();
            }
            */
            double ventas = 0;
            int mesas = 0;
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Ventas, Mesas FROM Usuarios WHERE IdUsuario = @IdMesero;", conectar))
                {
                    cmd.Parameters.AddWithValue("@IdMesero", idMesero);

                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        if (sqlDataReader.Read())
                        {
                            ventas = Convert.ToDouble(sqlDataReader["Ventas"]);
                            mesas = Convert.ToInt32(sqlDataReader["Mesas"]);
                        }
                    }
                    ventas -= total;
                    mesas--;
                    string query = @"INSERT INTO CORTE (Concepto, Total,FechaHora,FormaPago) VALUES
                                    (@Concepto, @Total, GETDATE(), 'CANCELADO')";
                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Concepto", $"Cancelacion de folio: {lblFolio.Text} por {usuario}");
                        cmd2.Parameters.AddWithValue("@Total", total * -1);
                        cmd2.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE folios set Estatus='CANCELADO' Where IdFolio = @IdFolio;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdFolio", lblFolio.Text);

                        cmd2.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Usuarios SET Ventas = @Ventas, Mesas = @Mesas WHERE IdUsuario = @IdMesero;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Ventas", ventas);
                        cmd2.Parameters.AddWithValue("@Mesas", mesas);
                        cmd2.Parameters.AddWithValue("@IdMesero", idMesero);

                        cmd2.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand("UPDATE MESAS SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", IdMesa);

                        cmd2.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd2 = new SqlCommand("UPDATE ArticulosMesa SET Estatus = 'CANCELADO' WHERE IdMesa = @IdMesa;", conectar))
                    {
                        cmd2.Parameters.AddWithValue("@IdMesa", IdMesa);

                        cmd2.ExecuteNonQuery();
                    }


                }

            }
            MessageBox.Show("ORDEN CANCELADA CON EXITO", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            this.Close();
        }
    }
}
