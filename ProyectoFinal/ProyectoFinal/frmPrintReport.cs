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
    public partial class frmPrintReport : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintReport()
        {
            InitializeComponent();
        }

        public void PrintReportTop10()
        {
            //XtraReportTop10Ventas report = new XtraReportTop10Ventas();
            //documentViewer1.DocumentSource = report;
            //report.CreateDocument();
        }

        public void PrintReport(int sucursal, DateTime fechaInicio, DateTime fechaFin)
        {
            XtraReportVentas report = new XtraReportVentas();
            report.InitData(sucursal, fechaInicio, fechaFin);

            documentViewer1.DocumentSource = report;
            report.CreateDocument();

        }

        private void frmPrintReport_Load(object sender, EventArgs e)
        {

        }
    }
}