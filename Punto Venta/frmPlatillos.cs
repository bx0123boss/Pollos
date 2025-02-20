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
        
        public frmPlatillos()
        {
            InitializeComponent();
        }

        private void frmPlatillos_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, A.IdSubcategoria, A.CostoTotal " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria "+
                    "ORDER BY A.Nombre;", conectar))
                {
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];
                    dgvInventario.Columns[0].Visible = false;
                    dgvInventario.Columns[6].Visible = false;
                    dgvInventario.Columns[7].Visible = false;
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
            if (dgvInventario.CurrentRow == null)
            {
                return;
            }
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
           
            platillo.Text = "Editar Platillo";
            platillo.id = dgvInventario.CurrentRow.Cells["IdInventario"].Value.ToString();
            platillo.txtNombre.Text = dgvInventario.CurrentRow.Cells["Nombre"].Value.ToString();
            platillo.txtPrecio.Text = dgvInventario.CurrentRow.Cells["Precio"].Value.ToString();
            platillo.cat1 = dgvInventario.CurrentRow.Cells["IdCategoria"].Value.ToString();
            platillo.cat2 = dgvInventario.CurrentRow.Cells["IdSubcategoria"].Value.ToString();
            if (Convert.ToBoolean(dgvInventario.CurrentRow.Cells["Comanda"].Value))
            {
                platillo.checkBox1.Checked = true;
            }
           
            platillo.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null)
            {
                return;
            }
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                using (SqlCommand cmd2 = new SqlCommand("DELETE FROM INVENTARIO WHERE IdInventario = @Id;", conectar))
                {
                    cmd2.Parameters.AddWithValue("@Id", dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString());
                    cmd2.ExecuteNonQuery();
                }

                MessageBox.Show("Se ha eliminado con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, A.IdSubcategoria , A.CostoTotal" +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    "ORDER BY A.Nombre;", conectar))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];

                    dgvInventario.Columns[0].Visible = false;
                    dgvInventario.Columns[6].Visible = false;
                    dgvInventario.Columns[7].Visible = false;
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
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, A.IdSubcategoria, A.CostoTotal " +
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
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, A.IdSubcategoria, A.CostoTotal " +
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
                dgvInventario.Columns[0].Visible = false;
                dgvInventario.Columns[6].Visible = false;
                dgvInventario.Columns[7].Visible = false;
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
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria, A.IdCategoria, A.IdSubcategoria, A.CostoTotal " +
                    " FROM Inventario A  " +
                    " INNER JOIN CATEGORIAS B ON A.IdCategoria = B.IdCategoria " +
                    " LEFT JOIN SUBCATEGORIAS C ON A.IdSubcategoria = C.IdSubcategoria " +
                    " WHERE B.IdCategoria = @Categoria ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Categoria", comboBox2.SelectedValue);
                        da.Fill(ds, "Id");
                    }
                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT A.IdInventario, A.Nombre, A.Precio, B.Nombre AS Categoria, A.Comanda, C.Nombre AS Subcategoria,  A.IdCategoria, A.IdSubcategoria, A.CostoTotal" +
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
                dgvInventario.Columns[6].Visible = false;
                dgvInventario.Columns[7].Visible = false;
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
