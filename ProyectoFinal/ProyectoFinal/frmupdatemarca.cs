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
    public partial class frmupdatemarca : Form
    {
        public frmupdatemarca()
        {
            InitializeComponent();
        }
        void cargar()
        {
            string query = "SELECT id_marca as 'Codigo Marca',nombre_marca as 'Nombre De la Marca',activo FROM tblMarca "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmupdatemarca_Load(object sender, EventArgs e)
        {
            cargar();
        }
        DataAccess da = new DataAccess();

        void mod()
        {
            string nombre,activo;
            if (checkEdit1.Checked)
            {
                activo = "1";
            }
            else
            { activo = "0"; }
            nombre = textEdit1.Text;
            int cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Marca"));

            string sCommand;
            sCommand = "UPDATE tblMarca SET nombre_marca='" + nombre + "', activo=" + Convert.ToByte(activo) + " WHERE id_marca='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Modifico La Marca " + nombre + " Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Modificar la Marca, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Modificar esta Marca?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { mod(); } else if (dialog == DialogResult.No) { }

        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            textEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre De la Marca"));
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
    }
}
