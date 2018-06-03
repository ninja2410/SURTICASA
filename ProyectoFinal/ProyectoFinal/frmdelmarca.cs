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
    public partial class frmdelmarca : Form
    {
        public frmdelmarca()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT id_marca as 'Codigo Marca',nombre_marca as 'Nombre De la Marca',activo FROM tblMarca "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmdelmarca_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void eliminar(int cod, string nombre)
        {
            string sCommand;
            sCommand = "delete from tblMarca where id_marca='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Marca " + nombre + " Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar la Marca, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre De la Marca"));
            int cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Marca"));

            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Marca? " + nombre, "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { eliminar(cod, nombre); } else if (dialog == DialogResult.No) { }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
