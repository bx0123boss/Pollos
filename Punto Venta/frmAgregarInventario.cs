using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Punto_Venta
{
    public partial class frmAgregarInventario : Form
    {
        public string lista = "";
        public frmAgregarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                using (SqlCommand cmdVerificar = new SqlCommand("SELECT Nombre FROM Productos WHERE Nombre = @Nombre;", conectar))
                {
                    cmdVerificar.Parameters.AddWithValue("@Nombre", txtProducto.Text);

                    using (SqlDataReader reader = cmdVerificar.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("Ya existe un producto con este nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (SqlCommand cmdInsertar = new SqlCommand(
                    "INSERT INTO Productos (Nombre, Cantidad, Medida, IdOrigen, Precio, Limite) " +
                    "VALUES (@Nombre, @Cantidad, @Medida, @IdOrigen, @Precio, @Limite);", conectar))
                {
                    // Agregar parámetros para evitar SQL Injection
                    cmdInsertar.Parameters.AddWithValue("@Nombre", txtProducto.Text);
                    cmdInsertar.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(txtCantidad.Text)); // Convertir a entero
                    cmdInsertar.Parameters.AddWithValue("@Medida", comboBox1.Text);
                    cmdInsertar.Parameters.AddWithValue("@IdOrigen", comboBox2.SelectedValue); // Usar SelectedValue en lugar de ValueMember
                    cmdInsertar.Parameters.AddWithValue("@Precio", Convert.ToDecimal(txtPrecio.Text)); // Convertir a decimal
                    cmdInsertar.Parameters.AddWithValue("@Limite", Convert.ToDecimal(txtLimite.Text)); // Convertir a decimal

                    cmdInsertar.ExecuteNonQuery();

                    MessageBox.Show("Se ha agregado el producto correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmInventario invent = new frmInventario();

                    invent.Show();

                    this.Close();
                }
            }
        }

        private void frmAgregarInventario_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                DataTable dt = new DataTable();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Origen;", conectar))
                {
                    // Crear el adaptador de datos
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // Llenar el DataTable con los resultados de la consulta
                        da.Fill(dt);
                    }
                }

                // Configurar el ComboBox
                comboBox2.DisplayMember = "Nombre";
                comboBox2.ValueMember = "IdOrigen";
                comboBox2.DataSource = dt;
            }

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }

        private void txtLimite_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (Char.IsNumber(e.KeyChar))//Si es número
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)Keys.Back)//si es tecla borrar
            {
                e.Handled = false;
            }
            else //Si es otra tecla cancelamos
            {
                e.Handled = true;
            }
        }
    }
}
