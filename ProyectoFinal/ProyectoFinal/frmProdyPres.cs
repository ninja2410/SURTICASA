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
    public partial class frmProdyPres : DevExpress.XtraEditors.XtraForm
    {
        public frmProdyPres()
        {
            InitializeComponent();
        }

        private void frmProdyPres_Load(object sender, EventArgs e)
        {
            reporteProdyPres miReporte = new reporteProdyPres();
            documentViewer1.DocumentSource = miReporte;
            miReporte.CreateDocument();
        }
    }
}