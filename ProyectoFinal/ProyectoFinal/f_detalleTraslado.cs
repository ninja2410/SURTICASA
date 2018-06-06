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
    public partial class f_detalleTraslado : DevExpress.XtraEditors.XtraForm
    {
        public f_detalleTraslado()
        {
            InitializeComponent();
        }
        public int id_traslado;
        DataAccess da = new DataAccess();
        DataTable dt = new DataTable();

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(DataRow x in dt.Rows)
                {
                    //string insrt_asign = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detail: " + ex.Message);
            }
        }

        private void f_detalleTraslado_Load(object sender, EventArgs e)
        {
            onloadDetails();
        }
        void onloadDetails()
        {
            string detalles = "SELECT tblDetalleTraslado.id_asignacion,tblProducto.id_producto, tblProducto.nombre_producto, tblPresentacion.tipo_presentacion, tblDetalleTraslado.cantidad ";
            detalles += "FROM tblDetalleTraslado INNER JOIN tblAsignacionCantidad ON ";
            detalles += "tblDetalleTraslado.id_asignacion = tblAsignacionCantidad.id_asignacion ";
            detalles += "INNER JOIN tblProducto ON tblAsignacionCantidad.id_producto = tblProducto.id_producto ";
            detalles += "INNER JOIN tblAsignacionPrecio ON tblAsignacionCantidad.id_asignacionprecio = tblAsignacionPrecio.id_asignacionprecio ";
            detalles += "INNER JOIN tblPresentacion ON tblAsignacionPrecio.id_presentacion = tblPresentacion.id_presentacion ";
            detalles += "WHERE tblDetalleTraslado.`id_traslado` = {0}";
            detalles = string.Format(detalles,id_traslado);
            dt.Clear();
            dt = da.fillDataTable(detalles);
            gridControl1.DataSource = da.fillDataTable(detalles);

        }
    }
}