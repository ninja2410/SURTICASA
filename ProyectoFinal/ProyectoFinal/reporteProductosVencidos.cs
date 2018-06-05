using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteProductosVencidos : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteProductosVencidos()
        {
            InitializeComponent();
        }

        public void InitData(DateTime today)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = today;
        }

    }
}
