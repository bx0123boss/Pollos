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
using System.IO;
using System.Data.SqlClient;

namespace Punto_Venta
{
    public partial class frmAgregarPlatillo : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbCommand cmd2;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public string cat1, cat2;
        public string id = "0";
        public string idArticulo1 = "0", idArticulo2 = "0", idArticulo3 = "0", idArticulo4 = "0", idArticulo5 = "0", idArticulo6 = "0", idArticulo7 = "0", idArticulo8 = "0", idArticulo9 = "0", idArticulo10 = "0";
        public string Nombre1 = "0", Nombre2 = "0", Nombre3 = "0", Nombre4 = "0", Nombre5 = "0", Nombre6 = "0", Nombre7 = "0", Nombre8 = "0", Nombre9 = "0", Nombre10 = "0";
        public string Medida1 = "0", Medida2 = "0", Medida3 = "0", Medida4 = "0", Medida5 = "0", Medida6 = "0", Medida7 = "0", Medida8 = "0", Medida9 = "0", Medida10 = "0";
        public string Precio1 = "0", Precio2 = "0", Precio3 = "0", Precio4 = "0", Precio5 = "0", Precio6 = "0", Precio7 = "0", Precio8 = "0", Precio9 = "0", Precio10 = "0";
        public frmAgregarPlatillo()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void btnArticulo1_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo1 = ori.Id;
                    Nombre1 = ori.Nombre;
                    Medida1 = ori.Medida;
                    Precio1 = ori.Precio;
                }
                lblNombre.Text = Nombre1;
                lblMedida.Text = Medida1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo2 = ori.Id;
                    Nombre2 = ori.Nombre;
                    Medida2 = ori.Medida;
                    Precio2 = ori.Precio;
                }
                lblNombre2.Text = Nombre2;
                lblMedida2.Text = Medida2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo3 = ori.Id;
                    Nombre3 = ori.Nombre;
                    Medida3 = ori.Medida;
                    Precio3 = ori.Precio;
                }
                lblNombre3.Text = Nombre3;
                lblMedida3.Text = Medida3;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo6 = ori.Id;
                    Nombre6 = ori.Nombre;
                    Medida6 = ori.Medida;
                    Precio6 = ori.Precio;
                }
                lblNombre6.Text = Nombre6;
                lblMedida6.Text = Medida6;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo7 = ori.Id;
                    Nombre7 = ori.Nombre;
                    Medida7 = ori.Medida;
                    Precio7 = ori.Precio;
                }
                lblNombre7.Text = Nombre7;
                lblMedida7.Text = Medida7;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo8 = ori.Id;
                    Nombre8 = ori.Nombre;
                    Medida8 = ori.Medida;
                    Precio8 = ori.Precio;
                }
                lblNombre8.Text = Nombre8;
                lblMedida8.Text = Medida8;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo4 = ori.Id;
                    Nombre4 = ori.Nombre;
                    Medida4 = ori.Medida;
                    Precio4 = ori.Precio;
                }
                lblNombre4.Text = Nombre4;
                lblMedida4.Text = Medida4;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo9 = ori.Id;
                    Nombre9 = ori.Nombre;
                    Medida9 = ori.Medida;
                    Precio9 = ori.Precio;
                }
                lblNombre9.Text = Nombre9;
                lblMedida9.Text = Medida9;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo5 = ori.Id;
                    Nombre5 = ori.Nombre;
                    Medida5 = ori.Medida;
                    Precio5 = ori.Precio;
                }
                lblNombre5.Text = Nombre5;
                lblMedida5.Text = Medida5;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (frmBusquedaArticulo ori = new frmBusquedaArticulo())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    idArticulo10 = ori.Id;
                    Nombre10 = ori.Nombre;
                    Medida10 = ori.Medida;
                    Precio10 = ori.Precio;
                }
                lblNombre10.Text = Nombre10;
                lblMedida10.Text = Medida10;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string comanda = checkBox1.Checked ? "1" : "0";

            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                if (this.Text == "Agregar Platillo")
                {
                    string query = @"
            INSERT INTO Inventario (
                Nombre, Precio, idProducto1, CantidadProducto1, idProducto2, CantidadProducto2, 
                idProducto3, CantidadProducto3, idProducto4, CantidadProducto4, idProducto5, 
                CantidadProducto5, idProducto6, CantidadProducto6, idProducto7, CantidadProducto7, 
                idProducto8, CantidadProducto8, idProducto9, CantidadProducto9, idProducto10, 
                CantidadProducto10, IdCategoria, CostoTotal, Comanda, IdSubCategoria
            ) VALUES (
                @Nombre, @Precio, @idProducto1, @CantidadProducto1, @idProducto2, @CantidadProducto2, 
                @idProducto3, @CantidadProducto3, @idProducto4, @CantidadProducto4, @idProducto5, 
                @CantidadProducto5, @idProducto6, @CantidadProducto6, @idProducto7, @CantidadProducto7, 
                @idProducto8, @CantidadProducto8, @idProducto9, @CantidadProducto9, @idProducto10, 
                @CantidadProducto10, @Categoria, @CostoTotal, @Comanda, @SubCategoria
            );";
                    MessageBox.Show(query);

                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd2.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        cmd2.Parameters.AddWithValue("@idProducto1", idArticulo1);
                        cmd2.Parameters.AddWithValue("@CantidadProducto1", txtCantidad.Text);
                        cmd2.Parameters.AddWithValue("@idProducto2", idArticulo2);
                        cmd2.Parameters.AddWithValue("@CantidadProducto2", txtCantidad2.Text);
                        cmd2.Parameters.AddWithValue("@idProducto3", idArticulo3);
                        cmd2.Parameters.AddWithValue("@CantidadProducto3", txtCantidad3.Text);
                        cmd2.Parameters.AddWithValue("@idProducto4", idArticulo4);
                        cmd2.Parameters.AddWithValue("@CantidadProducto4", txtCantidad4.Text);
                        cmd2.Parameters.AddWithValue("@idProducto5", idArticulo5);
                        cmd2.Parameters.AddWithValue("@CantidadProducto5", txtCantidad5.Text);
                        cmd2.Parameters.AddWithValue("@idProducto6", idArticulo6);
                        cmd2.Parameters.AddWithValue("@CantidadProducto6", txtCantidad6.Text);
                        cmd2.Parameters.AddWithValue("@idProducto7", idArticulo7);
                        cmd2.Parameters.AddWithValue("@CantidadProducto7", txtCantidad7.Text);
                        cmd2.Parameters.AddWithValue("@idProducto8", idArticulo8);
                        cmd2.Parameters.AddWithValue("@CantidadProducto8", txtCantidad8.Text);
                        cmd2.Parameters.AddWithValue("@idProducto9", idArticulo9);
                        cmd2.Parameters.AddWithValue("@CantidadProducto9", txtCantidad9.Text);
                        cmd2.Parameters.AddWithValue("@idProducto10", idArticulo10);
                        cmd2.Parameters.AddWithValue("@CantidadProducto10", txtCantidad10.Text);
                        cmd2.Parameters.AddWithValue("@Categoria", comboBox1.SelectedValue);
                        cmd2.Parameters.AddWithValue("@CostoTotal", lblTotal.Text);
                        cmd2.Parameters.AddWithValue("@Comanda", comanda);
                        cmd2.Parameters.AddWithValue("@SubCategoria", comboBox2.SelectedValue);

                        cmd2.ExecuteNonQuery();
                    }

                    MessageBox.Show("Se ha agregado el platillo con éxito", "AGREGADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (this.Text == "Editar Platillo")
                {
                    string query = @"
            UPDATE Inventario SET 
                Nombre = @Nombre, Precio = @Precio, idProducto1 = @idProducto1, 
                CantidadProducto1 = @CantidadProducto1, idProducto2 = @idProducto2, 
                CantidadProducto2 = @CantidadProducto2, idProducto3 = @idProducto3, 
                CantidadProducto3 = @CantidadProducto3, idProducto4 = @idProducto4, 
                CantidadProducto4 = @CantidadProducto4, idProducto5 = @idProducto5, 
                CantidadProducto5 = @CantidadProducto5, idProducto6 = @idProducto6, 
                CantidadProducto6 = @CantidadProducto6, idProducto7 = @idProducto7, 
                CantidadProducto7 = @CantidadProducto7, idProducto8 = @idProducto8, 
                CantidadProducto8 = @CantidadProducto8, idProducto9 = @idProducto9, 
                CantidadProducto9 = @CantidadProducto9, idProducto10 = @idProducto10, 
                CantidadProducto10 = @CantidadProducto10, IdCategoria = @Categoria, 
                CostoTotal = @CostoTotal, Comanda = @Comanda, IdSubCategoria = @SubCategoria 
            WHERE IdInventario = @Id;";

                    using (SqlCommand cmd2 = new SqlCommand(query, conectar))
                    {
                        cmd2.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd2.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        cmd2.Parameters.AddWithValue("@idProducto1", idArticulo1);
                        cmd2.Parameters.AddWithValue("@CantidadProducto1", txtCantidad.Text);
                        cmd2.Parameters.AddWithValue("@idProducto2", idArticulo2);
                        cmd2.Parameters.AddWithValue("@CantidadProducto2", txtCantidad2.Text);
                        cmd2.Parameters.AddWithValue("@idProducto3", idArticulo3);
                        cmd2.Parameters.AddWithValue("@CantidadProducto3", txtCantidad3.Text);
                        cmd2.Parameters.AddWithValue("@idProducto4", idArticulo4);
                        cmd2.Parameters.AddWithValue("@CantidadProducto4", txtCantidad4.Text);
                        cmd2.Parameters.AddWithValue("@idProducto5", idArticulo5);
                        cmd2.Parameters.AddWithValue("@CantidadProducto5", txtCantidad5.Text);
                        cmd2.Parameters.AddWithValue("@idProducto6", idArticulo6);
                        cmd2.Parameters.AddWithValue("@CantidadProducto6", txtCantidad6.Text);
                        cmd2.Parameters.AddWithValue("@idProducto7", idArticulo7);
                        cmd2.Parameters.AddWithValue("@CantidadProducto7", txtCantidad7.Text);
                        cmd2.Parameters.AddWithValue("@idProducto8", idArticulo8);
                        cmd2.Parameters.AddWithValue("@CantidadProducto8", txtCantidad8.Text);
                        cmd2.Parameters.AddWithValue("@idProducto9", idArticulo9);
                        cmd2.Parameters.AddWithValue("@CantidadProducto9", txtCantidad9.Text);
                        cmd2.Parameters.AddWithValue("@idProducto10", idArticulo10);
                        cmd2.Parameters.AddWithValue("@CantidadProducto10", txtCantidad10.Text);
                        cmd2.Parameters.AddWithValue("@Categoria", comboBox1.SelectedValue);
                        cmd2.Parameters.AddWithValue("@CostoTotal", lblTotal.Text);
                        cmd2.Parameters.AddWithValue("@Comanda", comanda);
                        cmd2.Parameters.AddWithValue("@SubCategoria", comboBox2.SelectedValue);
                        cmd2.Parameters.AddWithValue("@Id", id);

                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Se ha editado el platillo con éxito", "EDITADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        int suma;

        public void obtenersumar()
        {
            using (StreamReader sr = new StreamReader("C:\\Jaeger Soft\\platillo.txt", false))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    suma = Convert.ToInt32(line) + 1;
                }
            }

            FileStream stream = new FileStream("C:\\Jaeger Soft\\platillo.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("" + suma);
            writer.Close();
        }
        private void frmAgregarPlatillo_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmPlatillos platillo = new frmPlatillos();           
            platillo.Show();
        }

        private void txtCantidad8_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad8.Text == "")
            {
                lblPrecio8.Text = "0";
            }
            else
                lblPrecio8.Text = "" + Convert.ToDouble(Precio8) * Convert.ToDouble(txtCantidad8.Text);
            lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));

        }


        private void frmAgregarPlatillo_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DisplayMember = "Nombre";
                    comboBox1.ValueMember = "IdCategoria";
                    comboBox1.DataSource = dt;
                }

                // Llenar comboBox2 con SubCategoria
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SubCategoria;", conectar))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DisplayMember = "Nombre";
                    comboBox2.ValueMember = "IdSubcategoria";
                    comboBox2.DataSource = dt;
                }
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text == "")
                {
                    lblPrecio1.Text = "0";
                    lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
                }
                else
                {
                    lblPrecio1.Text = "" + Convert.ToDouble(Precio1) * Convert.ToDouble(txtCantidad.Text);
                    lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
                }
            }
            catch
            { }
        }

        private void txtCantidad2_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad2.Text == "")
            {
                lblPrecio2.Text = "0";
            }
            else
            {
                lblPrecio2.Text = "" + Convert.ToDouble(Precio2) * Convert.ToDouble(txtCantidad2.Text);
            }
            lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            catch 
            { }
        }

        private void txtCantidad3_TextChanged(object sender, EventArgs e)
        {
            try{    
            if (txtCantidad3.Text == "")
            {
                lblPrecio3.Text = "0";
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            else
            {
                lblPrecio3.Text = "" + Convert.ToDouble(Precio3) * Convert.ToDouble(txtCantidad3.Text);
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            }
            catch 
            { }
        }

        private void txtCantidad5_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad5.Text == "")
            {
                lblPrecio5.Text = "0";
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            else
            {
                lblPrecio5.Text = "" + Convert.ToDouble(Precio5) * Convert.ToDouble(txtCantidad5.Text);
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            }
            catch
            { }
        }

        private void txtCantidad4_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad4.Text == "")
            {
                lblPrecio4.Text = "0";
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            else
            {
                lblPrecio4.Text = "" + Convert.ToDouble(Precio4) * Convert.ToDouble(txtCantidad4.Text);
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }

            }
            catch 
            { }
        }

        private void txtCantidad6_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad6.Text == "")
            {
                lblPrecio6.Text = "0";
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            else
            {
                lblPrecio6.Text = "" + Convert.ToDouble(Precio6) * Convert.ToDouble(txtCantidad6.Text);
                lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            }
            catch 
            { }
        }

        private void txtCantidad7_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad7.Text == "")
            {
                lblPrecio7.Text = "0";
            }
            else
                lblPrecio7.Text = "" + Convert.ToDouble(Precio7) * Convert.ToDouble(txtCantidad7.Text);
            lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            catch 
            { }
        }

        private void txtCantidad9_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad9.Text == "")
            {
                lblPrecio9.Text = "0";
            }
            else
                lblPrecio9.Text = "" + Convert.ToDouble(Precio9) * Convert.ToDouble(txtCantidad9.Text);
            lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            catch 
            { }
        }

        private void txtCantidad10_TextChanged(object sender, EventArgs e)
        {
            try{
            if (txtCantidad10.Text == "")
            {
                lblPrecio10.Text = "0";
            }
            else
                lblPrecio10.Text = "" + Convert.ToDouble(Precio10) * Convert.ToDouble(txtCantidad10.Text);
            lblTotal.Text = "" + (Convert.ToDouble(lblPrecio1.Text) + Convert.ToDouble(lblPrecio2.Text) + Convert.ToDouble(lblPrecio3.Text) + Convert.ToDouble(lblPrecio4.Text) + Convert.ToDouble(lblPrecio5.Text) + Convert.ToDouble(lblPrecio6.Text) + Convert.ToDouble(lblPrecio7.Text) + Convert.ToDouble(lblPrecio8.Text) + Convert.ToDouble(lblPrecio9.Text) + Convert.ToDouble(lblPrecio10.Text));
            }
            catch
            { }
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

        private void txtCantidad2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad6_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCantidad7_KeyPress(object sender, KeyPressEventArgs e)
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
