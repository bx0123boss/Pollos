using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmArticulosCancelados : Form
    {
        public frmArticulosCancelados()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmArticulosCancelados_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT 
                                    A.Cantidad, 
                                    CASE 
                                    WHEN A.IdInventario = 0 THEN P.Nombre 
                                    ELSE C.Nombre 
                                    END AS Nombre,
                                    CASE 
                                        WHEN A.IdInventario = 0 THEN P.Precio 
                                        ELSE C.Precio 
                                    END AS Precio,
                                    A.Total,
                                    B.Usuario AS Mesero,
                                    A.Comentario
                                    from ArticulosMesa A
									 LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                                    LEFT JOIN USUARIOS B ON B.IdUsuario  =A.IdUsuarioCancelo
                                    INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
                                    WHERE A.Estatus = 'CANCELADO'
                                    AND A.FechaHora >= @StartDate AND A.FechaHora <= @EndDate;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                string query = @"SELECT 
                                    A.Cantidad, 
                                    CASE 
                                    WHEN A.IdInventario = 0 THEN P.Nombre 
                                    ELSE C.Nombre 
                                    END AS Nombre,
                                    CASE 
                                        WHEN A.IdInventario = 0 THEN P.Precio 
                                        ELSE C.Precio 
                                    END AS Precio,
                                    A.Total,
                                    B.Usuario AS Mesero,
                                    A.Comentario
                                    from ArticulosMesa A
									 LEFT JOIN Promos P ON A.IdPromo = P.IdPromo
                                    LEFT JOIN USUARIOS B ON B.IdUsuario  =A.IdUsuarioCancelo
                                    INNER JOIN INVENTARIO C ON C.IdInventario = A.IdInventario
                                    WHERE A.Estatus = 'CANCELADO'
                                    AND A.FechaHora >= @StartDate AND A.FechaHora <= @EndDate;";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conectar))
                {
                    da.SelectCommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1));

                    da.Fill(ds, "IdFolio");
                    dataGridView1.DataSource = ds.Tables["IdFolio"];
                }
            }
        }
    }
}
