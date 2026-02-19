using System;
using System.Collections;
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
using Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Punto_Venta
{
    public partial class frmAgregarPromo : Form
    {
        public string id;
        bool lunes, martes, miercoles, jueves, viernes, sabado, domingo;
        public frmAgregarPromo()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1008, 742);
            this.MaximumSize = new Size(1008, 742);
        }

        private void frmAgregarPromo_Load(object sender, EventArgs e)
        {
            using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
            {
                conectar.Open();
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT IdSubcategoria,Nombre from SUBCATEGORIAS ORDER BY NOMBRE;", conectar))
                {
                    da.Fill(ds, "Productos");
                    dgvInventario.DataSource = ds.Tables["Productos"];
                }
                if (dgvInventario.Columns.Count > 0)
                {
                    dgvInventario.Columns[0].Visible = false;
                }
                if (id != null)
                {
                    string Query = @"SELECT * FROM ArticulosPromo A
                                        INNER JOIN SUBCATEGORIAS B ON A.IdSubcategoria= B.IdSubcategoria WHERE IdPromo = @Id;";
                    using (SqlCommand cmd = new SqlCommand(Query, conectar))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader sqlReader = cmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                DgvPedidoprevio.Rows.Add(sqlReader["IdSubcategoria"].ToString(), sqlReader["Cantidad"].ToString(), sqlReader["Nombre"].ToString());
                            }
                        }
                    }
                    this.Text = "Editar";

                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ya = false;
            string nombre = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            string id = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                if (nombre == DgvPedidoprevio[1, i].Value.ToString())
                    ya = true;
            }
            if (ya)
                MessageBox.Show("No puedes poner varias veces la subcategoria, solo cambia la cantidad", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                DgvPedidoprevio.Rows.Add(id, "1", nombre);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
            }
            catch
            {

            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT IdSubcategoria,Nombre from SUBCATEGORIAS " +
                        "WHERE Nombre LIKE @Nombre ORDER BY NOMBRE;",
                    conectar))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@Nombre", "%" + txtBuscar.Text + "%");
                        da.Fill(ds, "Productos");
                    }
                    dgvInventario.DataSource = ds.Tables["Productos"];
                    if (dgvInventario.Columns.Count > 0)
                    {
                        dgvInventario.Columns[0].Visible = false;
                    }
                }
            }
            else
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT IdSubcategoria,Nombre from SUBCATEGORIAS ORDER BY NOMBRE;", conectar))
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


        private void DgvPedidoprevio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                double cantidad = Convert.ToDouble(DgvPedidoprevio[0, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
            }
            catch 
            {
                MessageBox.Show("Solo puedes introducir números", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DgvPedidoprevio.Rows[e.RowIndex].Cells[0].Value = "1";
            }
        }

        private void DgvPedidoprevio_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtPrecio.Text != "" && DgvPedidoprevio.RowCount > 0)
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    lunes = cbLunes.Checked;
                    martes = cbMartes.Checked;
                    miercoles = cbMiercoles.Checked;
                    jueves = cbJueves.Checked;
                    viernes = cbViernes.Checked;
                    sabado = cbSabado.Checked;
                    domingo = cbDomingo.Checked;
                    string upadd = "";
                    conectar.Open();
                    if (this.Text == "Editar")
                    {
                        upadd = "actualizado";
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM ArticulosPromo WHERE IdPromo = @Id;", conectar))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("UPDATE Promos set Nombre = @Nombre, Precio = @Precio, Lunes= @Lunes,Martes=@Martes,Miercoles=@Miercoles,Jueves=@Jueves,Viernes=@Viernes,Sabado=@Sabado,Domingo=@Domingo  WHERE IdPromo = @Id;", conectar))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                            cmd.Parameters.AddWithValue("@Lunes", lunes);
                            cmd.Parameters.AddWithValue("@Martes", martes);
                            cmd.Parameters.AddWithValue("@Miercoles", miercoles);
                            cmd.Parameters.AddWithValue("@Jueves", jueves);
                            cmd.Parameters.AddWithValue("@Viernes", viernes);
                            cmd.Parameters.AddWithValue("@Sabado", sabado);
                            cmd.Parameters.AddWithValue("@Domingo", domingo);
                            cmd.ExecuteNonQuery();
                        }
                        for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                        {
                            string query = @"INSERT INTO ArticulosPromo (IdPromo, IdSubcategoria, Cantidad) 
                                        VALUES(@IdPromo, @IdSubcategoria, @Cantidad);";
                            using (SqlCommand cmd = new SqlCommand(query, conectar))
                            {
                                string Cantidad = DgvPedidoprevio[1, i].Value.ToString();
                                string ID = DgvPedidoprevio[0, i].Value.ToString();
                                cmd.Parameters.AddWithValue("@IdPromo", id);
                                cmd.Parameters.AddWithValue("@IdSubcategoria", ID);
                                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        upadd = "agregado";
                        string query = "INSERT INTO Promos (Nombre, Precio, Lunes,Martes,Miercoles,Jueves,Viernes,Sabado,Domingo) " +
                                                 "VALUES (@Nombre, @Precio, @Lunes,@Martes,@Miercoles,@Jueves,@Viernes,@Sabado,@Domingo); " +
                                                 "SELECT SCOPE_IDENTITY();"; // Obtener el último ID insertado
                        int lastIdFolio;
                        using (SqlCommand cmd = new SqlCommand(query, conectar))
                        {
                            // Usar parámetros para evitar SQL Injection
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                            cmd.Parameters.AddWithValue("@Lunes", lunes);
                            cmd.Parameters.AddWithValue("@Martes", martes);
                            cmd.Parameters.AddWithValue("@Miercoles", miercoles);
                            cmd.Parameters.AddWithValue("@Jueves", jueves);
                            cmd.Parameters.AddWithValue("@Viernes", viernes);
                            cmd.Parameters.AddWithValue("@Sabado", sabado);
                            cmd.Parameters.AddWithValue("@Domingo", domingo);
                            lastIdFolio = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                        {
                            query = @"INSERT INTO ArticulosPromo (IdPromo, IdSubcategoria, Cantidad) 
                                        VALUES(@IdPromo, @IdSubcategoria, @Cantidad);";
                            using (SqlCommand cmd = new SqlCommand(query, conectar))
                            {
                                string Cantidad = DgvPedidoprevio[1, i].Value.ToString();
                                string ID= DgvPedidoprevio[0, i].Value.ToString();
                                cmd.Parameters.AddWithValue("@IdPromo", lastIdFolio);
                                cmd.Parameters.AddWithValue("@IdSubcategoria", ID);
                                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageBox.Show("Se ha " + upadd + " la promoción correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCombos com = new frmCombos();
                    com.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("FALTAN DATOS PARA COMPLETAR LA PROMOCION", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
