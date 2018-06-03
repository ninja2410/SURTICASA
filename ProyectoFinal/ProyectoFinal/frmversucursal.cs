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
    public partial class frmversucursal : Form
    {
        public frmversucursal()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT id_sucursal as 'Numero Sucursal',nombre_sucursal as 'Nombre Sucursal',activo, direccion, (select nombre from tblEmpleado where id_empleado=tblSucursal.id_empleado) as 'Nombre Encargado', (select apellido from tblEmpleado where id_empleado=tblSucursal.id_empleado) as 'Apellido Encargado' FROM tblSucursal "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmversucursal_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
