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
    public partial class frmMenosVendidos : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public frmMenosVendidos()
        {
            InitializeComponent();
        }

        private void frmMenosVendidos_Load(object sender, EventArgs e)
        {
            gridLookUpEditSucursal.Properties.DataSource = da.fillDataTable("SELECT nombre_sucursal from tblSucursal");
            gridLookUpEditSucursal.Properties.DisplayMember = "nombre_sucursal";
            gridLookUpEditSucursal.Properties.ValueMember = "nombre_sucursal";

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //Obtenemos la sucursal a generar
            string sucursal = gridLookUpEditSucursal.EditValue.ToString();

            //Enviamos al query del reporte
            reporteMenosVendidos miReporte = new reporteMenosVendidos();
            miReporte.InitData(sucursal);
            documentViewer1.DocumentSource = miReporte;
            miReporte.CreateDocument();

        }
    }
}