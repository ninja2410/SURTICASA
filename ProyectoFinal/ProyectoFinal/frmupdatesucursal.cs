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
    public partial class frmupdatesucursal : Form
    {
        public frmupdatesucursal()
        {
            InitializeComponent();
        }
        int cod_sucursal = 0;
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            textEdit2.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Sucursal"));
            int activo= Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "activo"));
            if(activo==1)
            {
                checkEdit1.Checked = true;
            }
            else
            {
                checkEdit1.Checked = false;
            }
            textEdit1.Text= Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "direccion"));
            lookUpEdit1.Text= Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Encargado"));
            cod_sucursal=Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Numero Sucursal"));


        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT id_sucursal as 'Numero Sucursal',nombre_sucursal as 'Nombre Sucursal',activo, direccion,id_empleado as 'Cod Empleado', (select nombre from tblEmpleado where id_empleado=tblSucursal.id_empleado) as 'Nombre Encargado', (select apellido from tblEmpleado where id_empleado=tblSucursal.id_empleado) as 'Apellido Encargado' FROM tblSucursal "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        void cargar_combo()
        {
            string query = "select id_empleado,nombre from tblEmpleado"; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.DisplayMember = "nombre";
            lookUpEdit1.Properties.ValueMember = "id_empleado";
            

        }
        private void frmupdatesucursal_Load(object sender, EventArgs e)
        {
            cargar();
            cargar_combo();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Modificar esta Sucursal?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { mod(); } else if (dialog == DialogResult.No) { }

        }
        void mod()
        {
            string nombre, direcion, encargado;
            nombre = textEdit2.Text;
            direcion=textEdit1.Text;
            
            string sCommand;
            sCommand = "UPDATE tblSucursal SET nombre_sucursal='"+ nombre +"', direccion='"+direcion+ "',id_empleado='"+lookUpEdit1.EditValue+"' WHERE id_sucursal='"+cod_sucursal+"'";
            
             try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Modifico La Sucursal " + nombre + " Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Modificar la Sucursal, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
