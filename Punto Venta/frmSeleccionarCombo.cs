using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Venta
{
    public partial class frmSeleccionarCombo : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        public string id { get; set; }
        public string cantidad { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public double total { get; set; }
        public frmSeleccionarCombo()
        {
            InitializeComponent();
        }

        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPromo.Rows.Clear();
            articulosPromo(dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString());
        }

        private void frmSeleccionarCombo_Load(object sender, EventArgs e)
        {
            conectar.Open();
            ds = new DataSet();
            da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE NOT Subcategoria='' ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dataGridView1.DataSource = ds.Tables["Id"];
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Promos where " + DateTime.Now.ToString("dddd") + "=True ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];
            dgvInventario.Columns[0].Visible = false;
            articulosPromo(dgvInventario[0, 0].Value.ToString());
            dgvInventario.Rows[0].Selected = true;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBuscar.Text != "")
                    da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE Nombre LIKE '%" + txtBuscar.Text + "%' ORDER BY Nombre;", conectar);                
                else
                    da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario ORDER BY Nombre;", conectar);
                ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                comparaCategorias();
                if (todoOK())
                {
                    id = dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString();
                    nombre = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
                    cantidad = lblCant.Text;
                    precio = Convert.ToDouble(dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString());
                    total = Convert.ToDouble(dgvInventario[2, dgvInventario.CurrentRow.Index].Value.ToString()) * Convert.ToDouble(cantidad);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;                           
                }
                else
                {
                    MessageBox.Show("FALTAN ELEMENTOS DEL COMBO, FAVOR DE CHECAR", "ALTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch {
                MessageBox.Show("NO HA SELECCIONADO COMBO", "ALTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error " + ex.ToString(), "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string nombre = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string categoria = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            DgvPedidoprevio.Rows.Add(id,"1", nombre,categoria,false,"1");
            //da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario ORDER BY Nombre;", conectar);
            //ds = new DataSet();
            //da.Fill(ds, "Id");
            //dataGridView1.DataSource = ds.Tables["Id"];
            //txtBuscar.Clear();
            comparaCategorias();
        }

        private void dgvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPromo.Rows.Clear();
            articulosPromo(dgvInventario[0, dgvInventario.CurrentRow.Index].Value.ToString());
            
        }
        public void comparaCategorias() 
        {
            for (int x = 0; x < dgvPromo.RowCount; x++)
            {
                dgvPromo[3, x].Value = dgvPromo[0, x].Value.ToString();
            }
            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                string categoria = DgvPedidoprevio[3, i].Value.ToString();
                double cantidad = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                for (int x = 0; x < dgvPromo.RowCount; x++)
                {
                    if (categoria == dgvPromo[1, x].Value.ToString())
                    {
                        double cantidadPromo = Convert.ToDouble(dgvPromo[3, x].Value.ToString());
                        double cantidadActual = cantidadPromo - cantidad;                       
                        dgvPromo[3, x].Value = cantidadActual;
                        //break;
                    }
                }
            }


            for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
            {
                string categoria = DgvPedidoprevio[3, i].Value.ToString();
                double cantidad = Convert.ToDouble(DgvPedidoprevio[1, i].Value.ToString());
                for (int x = 0; x < dgvPromo.RowCount; x++)
                {
                    if (categoria == dgvPromo[1, x].Value.ToString())
                    {
                        double cantidadPromo = Convert.ToDouble(dgvPromo[3, x].Value.ToString());
                        if (cantidadPromo == 0)
                        {
                            DgvPedidoprevio[5, i].Value = cantidadPromo;
                            DgvPedidoprevio[4, i].Value = true;
                            dgvPromo[2, x].Value = true;
                            //break;
                        }
                        else
                        {
                            DgvPedidoprevio[4, i].Value = false;
                            dgvPromo[2, x].Value = false;
                            DgvPedidoprevio[5, i].Value = cantidadPromo;
                            //break;
                        }
                    }
                }
            }
        }
        public void articulosPromo(string id)
        {
            string Articulos = "";
            cmd = new OleDbCommand("SELECT * FROM ArticulosPromos where IdPromo='" + id + "';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Articulos += reader[2].ToString() + ":" + reader[3].ToString() + "\n";
                dgvPromo.Rows.Add(reader[2].ToString(), reader[3].ToString(), false, reader[2].ToString());

            }
            //MessageBox.Show(Articulos, "La promoción incluye:");
        }
        public bool todoOK()
        {
            bool ok=true;
           
            for (int i = 0; i < dgvPromo.RowCount; i++)
            {
                if (dgvPromo[2, i].Value.ToString() == "False")
                {
                    ok = false;
                    break;
                }
            }
            if (DgvPedidoprevio.RowCount==0)
            {
                ok = false;
            }
            for (int x = 0; x < DgvPedidoprevio.RowCount; x++)
            {
                if (DgvPedidoprevio[4, x].Value.ToString() == "False")
                {
                    ok = false;
                    break;
                }

            }
            return ok;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DgvPedidoprevio.Rows.RemoveAt(DgvPedidoprevio.CurrentRow.Index);
                comparaCategorias();
            }
            catch
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dgvPromo[2, dgvPromo.CurrentRow.Index].Value.ToString());
        }

        private void DgvPedidoprevio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double cantidad = Convert.ToDouble(DgvPedidoprevio[0, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
                comparaCategorias();
            }
            catch 
            {
                MessageBox.Show("Solo puedes introducir números", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DgvPedidoprevio.Rows[e.RowIndex].Cells[1].Value = "1"; 
                comparaCategorias();
            }
        }

        private void dgvPromo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            da = new OleDbDataAdapter("select Id,Nombre,Precio,Subcategoria from Inventario WHERE Subcategoria='" + dgvPromo[1, dgvPromo.CurrentRow.Index].Value.ToString() + "' ORDER BY Nombre;", conectar);               
                ds = new DataSet();
                da.Fill(ds, "Id");
                dataGridView1.DataSource = ds.Tables["Id"];            
        }

        private void lblCant_Click(object sender, EventArgs e)
        {
            using (frmCantidad ori = new frmCantidad())
            {
                if (ori.ShowDialog() == DialogResult.OK)
                {
                    lblCant.Text = Math.Round(Convert.ToDouble(ori.cantidad)) + "";
                }
            }
        }

    }
}
