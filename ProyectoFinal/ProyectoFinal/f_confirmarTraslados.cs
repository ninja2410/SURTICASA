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
    public partial class f_confirmarTraslados : DevExpress.XtraEditors.XtraForm
    {
        public f_confirmarTraslados()
        {
            InitializeComponent();
        }
        public int sucursal_id;
        public int empleado_id;
        DataAccess da = new DataAccess();

        private void f_confirmarTraslados_Load(object sender, EventArgs e)
        {
            uploadTraslate();
        }
        void uploadTraslate()
        {
            string get_traslate = "SELECT T.id_traslado, fecha_salida AS FechaEnvio,";
            get_traslate += " (SELECT S.nombre_sucursal FROM tblSucursal S WHERE S.`id_sucursal`= T.id_sucursal_salida) AS DESDE,";
            get_traslate += " (SELECT CONCAT(A.nombre, ' ', A.apellido) FROM tblEmpleado A WHERE A.id_empleado = T.id_empleado_salida) AS Envio";
            get_traslate += " FROM tblTraslado T WHERE T.id_sucursal_entrada = {0}";
            get_traslate += " AND T.id_empleado_entrada IS NULL AND T.fecha_entrada IS NULL";
            get_traslate = string.Format(get_traslate, sucursal_id);
            gridTraslados.DataSource = da.fillDataTable(get_traslate);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
           int id_traslado_check = Convert.ToInt16(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id_traslado"));
            f_detalleTraslado detailTraslate = new f_detalleTraslado();
            detailTraslate.id_traslado = id_traslado_check;
            detailTraslate.Show();
        }
    }
}