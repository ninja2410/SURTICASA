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
    public partial class frmvercategoria : Form
    {
        public frmvercategoria()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {

            string query = "SELECT id_categoria as 'Codigo De la Categoria', nombre_categoria as 'Nombre De la Categoria', activo FROM tblCategoria "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmvercategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
