using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class top10Report : DevExpress.XtraReports.UI.XtraReport
    {
        public top10Report()
        {
            InitializeComponent();
        }

        public void InitData(string nameSucursal)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = nameSucursal;

        }
    }
}
