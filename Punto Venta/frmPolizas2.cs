using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmPolizas2 : Form
    {
        public frmPolizas2()
        {
            InitializeComponent();
        }

        private void frmPolizas2_Load(object sender, EventArgs e)
        {
            CargarPolizas();
        }

        private void CargarPolizas()
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    string query;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conectar;

                    if (!string.IsNullOrWhiteSpace(textBox1.Text))
                    {
                        query = "SELECT * FROM Poliza2 WHERE Folio LIKE @Folio ORDER BY Folio;";
                        cmd.Parameters.Add("@Folio", SqlDbType.VarChar, 50).Value = "%" + textBox1.Text.Trim() + "%";
                    }
                    else
                    {
                        query = "SELECT * FROM Poliza2 ORDER BY Folio;";
                    }

                    cmd.CommandText = query;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las pólizas canceladas: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CargarPolizas();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conectar = new SqlConnection(Conexion.CadConSql))
                {
                    conectar.Open();
                    DateTime fechaSel = dateTimePicker1.Value.Date;
                    DateTime fechaInicio = fechaSel;
                    DateTime fechaFin = fechaSel.AddDays(1).AddTicks(-1);

                    string query = "SELECT * FROM Poliza2 WHERE Fecha >= @FechaInicio AND Fecha <= @FechaFin ORDER BY Folio;";

                    using (SqlCommand cmd = new SqlCommand(query, conectar))
                    {
                        cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar por fecha: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccione una póliza cancelada de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmDetallesPoliza poli = new frmDetallesPoliza();
            poli.label1.Text = "POLIZA CANCELADA EL:";
            poli.button1.Visible = false;
            poli.que = 2;

            int index = dataGridView1.CurrentRow.Index;
            poli.Id = Convert.ToString(dataGridView1[5, index].Value);
            poli.lblFolio.Text = Convert.ToString(dataGridView1[0, index].Value);
            poli.lblFechaPoli.Text = Convert.ToString(dataGridView1[1, index].Value);
            poli.lblFechaRealizada.Text = Convert.ToString(dataGridView1[2, index].Value);
            poli.lblMonto.Text = Convert.ToString(dataGridView1[3, index].Value);
            poli.lblExtra.Text = Convert.ToString(dataGridView1[4, index].Value);

            double.TryParse(poli.lblMonto.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double monto);
            double.TryParse(poli.lblExtra.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double extra);

            poli.lblTotal.Text = (monto + extra).ToString("0.00", CultureInfo.InvariantCulture);

            poli.Show();
            this.Close();
        }
    }
}