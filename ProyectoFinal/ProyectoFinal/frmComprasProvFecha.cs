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
    public partial class frmComprasProvFecha : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public frmComprasProvFecha()
        {
            InitializeComponent();
        }
            void initData(int value)
            {
                if (value == 1)
                { //CARGAMOS LOS PROVEEDORES DE LAS COMPRAS REALIZADAS
                    gridLookUpEdit1.Properties.View.Columns.Clear();
                    gridLookUpEdit1.Properties.DataSource = da.fillDataTable("select tblCompra.id_proveedor, tblProveedor.nombre_proveedor from tblCompra INNER JOIN tblProveedor ON tblCompra.id_proveedor = tblProveedor.id_proveedor group by id_proveedor");
                    gridLookUpEdit1.Properties.ValueMember = "id_proveedor";
                    gridLookUpEdit1.Properties.DisplayMember = "nombre_proveedor";
                }
                else { }
            }
        private void frmComprasProvFecha_Load(object sender, EventArgs e)
        {
            //cargamos los Proveedores por DEFAULT
            initData(1);
            radioButtonProveedores.Checked = true;
            radioButtonFechas.Checked = false;
           
        }

        private void radioButtonProveedores_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonProveedores.Checked == true)
            {
                dateEditDesde.Enabled = false;
                dateEditHasta.Enabled = false;
                gridLookUpEdit1.Enabled = true;
                initData(1);
            }
        }

        private void radioButtonFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFechas.Checked == true)
            {
                dateEditDesde.Enabled = true;
                dateEditHasta.Enabled = true;
                gridLookUpEdit1.Enabled = false;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (radioButtonProveedores.Checked == true)
            {
                reporteComprasProveedores miReporte = new reporteComprasProveedores();
                int proveedor = Convert.ToInt16( gridLookUpEdit1.EditValue);
                miReporte.InitData(proveedor);
                documentViewer1.DocumentSource = miReporte;
                miReporte.CreateDocument();
            }
            else if (radioButtonFechas.Checked == true)
            {
                reporteComprasFechas miReporte = new reporteComprasFechas();
                DateTime fechaDesde, fechaHasta;
                fechaDesde =  Convert.ToDateTime( dateEditDesde.EditValue);
                fechaHasta = Convert.ToDateTime(dateEditHasta.EditValue);
                miReporte.InitData(fechaDesde,fechaHasta);
                documentViewer1.DocumentSource = miReporte;
                miReporte.CreateDocument();

            }
            
        }
    }
}