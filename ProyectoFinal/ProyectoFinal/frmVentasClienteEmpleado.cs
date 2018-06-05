using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProyectoFinal
{
    public partial class frmVentasClienteEmpleado : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public frmVentasClienteEmpleado()
        {
            InitializeComponent();
        }

        void initData( int value)
        {
            //Limpiando el data grid view;
            gridLookUpEdit1.Properties.View.Columns.Clear();
            // SI ES 1 EMPLEADO 2 CLIENTE
            if (value == 1)
            {
                gridLookUpEdit1.Properties.DataSource = da.fillDataTable("SELECT tblVenta.id_empleado, tblEmpleado.nombre from tblVenta INNER JOIN tblEmpleado ON tblVenta.id_empleado = tblEmpleado.id_empleado GROUP BY nombre");
                gridLookUpEdit1.Properties.DisplayMember = "nombre";
                gridLookUpEdit1.Properties.ValueMember = "id_empleado";

            }

            else if (value == 2)
            {
                gridLookUpEdit1.Properties.DataSource = da.fillDataTable("SELECT tblVenta.id_cliente, tblCliente.nit, CONCAT(tblCliente.nombre,' ',tblCliente.apellido) as Nombre from tblVenta INNER JOIN tblCliente ON tblVenta.id_cliente = tblCliente.id_cliente GROUP BY id_cliente");
                gridLookUpEdit1.Refresh();
                gridLookUpEdit1.Properties.DisplayMember = "Nombre";
                gridLookUpEdit1.Properties.ValueMember = "id_cliente";

            }
            else
            {

            }



        }

        private void frmVentasClienteEmpleado_Load(object sender, EventArgs e)
        {
            //Llenando el grid con los valores para Clientes o para Empleados
            //PARA EMPLEADOS POR DEFAULT
            radioButtonEmpleado.Checked = true;
            initData(1);

        }

        private void radioButtonCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCliente.Checked == true)
            {
                initData(2);
            }
        }

        private void radioButtonEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEmpleado.Checked == true)
            {
                initData(1);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Obtenemos el empleado o cliente para generar reporte

            if (radioButtonCliente.Checked == true)
            {
                string empleado = gridLookUpEdit1.EditValue.ToString();

                //Enviamos al query del reporte
                reporteVentasClieneEmpleado miReporte = new reporteVentasClieneEmpleado();
                miReporte.InitData(empleado);
                documentViewer1.DocumentSource = miReporte;
                miReporte.CreateDocument();
            }
            else if (radioButtonEmpleado.Checked == true)
            {
                string empleado = gridLookUpEdit1.EditValue.ToString();
                reporteEmpleado miReporte = new reporteEmpleado();
                miReporte.InitData(empleado);
                documentViewer1.DocumentSource = miReporte;
                miReporte.CreateDocument();
            }
            else
            {
                MessageBox.Show("TRY AGAIN! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}