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
    public partial class frmdelpresentacion : Form
    {
        public frmdelpresentacion()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;

       }
        DataAccess da = new DataAccess();
        void eliminar(int cod, string nombre)
        {
            string sCommand;
            sCommand = "delete from tblPresentacion where id_Presentacion='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Presentacion " + nombre + " Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar la Presentacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void cargar()
        {
            string query = "SELECT id_Presentacion as 'Codigo',tipo_presentacion as 'Tipo De Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tipo De Presentacion"));
            int cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo"));

            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Presentacion? " + nombre, "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { eliminar(cod, nombre); } else if (dialog == DialogResult.No) { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmdelpresentacion_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
