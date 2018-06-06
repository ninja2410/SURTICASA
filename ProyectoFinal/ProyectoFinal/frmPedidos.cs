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
    public partial class frmPedidos : DevExpress.XtraEditors.XtraForm
    {
        private int sucursal;
        private int empleado;
        public frmPedidos()
        {
            InitializeComponent();
        }
        public frmPedidos(int idSucursal, int idEmpleado)
        {
            sucursal = idSucursal;
            empleado = idEmpleado;

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }
    }
}