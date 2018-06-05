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
    public partial class frmClientesMorosos : DevExpress.XtraEditors.XtraForm
    {
        public frmClientesMorosos()
        {
            InitializeComponent();
        }

        private void frmClientesMorosos_Load(object sender, EventArgs e)
        {
            reporteClientesMorosos miReporte = new reporteClientesMorosos();            
            documentViewer1.DocumentSource = miReporte;
            miReporte.InitData(DateTime.Now);
            miReporte.CreateDocument();
        }
    }
}