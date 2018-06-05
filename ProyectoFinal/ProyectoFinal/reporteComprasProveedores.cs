using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public partial class reporteComprasProveedores : DevExpress.XtraReports.UI.XtraReport
    {
        public reporteComprasProveedores()
        {
            InitializeComponent();
        }

        public void InitData(int idProveedor)
        {
            sqlDataSource1.Queries[0].Parameters[0].Value = idProveedor;

        }

    }
}
