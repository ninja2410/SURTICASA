using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteMenosVendidos : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteMenosVendidos()
        {
            InitializeComponent();
        }

        public void InitData(string sucursal)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = sucursal;

        }

    }
}
