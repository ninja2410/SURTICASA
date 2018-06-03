using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProyectoFinal
{
    public partial class f_ListUsers : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public f_ListUsers()
        {
            InitializeComponent();
        }

        private void f_ListUsers_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblEmpleado "; 
            DataTable dt = new DataTable();          
            dt = da.fillDataTable(query); 
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
    }
}