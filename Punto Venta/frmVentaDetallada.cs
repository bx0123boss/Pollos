using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Punto_Venta
{
    public partial class frmVentaDetallada : Form
    {
        public string idMesero;
        public double total, utilidad;
        public string usuario = "";
        public string IdMesa;
        public string idCliente = "0";

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
                string query = @"SELECT A.IdInventario, A.IdArticulosFolio, A.Cantidad,
                                CASE 
                                    WHEN A.IdInventario = 0 THEN P.Nombre 
                                    ELSE B.Nombre 
                                END AS Nombre,
                                CASE 
                                    WHEN A.IdInventario = 0 THEN P.Precio 
                                    ELSE B.Precio 
                                END AS Precio, A.Total, A.Comentario , A.IdExtra as Ids, A.IdPromo
                                FROM ArticulosFolio A
                                INNER JOIN INVENTARIO B ON A.IdInventario = B.IdInventario
                                LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                                WHERE IdFolio = @Folio;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Folio", lblFolio.Text);
                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns["IdArticulosFolio"].Visible = false;
                    dataGridView1.Columns["Ids"].Visible = false;
                    dataGridView1.Columns["IdPromo"].Visible = false;
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
            List<Producto> productos = new List<Producto>();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                productos.Add(new Producto
                {
                    Nombre = dataGridView1[3, i].Value.ToString(),
                    Cantidad = Convert.ToDouble(dataGridView1[2, i].Value.ToString()),
                    PrecioUnitario = Convert.ToDouble(dataGridView1[5, i].Value.ToString()) / Convert.ToDouble(dataGridView1[2, i].Value.ToString()),
                    Total = Convert.ToDouble(dataGridView1[5, i].Value.ToString()),
                });
            }

            string GetNumericValue(string input)
            {
                return Regex.Replace(input, @"[^\d.-]", "");
            }

            double total = Convert.ToDouble(GetNumericValue(lblMonto.Text));
            Dictionary<string, double> totales = new Dictionary<string, double>();
            totales.Add("Subtotal", total / 1.16);
            totales.Add("IVA", (total / 1.16) * 0.16);
            totales.Add("Total", total);

            TicketPrinter ticketPrinter = new TicketPrinter(
                   Conexion.datosTicket,
                   Conexion.pieDeTicket,
                   Conexion.logoPath,
                   productos,
                   lblFolio.Text,
                   "",       // Datos Extra
                   "",
                   Convert.ToDouble(lblMonto.Text),
                   false,
                   totales,
                   "");

            ticketPrinter.ImprimirTicket();
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

                    using (SqlCommand cmd2 = new SqlCommand("UPDATE Folios set Estatus='CANCELADO', Utilidad = 0 Where IdFolio = @IdFolio;", conectar))
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
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        string ide = null;
                        if (dataGridView1.Rows[i].Cells["Ids"].Value.ToString().Length > 0)
                        {
                            ide = dataGridView1.Rows[i].Cells["Ids"].Value?.ToString();
                        }
                        string insertArticuloQuery = @"INSERT INTO tempInventario(id,cantidad, ide) 
                                            values (@id, @cantidad, @ide);";
                        using (SqlCommand cmd2 = new SqlCommand(insertArticuloQuery, conectar))
                        {
                            cmd2.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells["IdInventario"].Value?.ToString() == "0"
                                                                                ? dataGridView1.Rows[i].Cells["IdPromo"].Value
                                                                                : dataGridView1.Rows[i].Cells["IdInventario"].Value);
                            cmd2.Parameters.AddWithValue("@cantidad", Convert.ToDecimal(dataGridView1.Rows[i].Cells["Cantidad"].Value) * -1);
                            cmd2.Parameters.AddWithValue("@ide", (object)ide ?? DBNull.Value);

                            cmd2.ExecuteNonQuery();
                        }
                    }


                }
            }
            MessageBox.Show("ORDEN CANCELADA CON EXITO", "Comanda General", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
        }
    }
}
