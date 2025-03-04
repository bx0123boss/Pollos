
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Punto_Venta
{
    public partial class frmActInventario : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);  
        OleDbDataAdapter da;
        OleDbCommand cmd;

        public frmActInventario()
        {
            InitializeComponent();
            conectar.Open();
        }
        private void frmActInventario_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from temp;", conectar);
            da.Fill(ds, "Id");
            dgvMesa.DataSource = ds.Tables["Id"];
            dgvMesa.Columns[0].Visible = false;
            dgvMesa.Columns[5].Visible = false;

        }
        private void button1_Click(object sender, EventArgs e)
        {                        
                    for (int i = 0; i < dgvMesa.RowCount; i++)
                    {
                        if (dgvMesa.Rows[i].Cells["Ids"].Value.ToString().Length > 0)
                        {
                            //MessageBox.Show(dgvMesa[0, i].Value.ToString().Substring(0, 1));
                            if (dgvMesa[0, i].Value.ToString().Substring(0, 1) == "C" || dgvMesa[0, i].Value.ToString().Substring(0, 1) == "P")
                            {
                                //MessageBox.Show("NO SE DESCUENTA TAMBIÉN POR SER PIZZA");
                            }
                            else
                                descontarInventario(dgvMesa[0, i].Value.ToString(), Convert.ToDouble(dgvMesa[1, i].Value.ToString()));
                                //MessageBox.Show("SE DESCUENTA TAMBIÉN POR SER PIZZA");
                            string ide = dgvMesa[6, i].Value.ToString();
                            string[] ids = ide.Split(';');
                            foreach (var word in ids)
                            {
                                conectar.Close();
                                conectar.Open();
                                string[] ids2 = word.Split(',');
                                for (int i2 = 0; i2 < ids2.Length - 1; i2 = i2 + 2)
                                {
                                    cmd = new OleDbCommand("SELECT Id,Nombre FROM Inventario where Id=" + ids2[1] + ";", conectar);
                                    OleDbDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        descontarInventario(reader[0].ToString(), Convert.ToDouble(ids2[0]));
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            descontarInventario(dgvMesa[0, i].Value.ToString(),Convert.ToDouble(dgvMesa[1, i].Value.ToString()));
                        }
                        
                    }
                    cmd = new OleDbCommand("delete from temp where 1;", conectar);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("INVENTARIO ACTUALIZADO CORRECTAMENTE", "INVENTARIO ACTUALIZADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();                
        }

        public void descontarInventario(string id, double cantidad)
        {
            if (id.Substring(0, 1) != "P")
            {
                try
                {
                    cmd = new OleDbCommand("select * from Inventario where Id=" + id + ";", conectar);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        for (int x = 3; x <= 21; x = x + 2)
                        {
                            cmd = new OleDbCommand("select * from Articulos where Id=" + Convert.ToString(reader[x].ToString()) + ";", conectar);
                            OleDbDataReader reader2 = cmd.ExecuteReader();
                            if (reader2.Read())
                            {
                                //MessageBox.Show("Read: " + reader[1].ToString());
                                double actual = Convert.ToDouble(reader2[2].ToString());
                                double quitar = Convert.ToDouble(reader[x + 1].ToString()) * cantidad;
                                double existencia = actual - quitar;
                                cmd = new OleDbCommand("select * from invent where idArticulo=" + Convert.ToString(reader[x].ToString()) + ";", conectar);
                                OleDbDataReader reader3 = cmd.ExecuteReader();
                                if (reader3.Read())
                                {
                                    string salida = "" + (Convert.ToDouble(Convert.ToString(reader3[4].ToString())) + quitar);
                                    cmd = new OleDbCommand("UPDATE invent set salida='" + salida + "' Where idArticulo=" + Convert.ToString(reader[x].ToString()) + ";", conectar);
                                    cmd.ExecuteNonQuery();
                                }
                                cmd = new OleDbCommand("UPDATE Articulos set Cantidad='" + existencia + "' Where Id=" + Convert.ToString(reader[x].ToString()) + ";", conectar);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            /*
            // Consulta principal para obtener los datos de Inventario REVISAR
            cmd = new OleDbCommand("select * from Inventario where Id=" + id + ";", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                // Variables para acumular las actualizaciones de salida e existencia
                double totalSalida = 0;
                double totalExistencia = 0;

                // Recorre los campos de 3 a 21 en incrementos de 2
                for (int x = 3; x <= 21; x += 2)
                {
                    // Obtén el Id del artículo
                    string idArticulo = Convert.ToString(reader[x].ToString());

                    // Consulta para obtener los datos del artículo
                    cmd = new OleDbCommand("select * from Articulos where Id=" + idArticulo + ";", conectar);
                    OleDbDataReader reader2 = cmd.ExecuteReader();

                    if (reader2.Read())
                    {
                        // Calcula las cantidades
                        double actual = Convert.ToDouble(reader2[2].ToString());
                        double quitar = Convert.ToDouble(reader[x + 1].ToString()) * cantidad;
                        double existencia = actual - quitar;

                        // Actualiza las variables totales
                        totalSalida += quitar;
                        totalExistencia += existencia;

                        // Actualiza la tabla 'invent'
                        cmd = new OleDbCommand("UPDATE invent set salida=salida+" + quitar + " Where idArticulo=" + idArticulo + ";", conectar);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        break;
                    }
                }

                // Actualiza la tabla 'Articulos' fuera del bucle
                cmd = new OleDbCommand("UPDATE Articulos set Cantidad=Cantidad-" + totalExistencia + " Where Id=" + Convert.ToString(reader["Id"].ToString()) + ";", conectar);
                cmd.ExecuteNonQuery();
            }*/


        }
       
    }
}
