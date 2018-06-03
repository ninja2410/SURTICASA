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
    public partial class frmVentascs : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public frmVentascs()
        {
            InitializeComponent();
        }

        private void frmVentascs_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select id_sucursal, nombre_sucursal as Nombre from tblSucursal"; //Consulta que se enviara al servidor de la base
            try
            {
                // creando una nueva tabla
                dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
                gridLookUpSucursal.Properties.DataSource = dt;
                gridLookUpSucursal.Properties.DisplayMember = "Nombre";
                gridLookUpSucursal.Properties.ValueMember = "id_sucursal";

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al obtener las sucursales. \n Detalles del error: "+ex.Message);
            }
            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            int idSucursal = Convert.ToInt16(gridLookUpSucursal.EditValue);
            DateTime fechaDesde = Convert.ToDateTime(dateFrom.EditValue);
            DateTime fechaHasta = Convert.ToDateTime(dateTo.EditValue.ToString());
            string query = "CALL SP_VENTASSUCURSAL({0},'{1}','{2}')";
            try
            {
                query = string.Format(query, idSucursal, fechaDesde.ToString("yyyy-MM-dd"), fechaHasta.ToString("yyyy-MM-dd"));
                DataTable dt = new DataTable();
                dt = da.fillDataTable(query);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al extraer los datos de la base. \n Detalles del error: "+ ex.Message);
            }
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Leyendo los valores ingresados por el usuario.
            int idSucursal = Convert.ToInt16(gridLookUpSucursal.EditValue);
            DateTime fechaDesde = Convert.ToDateTime(dateFrom.EditValue);
            DateTime fechaHasta = Convert.ToDateTime(dateTo.EditValue.ToString());

            try
            {
                //Enviamos datos al reporte.
                frmPrintReport frm = new frmPrintReport();
                frm.PrintReport(idSucursal, fechaDesde, fechaHasta);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al generar el reporte. \n Detalles del error: "+ ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            
        }
    }
}