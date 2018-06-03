using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class XtraReportVentas : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportVentas()
        {
            InitializeComponent();
        }

        public void InitData(int idSucursal, DateTime fechaI, DateTime fechaF)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = fechaF;
            sqlDataSource1.Queries[0].Parameters[1].Value = fechaI;
            sqlDataSource1.Queries[0].Parameters[2].Value = idSucursal;

        }
    }
}
