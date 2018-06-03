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
    public partial class frmVerAsignacionPermisos : Form
    {
        DataAccess da = new DataAccess();
        public frmVerAsignacionPermisos()
        {
            InitializeComponent();
        }
        void cargar()
        {
            string query = "SELECT nombre_rol as 'Nombre Del Rol',activo as 'Activo' FROM tblRoles "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmVerAsignacionPermisos_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
