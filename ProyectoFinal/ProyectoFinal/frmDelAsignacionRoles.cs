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
    public partial class frmDelAsignacionRoles : Form
    {
        public frmDelAsignacionRoles()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT id_rol as 'codigo rol',nombre_rol as 'Nombre Del Rol',activo as 'Activo' FROM tblRoles "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmDelAsignacionRoles_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void eliminar(string cod)
        {
            string sCommand;
            sCommand = "delete from tblRoles where id_rol='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Asignacion Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar La Asignacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string cod = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "codigo rol"));

                DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Asignacion De Rol? " + cod, "Cancelar", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) { eliminar(cod); } else if (dialog == DialogResult.No) { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
