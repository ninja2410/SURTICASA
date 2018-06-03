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
    public partial class frmdelcategoria : Form
    {
        public frmdelcategoria()
        {
            InitializeComponent();
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
        private void frmdelcategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void eliminar(int cod, string nombre)
        {
            string sCommand;
            sCommand = "delete from tblCategoria where id_categoria='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Categoria " + nombre + " Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar la Categoria, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre De la Categoria"));
            int cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo De la Categoria"));

            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Categoria? " + nombre, "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { eliminar(cod, nombre); } else if (dialog == DialogResult.No) { }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
