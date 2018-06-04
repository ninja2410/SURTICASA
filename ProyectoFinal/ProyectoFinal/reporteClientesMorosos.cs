using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteClientesMorosos : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteClientesMorosos()
        {
            InitializeComponent();
        }
        public void InitData(DateTime fechaActual)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = fechaActual;

        }


    }
}
