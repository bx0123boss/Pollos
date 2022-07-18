using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.IO;

namespace Punto_Venta
{
    public partial class frmPlatillos : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon); 
        OleDbDataAdapter da;
        OleDbCommand cmd2;
        OleDbCommand cmd;
        
        public frmPlatillos()
        {
            InitializeComponent();
            conectar.Open();
        }

        private void frmPlatillos_Load(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = false;
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda, Subcategoria from Inventario where Categoria='" + comboBox2.Text + "' order by Nombre;", conectar);
                da.Fill(ds, "Id");
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from inventario ORDER BY Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
            }
            dgvInventario.Columns[0].Visible = false;

            //dgvInventario.Columns[3].Visible = false;
            //dgvInventario.Columns[4].Visible = false;
            //dgvInventario.Columns[5].Visible = false;
            //dgvInventario.Columns[6].Visible = false;
            //dgvInventario.Columns[7].Visible = false;
            //dgvInventario.Columns[8].Visible = false;
            //dgvInventario.Columns[9].Visible = false;
            //dgvInventario.Columns[10].Visible = false;
            //dgvInventario.Columns[11].Visible = false;
            //dgvInventario.Columns[12].Visible = false;
            //dgvInventario.Columns[13].Visible = false;
            //dgvInventario.Columns[14].Visible = false;
            //dgvInventario.Columns[15].Visible = false;
            //dgvInventario.Columns[16].Visible = false;
            //dgvInventario.Columns[17].Visible = false;
            //dgvInventario.Columns[18].Visible = false;
            //dgvInventario.Columns[19].Visible = false;
            //dgvInventario.Columns[20].Visible = false;
            //dgvInventario.Columns[21].Visible = false;
            //dgvInventario.Columns[22].Visible = false;
            DataTable dt = new DataTable();
            cmd = new OleDbCommand("SELECT * from Categorias;", conectar);
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            comboBox2.DisplayMember = "Nombre";
            comboBox2.ValueMember = "Id";
            comboBox2.DataSource = dt;     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
            platillo.lista = comboBox2.Text;
            platillo.Text = "Agregar Platillo";
            platillo.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAgregarPlatillo platillo = new frmAgregarPlatillo();
           
            platillo.Text = "Editar Platillo";
            platillo.id = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.txtNombre.Text = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.txtPrecio.Text = dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.cat1 = dgvInventario[3, dgvInventario.CurrentRow.Index].Value.ToString();
            platillo.cat2 = dgvInventario[5, dgvInventario.CurrentRow.Index].Value.ToString();
            if (dgvInventario[4, dgvInventario.CurrentRow.Index].Value.ToString() == "1")
            {
                platillo.checkBox1.Checked = true;
            }
            
            string idInvent = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
            cmd2 = new OleDbCommand("select * from Inventario where Id=" + idInvent + ";", conectar);
            OleDbDataReader invent = cmd2.ExecuteReader();
            if (invent.Read())
            {
                string id = invent[3].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                OleDbDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo1 = id;
                    platillo.Nombre1 = Convert.ToString(reader[1].ToString());
                    platillo.Medida1 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad.Text = invent[4].ToString();
                    platillo.Precio1 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio1.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[4].ToString());
                }
                id = invent[5].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo2 = id;
                    platillo.Nombre2 = Convert.ToString(reader[1].ToString());
                    platillo.Medida2 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre2.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida2.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad2.Text = invent[6].ToString();
                    platillo.Precio2 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio2.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[6].ToString());
                }
                id = invent[7].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo3 = id;
                    platillo.Nombre3 = Convert.ToString(reader[1].ToString());
                    platillo.Medida3 = Convert.ToString(reader[3].ToString());
                    platillo.lblNombre3.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida3.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad3.Text = invent[8].ToString();
                    platillo.Precio3 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio3.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[8].ToString());
                }
                id = invent[9].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo4 = id;
                    platillo.Nombre4 = Convert.ToString(reader[1].ToString());
                    platillo.Medida4 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre4.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida4.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad4.Text = invent[10].ToString();
                    platillo.Precio4 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio4.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[10].ToString());
                }
                id = invent[11].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo5 = id;
                    platillo.Nombre5 = Convert.ToString(reader[1].ToString());
                    platillo.Medida5 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre5.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida5.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad5.Text = invent[12].ToString();
                    platillo.Precio5 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio5.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[12].ToString());
                }
                id = invent[13].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo6 = id;
                    platillo.Nombre6 = Convert.ToString(reader[1].ToString());
                    platillo.Medida6 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre6.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida6.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad6.Text = invent[14].ToString();
                    platillo.Precio6 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio6.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[14].ToString());
                }
                id = invent[15].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo7 = id;
                    platillo.Nombre7 = Convert.ToString(reader[1].ToString());
                    platillo.Medida7 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre7.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida7.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad7.Text = invent[16].ToString();
                    platillo.Precio7 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio7.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[16].ToString());
                }
                id = invent[17].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo8 = id;
                    platillo.Nombre8 = Convert.ToString(reader[1].ToString());
                    platillo.Medida8 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre8.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida8.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad8.Text = invent[18].ToString();
                    platillo.Precio8 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio8.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[18].ToString());
                }
                id = invent[19].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo9 = id;
                    platillo.Nombre9 = Convert.ToString(reader[1].ToString());
                    platillo.Medida9 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre9.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida9.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad9.Text = invent[20].ToString();
                    platillo.Precio9 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio9.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[20].ToString());
                }
                id = invent[21].ToString();
                cmd2 = new OleDbCommand("select * from Articulos where Id=" + id + ";", conectar);
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    platillo.idArticulo10 = id;
                    platillo.Nombre10 = Convert.ToString(reader[1].ToString());
                    platillo.Medida10 = Convert.ToString(reader[2].ToString());
                    platillo.lblNombre10.Text = Convert.ToString(reader[1].ToString());
                    platillo.lblMedida10.Text = Convert.ToString(reader[3].ToString());
                    platillo.txtCantidad10.Text = invent[22].ToString();
                    platillo.Precio10 = Convert.ToString(reader[5].ToString());
                    platillo.lblPrecio10.Text = "" + Convert.ToDouble(Convert.ToString(reader[5].ToString())) * Convert.ToDouble(invent[22].ToString());
                }
            }
            platillo.lblTotal.Text = "" + (Convert.ToDouble(platillo.lblPrecio1.Text) + Convert.ToDouble(platillo.lblPrecio2.Text) + Convert.ToDouble(platillo.lblPrecio3.Text) + Convert.ToDouble(platillo.lblPrecio4.Text) + Convert.ToDouble(platillo.lblPrecio5.Text) + Convert.ToDouble(platillo.lblPrecio6.Text) + Convert.ToDouble(platillo.lblPrecio7.Text) + Convert.ToDouble(platillo.lblPrecio8.Text) + Convert.ToDouble(platillo.lblPrecio9.Text) + Convert.ToDouble(platillo.lblPrecio10.Text));
            conectar.Close();
            platillo.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd2 = new OleDbCommand("delete from inventario where Id=" + dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString() + ";", conectar);
            cmd2.ExecuteNonQuery();
            //cmd = new MySqlCommand("delete from inventario where Id='" + dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString() + "';", Conexion.obtenerConexion());
            //cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha eliminado con exito", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ds = new DataSet();
            da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda from inventario ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];
            //dgvInventario.Columns[0].Visible = false;
            //dgvInventario.Columns[3].Visible = false;
            //dgvInventario.Columns[4].Visible = false;
            //dgvInventario.Columns[5].Visible = false;
            //dgvInventario.Columns[6].Visible = false;
            //dgvInventario.Columns[7].Visible = false;
            //dgvInventario.Columns[8].Visible = false;
            //dgvInventario.Columns[9].Visible = false;
            //dgvInventario.Columns[10].Visible = false;
            //dgvInventario.Columns[11].Visible = false;
            //dgvInventario.Columns[12].Visible = false;
            //dgvInventario.Columns[13].Visible = false;
            //dgvInventario.Columns[14].Visible = false;
            //dgvInventario.Columns[15].Visible = false;
            //dgvInventario.Columns[16].Visible = false;
            //dgvInventario.Columns[17].Visible = false;
            //dgvInventario.Columns[18].Visible = false;
            //dgvInventario.Columns[19].Visible = false;
            //dgvInventario.Columns[20].Visible = false;
            //dgvInventario.Columns[21].Visible = false;
            //dgvInventario.Columns[22].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from inventario ORDER BY Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
                //dgvInventario.Columns[0].Visible = false;
                //dgvInventario.Columns[3].Visible = false;
                //dgvInventario.Columns[4].Visible = false;
                //dgvInventario.Columns[5].Visible = false;
                //dgvInventario.Columns[6].Visible = false;
                //dgvInventario.Columns[7].Visible = false;
                //dgvInventario.Columns[8].Visible = false;
                //dgvInventario.Columns[9].Visible = false;
                //dgvInventario.Columns[10].Visible = false;
                //dgvInventario.Columns[11].Visible = false;
                //dgvInventario.Columns[12].Visible = false;
                //dgvInventario.Columns[13].Visible = false;
                //dgvInventario.Columns[14].Visible = false;
                //dgvInventario.Columns[15].Visible = false;
                //dgvInventario.Columns[16].Visible = false;
                //dgvInventario.Columns[17].Visible = false;
                //dgvInventario.Columns[18].Visible = false;
                //dgvInventario.Columns[19].Visible = false;
                //dgvInventario.Columns[20].Visible = false;
                //dgvInventario.Columns[21].Visible = false;
                //dgvInventario.Columns[22].Visible = false;
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from inventario where Nombre LIKE '%" + textBox1.Text + "%';", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
                //dgvInventario.Columns[0].Visible = false;
                //dgvInventario.Columns[3].Visible = false;
                //dgvInventario.Columns[4].Visible = false;
                //dgvInventario.Columns[5].Visible = false;
                //dgvInventario.Columns[6].Visible = false;
                //dgvInventario.Columns[7].Visible = false;
                //dgvInventario.Columns[8].Visible = false;
                //dgvInventario.Columns[9].Visible = false;
                //dgvInventario.Columns[10].Visible = false;
                //dgvInventario.Columns[11].Visible = false;
                //dgvInventario.Columns[12].Visible = false;
                //dgvInventario.Columns[13].Visible = false;
                //dgvInventario.Columns[14].Visible = false;
                //dgvInventario.Columns[15].Visible = false;
                //dgvInventario.Columns[16].Visible = false;
                //dgvInventario.Columns[17].Visible = false;
                //dgvInventario.Columns[18].Visible = false;
                //dgvInventario.Columns[19].Visible = false;
                //dgvInventario.Columns[20].Visible = false;
                //dgvInventario.Columns[21].Visible = false;
                //dgvInventario.Columns[22].Visible = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = false;
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from Inventario where Categoria='" + comboBox2.Text + "' order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
            //    dgvInventario.Columns[0].Visible = false;
            //    dgvInventario.Columns[3].Visible = false;
            //    dgvInventario.Columns[4].Visible = false;
            //    dgvInventario.Columns[5].Visible = false;
            //    dgvInventario.Columns[6].Visible = false;
            //    dgvInventario.Columns[7].Visible = false;
            //    dgvInventario.Columns[8].Visible = false;
            //    dgvInventario.Columns[9].Visible = false;
            //    dgvInventario.Columns[10].Visible = false;
            //    dgvInventario.Columns[11].Visible = false;
            //    dgvInventario.Columns[12].Visible = false;
            //    dgvInventario.Columns[13].Visible = false;
            //    dgvInventario.Columns[14].Visible = false;
            //    dgvInventario.Columns[15].Visible = false;
            //    dgvInventario.Columns[16].Visible = false;
            //    dgvInventario.Columns[17].Visible = false;
            //    dgvInventario.Columns[18].Visible = false;
            //    dgvInventario.Columns[19].Visible = false;
            //    dgvInventario.Columns[20].Visible = false;
            //    dgvInventario.Columns[21].Visible = false;
            //    dgvInventario.Columns[22].Visible = false;
            }
            else
            {
                textBox1.Enabled = true;
                comboBox2.SelectedIndex = 0;
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from Inventario order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
                //dgvInventario.Columns[0].Visible = false;
                //dgvInventario.Columns[3].Visible = false;
                //dgvInventario.Columns[4].Visible = false;
                //dgvInventario.Columns[5].Visible = false;
                //dgvInventario.Columns[6].Visible = false;
                //dgvInventario.Columns[7].Visible = false;
                //dgvInventario.Columns[8].Visible = false;
                //dgvInventario.Columns[9].Visible = false;
                //dgvInventario.Columns[10].Visible = false;
                //dgvInventario.Columns[11].Visible = false;
                //dgvInventario.Columns[12].Visible = false;
                //dgvInventario.Columns[13].Visible = false;
                //dgvInventario.Columns[14].Visible = false;
                //dgvInventario.Columns[15].Visible = false;
                //dgvInventario.Columns[16].Visible = false;
                //dgvInventario.Columns[17].Visible = false;
                //dgvInventario.Columns[18].Visible = false;
                //dgvInventario.Columns[19].Visible = false;
                //dgvInventario.Columns[20].Visible = false;
                //dgvInventario.Columns[21].Visible = false;
                //dgvInventario.Columns[22].Visible = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from Inventario where Categoria='" + comboBox2.Text + "' order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
                dgvInventario.Columns[0].Visible = false;
            }
            else
            {
                ds = new DataSet();
                da = new OleDbDataAdapter("select id,Nombre,Precio,Categoria,Comanda,Subcategoria from Inventario order by Nombre;", conectar);
                da.Fill(ds, "Id");
                dgvInventario.DataSource = ds.Tables["Id"];
                dgvInventario.Columns[0].Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmGastos gas = new frmGastos();
            gas.Show();
            this.Close();
        }

        private void dgvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
