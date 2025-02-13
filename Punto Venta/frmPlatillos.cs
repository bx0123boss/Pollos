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
using System.IO;
using System.Data.SqlClient;
using System.Reflection;

namespace Punto_Venta
{
    public partial class frmPlatillos : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd2;
        
        public frmPlatillos()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void frmPlatillos_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria "+
                    "ORDER BY A.Nombre;", conectar))
                {
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];
                    dgvInventario.Columns[0].Visible = false;
                }

                // Llenar el ComboBox
                DataTable dt = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    comboBox2.DisplayMember = "Nombre";
                    comboBox2.ValueMember = "IdCategoria";
                    comboBox2.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
            platillo.Text = "Agregar Platillo";
            platillo.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
           
            platillo.Text = "Editar Platillo";
            platillo.id = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.txtNombre.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.txtPrecio.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.cat1 = dgvInventario[3, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.cat2 = dgvInventario[5, dgvInventario.CurrentRow.Index].Value.ToString();
            if (dgvInventario[4, dgvInventario.CurrentRow.Index].Value.ToString() == "1")
            {
                platillo.checkBox1.Checked = true;
            }
            
            string idInvent = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            cmd2 = new OleDbCommand("select * from Inventario where Id=" + idInvent + ";", conectar);
            OleDbDataReader invent = cmd2.ExecuteReader();
            if (invent.Read())
            {
                string id = invent[3].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                OleDbDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo1 = id;
                    platillo.Nombre1 = Convert.ToString(reader[1].ToString());
                    platillo.Medida1 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad.Text = invent[4].ToString();
                    platillo.Precio1 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio1.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[4].ToString());
                }
                id = invent[5].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo2 = id;
                    platillo.Nombre2 = Convert.ToString(reader[1].ToString());
                    platillo.Medida2 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre2.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida2.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad2.Text = invent[6].ToString();
                    platillo.Precio2 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio2.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[6].ToString());
                }
                id = invent[7].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo3 = id;
                    platillo.Nombre3 = Convert.ToString(reader[1].ToString());
                    platillo.Medida3 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre3.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida3.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad3.Text = invent[8].ToString();
                    platillo.Precio3 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio3.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[8].ToString());
                }
                id = invent[9].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo4 = id;
                    platillo.Nombre4 = Convert.ToString(reader[1].ToString());
                    platillo.Medida4 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre4.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida4.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad4.Text = invent[10].ToString();
                    platillo.Precio4 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio4.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[10].ToString());
                }
                id = invent[11].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo5 = id;
                    platillo.Nombre5 = Convert.ToString(reader[1].ToString());
                    platillo.Medida5 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre5.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida5.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad5.Text = invent[12].ToString();
                    platillo.Precio5 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio5.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[12].ToString());
                }
                id = invent[13].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo6 = id;
                    platillo.Nombre6 = Convert.ToString(reader[1].ToString());
                    platillo.Medida6 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre6.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida6.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad6.Text = invent[14].ToString();
                    platillo.Precio6 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio6.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[14].ToString());
                }
                id = invent[15].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo7 = id;
                    platillo.Nombre7 = Convert.ToString(reader[1].ToString());
                    platillo.Medida7 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre7.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida7.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad7.Text = invent[16].ToString();
                    platillo.Precio7 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio7.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[16].ToString());
                }
                id = invent[17].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo8 = id;
                    platillo.Nombre8 = Convert.ToString(reader[1].ToString());
                    platillo.Medida8 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre8.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida8.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad8.Text = invent[18].ToString();
                    platillo.Precio8 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio8.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[18].ToString());
                }
                id = invent[19].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo9 = id;
                    platillo.Nombre9 = Convert.ToString(reader[1].ToString());
                    platillo.Medida9 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre9.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida9.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad9.Text = invent[20].ToString();
                    platillo.Precio9 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio9.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[20].ToString());
                }
                id = invent[21].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo10 = id;
                    platillo.Nombre10 = Convert.ToString(reader[1].ToString());
                    platillo.Medida10 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre10.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida10.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad10.Text = invent[22].ToString();
                    platillo.Precio10 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio10.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[22].ToString());
                }
            }
            platillo.lblTotal.Text = "" + (Convert.ToDouble(platillo.lblPrecio1.Text) + Convert.ToDouble(platillo.lblPrecio2.Text) + Convert.ToDouble(platillo.lblPrecio3.Text) + Convert.ToDouble(platillo.lblPrecio4.Text) + Convert.ToDouble(platillo.lblPrecio5.Text) + Convert.ToDouble(platillo.lblPrecio6.Text) + Convert.ToDouble(platillo.lblPrecio7.Text) + Convert.ToDouble(platillo.lblPrecio8.Text) + Convert.ToDouble(platillo.lblPrecio9.Text) + Convert.ToDouble(platillo.lblPrecio10.Text));
            conectar.Close();
            platillo.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM INVENTARIO WHERE IdInventario = @Id;", conectar))
                {
                    cmd2.Parameters.AddWithValue("@Id", dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString());
                    cmd2.ExecuteNonQuery();
                }

                MessageBox.Show("Se ha eliminado con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    "ORDER BY A.Nombre;", conectar))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                DataSet ds = new DataSet();

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    "ORDER BY A.Nombre;", conectar))
                    {
                        da.Fill(ds, "Id");
                    }
                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    " WHERE A.Nombre LIKE @Nombre ORDER BY A.Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", $"%{textBox1.Text}%");
                        da.Fill(ds, "Id");
                    }
                }

                dgvInventario.DataSource = ds.Tables["Id"];
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                DataSet ds = new DataSet();

                if (checkBox1.Checked)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    " WHERE B.IdCategoria = @Categoria ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Categoria", comboBox2.Text);
                        da.Fill(ds, "Id");
                    }
                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria " +
                     " FROM Inventario A  " +
                     " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                     " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                     "ORDER BY A.Nombre;", conectar))
                    {
                        da.Fill(ds, "Id");
                    }
                }

                dgvInventario.DataSource = ds.Tables["Id"];
                dgvInventario.Columns[0].Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmGastos gas = new frmGastos();
            gas.Show();
            this.Close();
        }
    }
}
