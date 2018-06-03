using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class frmverpresentacion : Form
    {
        public frmverpresentacion()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT tipo_presentacion as 'Tipo De Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmverpresentacion_Load(object sender, EventArgs e)
        {
            cargar();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            agregarpresentacion a = new agregarpresentacion();
            a.ShowDialog();
            this.Close();
        }
    }
}
