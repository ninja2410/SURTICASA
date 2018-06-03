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
    public partial class XtraFormTop10Ventas : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public XtraFormTop10Ventas()
        {
            InitializeComponent();
        }

        private void XtraFormTop10Ventas_Load(object sender, EventArgs e)
        {
            //Cargamos las Sucursales
            //SUCURSALES            
            gridLookUpEditSucursales.Properties.DataSource = da.fillDataTable("SELECT nombre_sucursal from tblSucursal");
            gridLookUpEditSucursales.Properties.DisplayMember = "nombre_sucursal";
            gridLookUpEditSucursales.Properties.ValueMember = "nombre_sucursal";

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Obtenemos la sucursal a generar
            string sucursal = gridLookUpEditSucursales.EditValue.ToString();

            //Enviamos al query del reporte
            top10Report miReporte = new top10Report();
            miReporte.InitData(sucursal);
            viwerTop10.DocumentSource = miReporte;
            miReporte.CreateDocument();         


        }
    }
}