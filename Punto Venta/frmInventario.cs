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
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmInventario : Form
    {
       
        bool loaded = false;
        public string usuario;
        public frmInventario()
        {
            InitializeComponent();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (checkBox1.Checked)
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos WHERE IdOrigen = @Origen ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Origen", cmbOrigen.SelectedValue);
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }

                System.Data.DataTable dt = new System.Data.DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Origen;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                cmbOrigen.DisplayMember = "Nombre";
                cmbOrigen.ValueMember = "IdOrigen";
                cmbOrigen.DataSource = dt;

                if (usuario == "Administrador")
                {
                    button10.Visible = true;
                }

            }
            loaded = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarInventario add = new frmAgregarInventario();
            add.lista = cmbOrigen.SelectedValue.ToString();
            add.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmEditarInventario edita = new frmEditarInventario();
            edita.lista = cmbOrigen.SelectedValue.ToString();
            edita.txtID.Text = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.txtProducto.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.txtCantidad.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.comboBox1.Text = dgvInventario[3, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.origen = dgvInventario[4, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.txtPrecio.Text = dgvInventario[5, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.txtLimite.Text = dgvInventario[6, dgvInventario.CurrentRow.Index].Value.ToString();
            edita.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el artículo?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Productos WHERE IdProducto = @Id;", conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", dgvInventario[0, dgvInventario.CurrentRow.Index].Value);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha eliminado el Productos con éxito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }

                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAgregarExistencias agregar = new frmAgregarExistencias();
            agregar.lblProducto.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.lista = cmbOrigen.SelectedValue.ToString();
            agregar.txtActuales.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.lblID.Text = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPlatillos platillo = new frmPlatillos();
            platillo.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmDescontarExistencias agregar = new frmDescontarExistencias();
            agregar.lista = cmbOrigen.SelectedValue.ToString();
            agregar.lblProducto.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.txtActuales.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.lblID.Text = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            agregar.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (textBox1.Text == "")
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos WHERE Nombre LIKE @Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", "%" + textBox1.Text + "%");
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (checkBox1.Checked)
                {
                    textBox1.Enabled = false;
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos WHERE IdOrigen = @Origen ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Origen", cmbOrigen.SelectedValue);
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
                else
                {
                    textBox1.Enabled = true;
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                if (checkBox1.Checked)
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos WHERE IdOrigen = @Origen ORDER BY Nombre;", conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Origen", cmbOrigen.SelectedValue);
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
                else
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Productos ORDER BY Nombre;", conectar))
                    {
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmOrigen or = new frmOrigen();
            or.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmCategorias cat = new frmCategorias();
            cat.tipo = "Categorias";
            cat.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea imprimir un reporte para capturar el inventario?", "Alto!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = (Worksheet)xla.ActiveSheet;

                xla.Visible = true;

                ws.Cells[1, 1] = "ID";
                ws.Cells[1, 2] = "Nombre";
                ws.Cells[1, 3] = "Existen";
                int cont = 1;
                for (int i = 0; i < dgvInventario.RowCount; i++)
                {
                    cont++;
                    ws.Cells[cont, 1] = dgvInventario[0, i].Value.ToString();
                    ws.Cells[cont, 2] = dgvInventario[1, i].Value.ToString();
                    ws.Cells[cont, 3] = dgvInventario[2, i].Value.ToString();
                }
                frmExportarInventario fis = new frmExportarInventario();
                fis.Show();
                this.Close();
            }
            else
            {
                frmExportarInventario fis = new frmExportarInventario();
                fis.Show();
                this.Close();

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmCompras com = new frmCompras();
            com.usuario = usuario;
            com.Show();
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmCombos com = new frmCombos();
            com.Show();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmCategorias cat = new frmCategorias();
            cat.tipo = "Subcategoria";
            cat.Show();
            this.Close();
        }
    }
}
