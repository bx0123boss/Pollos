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
    public partial class frmAgregarPromo : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
        OleDbCommand cmd;
        int suma;
        public string id;
        double total = 0;
        bool lunes, martes, miercoles, jueves, viernes, sabado, domingo;
        public frmAgregarPromo()
        {
            InitializeComponent();
        }
        public void obtenerYSumar()
        {
            cmd = new OleDbCommand("select Numero from Folio where Folio='Combo';", conectar);
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                suma = Convert.ToInt32(Convert.ToString(reader[0].ToString()));
            }
        }

        public void obtenerYSumar2()
        {
            suma = suma + 1;
            cmd = new OleDbCommand("UPDATE Folio set Numero=" + suma + " where Folio='Combo';", conectar);
            cmd.ExecuteNonQuery();
        }        

        private void frmAgregarPromo_Load(object sender, EventArgs e)
        {
            conectar.Open();
            if (id != null)
            {
                cmd = new OleDbCommand("select * from ArticulosPromos where IdPromo='"+id+"';", conectar);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DgvPedidoprevio.Rows.Add(reader[2].ToString(), reader[3].ToString());
                    
                }
                this.Text = "Editar";
                
            }
            ds = new DataSet();
            da = new OleDbDataAdapter("select id,Nombre from Subcategoria ORDER BY Nombre;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];
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

        private void button1_Click(object sender, EventArgs e)
        {
            //if (this.Text == "Editar")
            //{
            //    bool ya = false;
            //    string nombre = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
            //    for (int i = 0; i < dgvEditar.RowCount; i++)
            //    {
            //        if (nombre == dgvEditar[3, i].Value.ToString())
            //            ya = true;
            //    }
            //    if (ya)
            //        MessageBox.Show("No puedes poner varias veces la subcategoria, solo cambia la cantidad", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    else
            //        dgvEditar.Rows.Add("1","1","1", nombre);
            //}
            //else
            //{
                bool ya = false;
                string nombre = dgvInventario[1, dgvInventario.CurrentRow.Index].Value.ToString();
                for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                {
                    if (nombre == DgvPedidoprevio[1, i].Value.ToString())
                        ya = true;
                }
                if (ya)
                    MessageBox.Show("No puedes poner varias veces la subcategoria, solo cambia la cantidad", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    DgvPedidoprevio.Rows.Add("1", nombre);
            //}

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
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBuscar.Text != "")
                {
                    ds = new DataSet();
                    da = new OleDbDataAdapter("select id,Nombre from subcategoria where Nombre LIKE '%" + txtBuscar.Text + "%' ORDER BY Nombre;", conectar);
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];
                }
                else
                {
                    ds = new DataSet();
                    da = new OleDbDataAdapter("select id,Nombre from Subcategoria ORDER BY Nombre;", conectar);
                    da.Fill(ds, "Id");
                    dgvInventario.DataSource = ds.Tables["Id"];
                }
            }
        }

        private void DgvPedidoprevio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                double cantidad = Convert.ToDouble(DgvPedidoprevio[0, DgvPedidoprevio.CurrentRow.Index].Value.ToString());
            }
            catch (Exception ex)
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
                lunes = cbLunes.Checked;
                martes = cbMartes.Checked;
                miercoles = cbMiercoles.Checked;
                jueves = cbJueves.Checked;
                viernes = cbViernes.Checked;
                sabado = cbSabado.Checked;
                domingo = cbDomingo.Checked;
                string upadd = "";
                if (this.Text == "Editar")
                {
                    upadd = "actualizado";
                    cmd = new OleDbCommand("delete from ArticulosPromos where IdPromo='" + id + "';", conectar);
                    cmd.ExecuteNonQuery();
                    cmd = new OleDbCommand("UPDATE Promos set Nombre='" + txtNombre.Text + "',Precio='" + txtPrecio.Text + "', Lunes=" + lunes + ",Martes=" + martes + ",Miércoles=" + miercoles + ",Jueves=" + jueves + ",Viernes=" + viernes + ", Sábado=" + sabado + ",Domingo=" + domingo + " where Id='" + id + "';", conectar);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                    {

                        string Cantidad = DgvPedidoprevio[0, i].Value.ToString();
                        string Nombre = DgvPedidoprevio[1, i].Value.ToString();
                        cmd = new OleDbCommand("INSERT INTO ArticulosPromos(IdPromo, Cantidad, Nombre) VALUES ('" + id + "'," + Cantidad + ",'" + Nombre + "');", conectar);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    upadd = "agregado";
                    obtenerYSumar();
                    cmd = new OleDbCommand("INSERT INTO Promos VALUES ('C" + suma + "','" + txtNombre.Text + "'," + txtPrecio.Text + "," + lunes + "," + martes + "," + miercoles + "," + jueves + "," + viernes + "," + sabado + "," + domingo + ");", conectar);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < DgvPedidoprevio.RowCount; i++)
                    {

                        string Cantidad = DgvPedidoprevio[0, i].Value.ToString();
                        string Nombre = DgvPedidoprevio[1, i].Value.ToString();
                        cmd = new OleDbCommand("INSERT INTO ArticulosPromos(IdPromo, Cantidad, Nombre) VALUES ('C" + suma + "'," + Cantidad + ",'" + Nombre + "');", conectar);
                        cmd.ExecuteNonQuery();
                    }
                    obtenerYSumar2();                   
                }
                MessageBox.Show("Se ha "+upadd+" la promoción correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmCombos com = new frmCombos();
                com.Show();
                this.Close();
            }
            else
                MessageBox.Show("FALTAN DATOS PARA COMPLETAR LA PROMOCION", "Alto", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
