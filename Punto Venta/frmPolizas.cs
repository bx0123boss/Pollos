using Punto_Venta;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPolizas : frmBase
    {
        public string usuario { get; set; } = "";
        public int idProveedor { get; set; } = 0;

        public frmPolizas()
        {
            InitializeComponent();
        }

        private void frmPolizas_Load(object sender, EventArgs e)
        {
            if (usuario == "Invitado")
            {
                button3.Hide();
            }

            // ESTILOS HEREDADOS DE frmBase
            EstilizarDataGridView(dataGridView1);
            EstilizarTextBox(textBox1);
            EstilizarBotonPrimario(button1);
            EstilizarBotonPrimario(button2);
            EstilizarBotonAdvertencia(button3);

            CargarPolizas();
        }

        private void CargarPolizas()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();

                    string query = "";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conectar;

                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        query = "SELECT Folio, Fecha, FechaCaptura, CostoTotal, CostoExtra, Id, IdProv, Proveedor, IVA, Total, IdAlmacen FROM Poliza WHERE Folio LIKE @Folio ORDER BY Folio";
                        cmd.Parameters.Add("@Folio", SqlDbType.NVarChar, 50).Value = "%" + textBox1.Text.Trim() + "%";
                    }
                    else if (idProveedor != 0)
                    {
                        query = "SELECT Folio, Fecha, FechaCaptura, CostoTotal, CostoExtra, Id, IdProv, Proveedor, IVA, Total, IdAlmacen FROM Poliza WHERE IdProv = @IdProv ORDER BY Fecha DESC";
                        cmd.Parameters.Add("@IdProv", SqlDbType.Int).Value = idProveedor;
                    }
                    else
                    {
                        DateTime fechaInicio = dateTimePicker1.Value.Date;
                        DateTime fechaFin = fechaInicio.AddDays(1).AddTicks(-1);

                        query = "SELECT Folio, Fecha, FechaCaptura, CostoTotal, CostoExtra, Id, IdProv, Proveedor, IVA, Total, IdAlmacen FROM Poliza WHERE Fecha >= @FechaInicio AND Fecha <= @FechaFin ORDER BY Fecha DESC";
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;
                    }

                    cmd.CommandText = query;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns["IdAlmacen"] != null)
                            dataGridView1.Columns["IdAlmacen"].Visible = false;
                    }

                    conectar.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar las pólizas: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarCompras add = new frmAgregarCompras();
            add.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CargarPolizas();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            // Limpiamos la búsqueda por folio al cambiar de fecha para evitar conflicto visual de filtros
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Clear();
            }
            else
            {
                CargarPolizas();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccione una póliza de la lista para ver los detalles.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmDetallesPoliza poli = new frmDetallesPoliza();
            poli.usuario = usuario;
            poli.lblFolio.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
            poli.que = 1;
            poli.lblFechaPoli.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentRow.Index].Value);
            poli.lblFechaRealizada.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentRow.Index].Value);
            poli.Id = Convert.ToString(dataGridView1[5, dataGridView1.CurrentRow.Index].Value);
            poli.lblMonto.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentRow.Index].Value);

            double.TryParse(Convert.ToString(dataGridView1[4, dataGridView1.CurrentRow.Index].Value), out double extra);
            poli.lblExtra.Text = extra.ToString("#,#.00", CultureInfo.InvariantCulture);

            poli.lblTotal.Text = Convert.ToString(dataGridView1[9, dataGridView1.CurrentRow.Index].Value);
            poli.lblIVA.Text = Convert.ToString(dataGridView1[8, dataGridView1.CurrentRow.Index].Value);

            poli.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPolizas2 poli2 = new frmPolizas2();
            poli2.Show();
            this.Close();
        }
    }
}