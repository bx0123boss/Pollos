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
    public partial class frmArmaCombo : Form
    {
        private DataSet ds;
        OleDbConnection conectar = new OleDbConnection(Conexion.CadCon);
        OleDbDataAdapter da;
      
        public frmArmaCombo()
        {
            InitializeComponent();
        }

        private void frmArmaCombo_Load(object sender, EventArgs e)
        {
            conectar.Open();
            ds = new DataSet();
            da = new OleDbDataAdapter("select * from Promos ORDER BY Nombre where;", conectar);
            da.Fill(ds, "Id");
            dgvInventario.DataSource = ds.Tables["Id"];
        }
    }
}
