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
    public partial class frmCuentasxCobrar : DevExpress.XtraEditors.XtraForm
    {
        public frmCuentasxCobrar()
        {
            InitializeComponent();
        }

        private void frmCuentasxCobrar_Load(object sender, EventArgs e)
        {
            reporteCuentasxCobrar miReporte = new reporteCuentasxCobrar();
            documentViewer1.DocumentSource = miReporte;
            miReporte.CreateDocument();
            

        }
    }
}