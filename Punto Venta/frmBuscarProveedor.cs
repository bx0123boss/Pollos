using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmBuscarProveedor : Form
    {
        public string ID { get; set; }
        public string Nombre { get; set; }

        public frmBuscarProveedor()
        {
            InitializeComponent();
        }

        private void frmBuscarProveedor_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        private void CargarProveedores(string filtroNombre = "")
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
                                dataGridView1.Columns[0].Visible = false; // Id
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar proveedores: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                ID = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
                Nombre = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (frmAgregarProveedor cliente = new frmAgregarProveedor())
            {
                cliente.buscar = true;
                if (cliente.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = cliente.Nombre;
                    CargarProveedores(textBox1.Text);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CargarProveedores(textBox1.Text);
        }
    }
}