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
        public int sucursal_id;
        public int empleado_id;
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
                foreach (DataRow x in dt.Rows)
                {
                    string get_due_date = "SELECT fecha_caducidad, id_asignacionprecio FROM tblAsignacionCantidad WHERE id_asignacion = {0};";
                    get_due_date = string.Format(get_due_date, x["id_asignacion"]);
                    DataTable tmp = da.fillDataTable(get_due_date);
                    DateTime temp = Convert.ToDateTime(tmp.Rows[0]["fecha_caducidad"]);

                   // string due_date = Date.ToString("yyyy-MM-dd");
                    string insert_asign = "INSERT INTO tblAsignacionCantidad(cantidad,id_producto,id_sucursal,fecha_caducidad,id_asignacionprecio) ";
                    insert_asign += "VALUES({0},'{1}',{2},'{3}',{4});";
                    insert_asign = string.Format(insert_asign,x["cantidad"], x["id_producto"],sucursal_id, temp.ToString("yyyy-MM-dd"), tmp.Rows[0]["id_asignacionprecio"]);
                    //MessageBox.Show(insert_asign);
                    da.executeCommand(insert_asign);
                }
                string update_traslate = "UPDATE tblTraslado SET id_empleado_entrada = {0}, fecha_entrada = CURDATE() WHERE id_traslado = {1} ;";
                update_traslate = string.Format(update_traslate,empleado_id, id_traslado);
                da.executeCommand(update_traslate);
                MessageBox.Show("Confirmacion de traslado exitosa.", "Traslado Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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