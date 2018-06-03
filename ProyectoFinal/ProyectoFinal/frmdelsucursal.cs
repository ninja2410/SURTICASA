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
    public partial class frmdelsucursal : Form
    {
        public frmdelsucursal()
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

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmdelsucursal_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void eliminar(int cod,string nombre)
        {
            string sCommand;
            sCommand = "delete from tblSucursal where id_sucursal='"+cod+"'";
            
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Sucursal " + nombre + " Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar la Sucursal, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Sucursal"));
            int cod= Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Numero Sucursal"));

            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Sucursal? "+nombre, "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { eliminar(cod,nombre); } else if (dialog == DialogResult.No) { }

        }
    }
}
