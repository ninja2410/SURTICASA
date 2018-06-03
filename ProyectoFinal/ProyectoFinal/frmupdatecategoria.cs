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
    public partial class frmupdatecategoria : Form
    {
        public frmupdatecategoria()
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
        private void frmupdatecategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void mod()
        {
            string nombre,activo;
            nombre = textEdit1.Text;
            int cod= Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo De la Categoria"));
            if (checkEdit1.Checked)
            {
                activo = "1";
            }
            else
            { activo = "0"; }
            string sCommand;
            sCommand = "UPDATE tblCategoria SET nombre_categoria='" + nombre + "', activo="+Convert.ToByte(activo)+ " WHERE id_categoria='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Modifico La Categoria " + nombre + " Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Modificar la Categoria, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            textEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre De la Categoria"));
            int activo = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "activo"));
            if (activo == 1)
            {
                checkEdit1.Checked = true;
            }
            else
            {
                checkEdit1.Checked = false;
            }
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Modificar esta Categoria?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { mod(); } else if (dialog == DialogResult.No) { }

        }
    }
}
