using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteKardex : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteKardex()
        {
            InitializeComponent();
        }

        public void InitData(string sucursal, string producto)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = sucursal;
            sqlDataSource1.Queries[0].Parameters[1].Value = producto;

        }

    }
}
