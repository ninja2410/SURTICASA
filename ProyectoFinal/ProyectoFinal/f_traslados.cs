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
    public partial class f_traslados : DevExpress.XtraEditors.XtraForm
    {
        public f_traslados()
        {
            InitializeComponent();
        }
        public int empleado_id = 0;
        public int sucursal_id = 0;
        DataAccess da = new DataAccess();
        DataTable dt = new DataTable();
        int sucursal_entrada_id;
        private void f_traslados_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Presentacion");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Sucursal");
            timer1.Enabled = true;
            uploadSucursales();
            uploadPresentacion();
            gridControl1.DataSource = dt;
        }
        void uploadSucursales()
        {
            string sQuery_sucursales = "SELECT id_sucursal, nombre_sucursal FROM tblSucursal WHERE id_sucursal != {0};";
            sQuery_sucursales = string.Format(sQuery_sucursales, sucursal_id);
            cmbSucursal.Properties.DataSource = da.fillDataTable(sQuery_sucursales);
            cmbSucursal.Properties.DisplayMember = "nombre_sucursal";
            cmbSucursal.Properties.ValueMember = "id_sucursal";
        }
        void uploadPresentacion()
        {
            //"SELECT id_sucursal, nombre_sucursal FROM tblSucursal WHERE id_sucursal = {0};";
            string sQuery_productos = "SELECT AC.`id_asignacion`,P.`nombre_producto`, S.`nombre_sucursal`, PRES.tipo_presentacion, AC.`cantidad`";
            sQuery_productos += " FROM tblAsignacionCantidad AC INNER JOIN tblProducto P";
            sQuery_productos += " ON AC.`id_producto` = P.`id_producto` INNER JOIN tblSucursal S";
            sQuery_productos += " ON AC.`id_sucursal` = S.`id_sucursal` INNER JOIN tblAsignacionPrecio APR";
            sQuery_productos += " ON AC.`id_asignacionprecio` = APR.id_asignacionprecio INNER JOIN tblPresentacion PRES";
            sQuery_productos += " ON APR.id_Presentacion = PRES.id_Presentacion WHERE AC.`cantidad` > 0 AND S.`id_sucursal` = {0}";
            sQuery_productos = string.Format(sQuery_productos, sucursal_id);
            cmbProducto.Properties.DataSource = da.fillDataTable(sQuery_productos);
            cmbProducto.Properties.DisplayMember = "nombre_producto";
            cmbProducto.Properties.ValueMember = "id_asignacion";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           label1.Text = DateTime.Now.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cmbSucursal.Enabled = false;
            int producto_id_sucursal = Convert.ToInt16(cmbSucursal.EditValue);
            int producto_id_asignacion = Convert.ToInt16(cmbProducto.EditValue);
            int producto_cantidad;

            sucursal_entrada_id = producto_id_sucursal;
            if (txtCantidad.Text != "")
            {
                producto_cantidad = Convert.ToInt16(txtCantidad.Text);
                if (!verificarExistencia(producto_cantidad, producto_id_asignacion))
                {
                    DataRow dr = dt.NewRow();
                    dr["Codigo"] = producto_id_asignacion;
                    dr["Cantidad"] = producto_cantidad;
                    string info_query = "SELECT AC.`id_asignacion`,P.`nombre_producto`, S.`nombre_sucursal`, PRES.tipo_presentacion, AC.`cantidad`";
                    info_query += " FROM tblAsignacionCantidad AC INNER JOIN tblProducto P";
                    info_query += " ON AC.`id_producto` = P.`id_producto` INNER JOIN tblSucursal S";
                    info_query += " ON AC.`id_sucursal` = S.`id_sucursal` INNER JOIN tblAsignacionPrecio APR";
                    info_query += " ON AC.`id_asignacionprecio` = APR.id_asignacionprecio INNER JOIN tblPresentacion PRES";
                    info_query += " ON APR.id_Presentacion = PRES.id_Presentacion WHERE AC.`cantidad` > 0 AND S.`id_sucursal` = {0} AND AC.`id_asignacion` = {1};";
                    info_query = string.Format(info_query, sucursal_id, producto_id_asignacion);
                    DataTable temp = da.fillDataTable(info_query);
                    dr["Producto"] = Convert.ToString(temp.Rows[0]["nombre_producto"]);
                    dr["Presentacion"] = Convert.ToString(temp.Rows[0]["tipo_presentacion"]);
                    info_query = "SELECT id_sucursal, nombre_sucursal FROM tblSucursal WHERE id_sucursal = {0};";
                    info_query = string.Format(info_query, producto_id_sucursal);
                    temp = da.fillDataTable(info_query);
                    dr["Sucursal"] = Convert.ToString(temp.Rows[0]["nombre_sucursal"]);
                    dt.Rows.Add(dr);
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();
                    cmbProducto.Properties.DisplayMember = "nombre_producto";
                    cmbProducto.Properties.ValueMember = "id_asignacion";
                    cmbSucursal.Properties.DisplayMember = "nombre_sucursal";
                    cmbSucursal.Properties.ValueMember = "id_sucursal";
                    txtCantidad.Text = "";
                }
                else
                {
                    MessageBox.Show(
                        "La cantidad excede la existencia de este producto en esta sucursal.",
                        "Error de existencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }

            }
            else
            {
                MessageBox.Show("No se ha insetado una cantidad","Advertencia", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtCantidad.Focus();
            }
           // sucursal_entrada_id = producto_id_sucursal;           
        }
        bool verificarExistencia (int cantidad, int asignacion)
        {
            string sQuery= "SELECT cantidad FROM tblAsignacionCantidad WHERE id_asignacion = {0}; ";
            sQuery = string.Format(sQuery,asignacion);
            DataTable dtCheck = da.fillDataTable(sQuery);
            int existente = Convert.ToInt16(dtCheck.Rows[0]["cantidad"]);
            //MessageBox.Show(existente.ToString());
            if (existente >= cantidad)
                return false;
            else      
                return true;    
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string insert_dtraslado, update_asignacion;
                    string insert_traslado = "INSERT INTO tblTraslado(id_sucursal_salida,id_sucursal_entrada,id_empleado_salida,fecha_salida)";
                    insert_traslado += " VALUES ({0},{1},{2}, CURDATE());";
                    insert_traslado = string.Format(insert_traslado, sucursal_id,sucursal_entrada_id,empleado_id);
                    da.executeCommand(insert_traslado);
                    string getMaxTraslate = "SELECT MAX(id_traslado) AS codigoTraslado FROM tblTraslado;";
                    int idTraslado = Convert.ToInt16(da.fillDataTable(getMaxTraslate).Rows[0]["codigoTraslado"]);

                    foreach (DataRow x in dt.Rows)
                    {
                        insert_dtraslado = "INSERT INTO tblDetalleTraslado(cantidad,id_traslado, id_asignacion)";
                        insert_dtraslado += " VALUES ({0},{1},{2});";
                        insert_dtraslado = string.Format(insert_dtraslado,Convert.ToInt16(x["Cantidad"].ToString()),idTraslado,
                            Convert.ToInt16(x["Codigo"].ToString()));
                        da.executeCommand(insert_dtraslado);
                        update_asignacion = "UPDATE tblAsignacionCantidad SET cantidad = cantidad - {0} WHERE id_asignacion = {1};";
                        update_asignacion = string.Format(update_asignacion, Convert.ToInt16(x["Cantidad"].ToString()), Convert.ToInt16(x["Codigo"].ToString()));
                        da.executeCommand(update_asignacion); 
                    }
                    cmbSucursal.Enabled = true;
                    dt.Clear();
                    gridControl1.Refresh();
                    gridControl1.RefreshDataSource();
                    gridView1.RefreshData();
                    uploadPresentacion();
                    uploadSucursales();
                    MessageBox.Show("Traslado realizado con exito, en espera de confirmacion.","Traslado exitoso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error en la confirmación", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}