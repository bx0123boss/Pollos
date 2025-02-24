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
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmEditarInventario : Form
    {
        public string origen;
        public frmEditarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                // Usar parámetros para evitar la inyección SQL
                string query = "UPDATE Productos SET Nombre=@Nombre, Cantidad=@Cantidad, Medida=@Medida, IdOrigen=@Origen, precio=@precio, limite=@limite WHERE IdProducto=@id;";

                using (SqlCommand cmd = new SqlCommand(query, conectar))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtProducto.Text);
                    cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                    cmd.Parameters.AddWithValue("@Medida", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Origen", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@precio", txtPrecio.Text);
                    cmd.Parameters.AddWithValue("@limite", txtLimite.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Se ha actualizado el producto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmEditarInventario_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Origen;", conectar))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }

            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "IdOrigen";
            comboBox2.DataSource = dt;
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtLimite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
