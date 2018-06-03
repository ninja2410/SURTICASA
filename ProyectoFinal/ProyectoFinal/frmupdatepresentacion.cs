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
    public partial class frmupdatepresentacion : Form
    {
        public frmupdatepresentacion()
        {
            InitializeComponent();
        }
        void cargar()
        {
            string query = "SELECT id_Presentacion as 'Codigo Presentacion',tipo_presentacion as 'Tipo De Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmupdatepresentacion_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        DataAccess da = new DataAccess();
        void  mod()
        {

            string nombre;
            nombre = textEdit1.Text;
            int cod;
            cod= Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Presentacion"));
            string sCommand;
            sCommand = "UPDATE tblPresentacion SET tipo_presentacion='" + nombre + "' WHERE id_Presentacion='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Modifico La Presentacion " + nombre + " Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Modificar la Presentacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Modificar esta Presentacion?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { mod(); } else if (dialog == DialogResult.No) { }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            simpleButton2.Enabled = true;
        string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tipo De Presentacion"));
            textEdit1.Text = nombre;
            textEdit1.Focus();
        }
    }
}
