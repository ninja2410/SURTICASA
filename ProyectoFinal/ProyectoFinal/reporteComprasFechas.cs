using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteComprasFechas : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteComprasFechas()
        {
            InitializeComponent();
        }

        public void InitData(DateTime fDesde, DateTime fHasta)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = fDesde;
            sqlDataSource1.Queries[0].Parameters[1].Value = fHasta;

        }

    }
}
