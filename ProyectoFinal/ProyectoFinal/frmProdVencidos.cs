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
    public partial class frmProdVencidos : DevExpress.XtraEditors.XtraForm
    {
        public frmProdVencidos()
        {
            InitializeComponent();
        }

        private void frmProdVencidos_Load(object sender, EventArgs e)
        {
            reporteProductosVencidos miReporte = new reporteProductosVencidos();
            documentViewer1.DocumentSource = miReporte;
            miReporte.InitData(DateTime.Today);
            miReporte.CreateDocument();
        }
    }
}