using Punto_Venta;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmProveedores : frmBase // Hereda de frmBase
    {
        public frmProveedores()
        {
            InitializeComponent();
            AplicarEstilos();
        }

        private void AplicarEstilos()
        {
            EstilizarDataGridView(this.dataGridView1);
            EstilizarBotonPrimario(this.button1);    // Agregar
            EstilizarBotonAdvertencia(this.button2); // Editar
            EstilizarBotonPeligro(this.button3);     // Eliminar
            EstilizarBotonPrimario(this.button4);    // Compras
            EstilizarBotonPrimario(this.button5);    // Agregar Abono
            EstilizarTextBox(this.textBox1);         // Buscador
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos(string filtroNombre = "")
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query;

                    if (string.IsNullOrWhiteSpace(filtroNombre))
                    {
                        query = "SELECT * FROM Proveedores ORDER BY Nombre;";
                    }
                    else
                    {
                        query = "SELECT * FROM Proveedores WHERE Nombre LIKE @Filtro ORDER BY Nombre;";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        if (!string.IsNullOrWhiteSpace(filtroNombre))
                        {
                            cmd.Parameters.Add("@Filtro", SqlDbType.VarChar, 150).Value = "%" + filtroNombre.Trim() + "%";
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;

                            if (dataGridView1.Columns.Count > 0)
                            {
                                dataGridView1.Columns[0].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CargarDatos(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarProveedor add = new frmAgregarProveedor();
            add.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    frmAgregarProveedor prov = new frmAgregarProveedor();
                    prov.Text = "Editar";
                    prov.lblID.Text = Convert.ToString(dataGridView1[0, index].Value);
                    prov.txtNombre.Text = Convert.ToString(dataGridView1[1, index].Value);
                    prov.txtRFC.Text = Convert.ToString(dataGridView1[2, index].Value);
                    prov.txtDireccion.Text = Convert.ToString(dataGridView1[3, index].Value);
                    prov.txtTelefono.Text = Convert.ToString(dataGridView1[4, index].Value);
                    prov.txtCorreo.Text = Convert.ToString(dataGridView1[5, index].Value);
                    prov.txtReferencia.Text = Convert.ToString(dataGridView1[6, index].Value);
                    prov.textBox1.Text = Convert.ToString(dataGridView1[7, index].Value);
                    prov.button1.Text = "Editar";
                    prov.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Seleccione un proveedor para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    frmPolizas add = new frmPolizas();
                    add.idProveedor = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                    add.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Seleccione un proveedor para ver sus compras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    frmAbonoProveedor abono = new frmAbonoProveedor();
                    abono.txtAdeudo.Text = Convert.ToString(dataGridView1[8, index].Value);
                    abono.lblID.Text = Convert.ToString(dataGridView1[0, index].Value);
                    abono.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Seleccione un proveedor para agregar un abono.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("¿Estás seguro de eliminar el proveedor seleccionado?", "¡Alto!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int idProveedor = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);

                        using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                        {
                            conectar.Open();
                            string query = "DELETE FROM Proveedores WHERE Id = @Id;";
                            using (SqlCommand cmd = new SqlCommand(query, conectar))
                            {
                                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idProveedor;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("PROVEEDOR ELIMINADO CON ÉXITO", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos(textBox1.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un proveedor para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}