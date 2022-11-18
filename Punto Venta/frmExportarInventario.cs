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
    public partial class frmExportarInventario : Form
    {
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbConnection conn;
        OleDbDataAdapter MyDataAdapter;
        DataTable dt;
        OleDbCommand cmd;

        public frmExportarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            importarExcel(dataGridView1, "Hoja1");
        }

        public void importarExcel(DataGridView dgv, String nombreHoja)
        {
            try
            {
                String ruta = "";
                OpenFileDialog openfile1 = new OpenFileDialog();
                openfile1.Filter = "Excel Files |*.xlsx";
                openfile1.Title = "Seleccione el archivo de Excel";
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openfile1.FileName.Equals("") == false)
                    {
                        ruta = openfile1.FileName;
                    }
                }

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'");
                MyDataAdapter = new OleDbDataAdapter("Select ID, Nombre, Existen from [" + nombreHoja + "$]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch 
            { }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conectar.Open();                       
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {                
                cmd = new OleDbCommand("Update Articulos set Cantidad='" + dataGridView1[2, i].Value.ToString() + "' where Id=" + dataGridView1[0, i].Value.ToString() + ";", conectar);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("El inventario se ha actualizado con exito", "Exito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
