using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class f_ventas : Form
    {
        DataAccess da = new DataAccess();
        DataAccessSQL sql_conn = new DataAccessSQL();
        DataTable dt = new DataTable();
        DataTable tmp = new DataTable();
        List<ProductosVenta> lista = new List<ProductosVenta>();
        string query;
        public int sucursal;
        decimal totalFactura;
        public bool venta=true;
        public int empleado;
        public string nombreEmpleado;
        public string nombreSucursal;
        int codigoProveedor;
        int codigoCliente;
        int idCaja;
        public f_ventas()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }
        public f_ventas(int caja)
        {
         idCaja= caja;
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void f_ventas_Load(object sender, EventArgs e)
        {
          
            

            //LLENADO DE LOOKUPEDIT DE PRODUCTO CON VISTA listarProductos
            try
            {
                query = "SELECT id_producto, nombre_producto from listarProductos WHERE id_sucursal={0}";
                query = string.Format(query, sucursal);
                lProductos.Properties.DataSource = da.fillDataTable(query);
                lProductos.Properties.DisplayMember = "nombre_producto";
                lProductos.Properties.ValueMember = "id_producto";

            }
            catch (Exception ex)
            {

                throw new Exception("Error al ingresar productos "+ex.Message); 
            }
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Precio/U");
            dt.Columns.Add("Total");

            cargar();
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            gridControl1.DataSource = dt;
            
        }
        private void cargar()
        {
            if (!venta)
            {
                layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblTitulo.Text = "Compras Surticasa S.A.";
                button1.Text = "Comprar";
                dt.Columns.Add("Fecha Vencimiento");
                simpleButton1.Text = "Agregar Proveedor";
            }
            else
            {
                layoutControlItem16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem18.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblTitulo.Text = "Ventas Surticasa S.A.";
                button1.Text = "Vender";

                simpleButton1.Text = "Agregar Cliente";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string dateTime = dtFechaPago.DateTime.Date.ToString();
            string date_credit = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
            //MessageBox.Show(date_credit);
            if (verificar() == true)
            {
                try
                {
                    if (!venta)
                    {
                        da.tansactCompra(dt, txtDocumento.Text, empleado, false, codigoProveedor, sucursal, totalFactura);
                        //enviamos el dato a la tabla de caja para sumar la cantidad.
                        string query = "CALL SP_UPDATECAJA({0},-{1})"; //Actualizamos la cantidad de caja
                        query = string.Format(query,idCaja,totalFactura);
                        da.executeCommand(query);

                        

                        if (chkCredito.Checked == true)
                        {
                            //int idCliente = Convert.ToInt16(lCliente.EditValue);
                            decimal monto = Convert.ToDecimal(totalFactura.ToString());
                            string sQuery;
                            MessageBox.Show(codigoProveedor.ToString());
                            sQuery = "insert into tblCreditos (deudor,monto,fecha_limite,tipo_cuenta,documento) ";
                            sQuery += "values ({0},{1},'{2}',0,'{3}')";
                            sQuery = string.Format(sQuery, codigoProveedor, monto, date_credit, txtDocumento.Text);
                            da.executeCommand(sQuery);
                            string insertdb_credit = "INSERT INTO log_compras(proveedor,empleado,sucursal,tipo_compra,documento,total,fecha) ";
                            insertdb_credit += "VALUES ('{0}','{1}','{2}','Credito','{3}',{4}, getdate());";
                            insertdb_credit = string.Format(insertdb_credit, codigoProveedor, nombreEmpleado, nombreSucursal, txtDocumento.Text, totalFactura);
                            sql_conn.executeCommand(insertdb_credit);
                            MessageBox.Show("Se ha generado el credito con exito");
                            // el 0 significa que es COMPRA
                        }
                        else
                        {
                            string insertdb = "INSERT INTO log_compras(proveedor,empleado,sucursal,tipo_compra,documento,total,fecha) ";
                            insertdb += "VALUES ('{0}','{1}','{2}','Contado','{3}',{4}, getdate());";
                            insertdb = string.Format(insertdb, codigoProveedor, nombreEmpleado, nombreSucursal, txtDocumento.Text, totalFactura);
                            sql_conn.executeCommand(insertdb);
                        }
                    }
                    else
                    {
                        da.transact(dt, txtDocumento.Text, empleado, true, codigoCliente,
                            sucursal, totalFactura);
                        string query = "CALL SP_UPDATECAJA({0},{1})"; //Actualizamos la cantidad de caja
                        query = string.Format(query, idCaja, totalFactura);
                        da.executeCommand(query);
                        DataTable tmp = new DataTable();
                        query = "select MAX(id_venta) as cod from tblVenta";
                        tmp = da.fillDataTable(query);

                        
                        if (chkCredito.Checked == true)
                        {
                            //int idCliente = Convert.ToInt16(lCliente.EditValue);
                            decimal monto = Convert.ToDecimal(totalFactura.ToString());
                            string sQuery;
                            MessageBox.Show(codigoCliente.ToString());
                            sQuery = "insert into tblCreditos (deudor,monto,fecha_limite,tipo_cuenta,documento) ";
                            sQuery += "values ({0},{1},'{2}',1,'{3}')";
                            sQuery = string.Format(sQuery, codigoCliente, monto, date_credit, txtDocumento.Text);
                            da.executeCommand(sQuery);
                            string insertdb_credit = "INSERT INTO log_ventas(cliente,empleado,sucursal,tipo_venta,documento,total,fecha) ";
                            insertdb_credit += "VALUES ('{0}','{1}','{2}','Credito','{3}',{4}, getdate());";
                            insertdb_credit = string.Format(insertdb_credit, codigoCliente, nombreEmpleado, nombreSucursal, txtDocumento.Text, totalFactura);
                            sql_conn.executeCommand(insertdb_credit);

                            MessageBox.Show("Se ha generado el credito con exito");
                            // el 1 significa que es VENTA
                        }
                        else {
                            string insertdb = "INSERT INTO log_ventas(cliente,empleado,sucursal,tipo_venta,documento,total,fecha) ";
                            insertdb += "VALUES ('{0}','{1}','{2}','Contado','{3}',{4}, getdate());";
                            insertdb = string.Format(insertdb, codigoCliente, nombreEmpleado, nombreSucursal, txtDocumento.Text, totalFactura);
                            sql_conn.executeCommand(insertdb);

                        }
                        imprimirFactura(Convert.ToInt16(tmp.Rows[0]["cod"]));
                    }

                    


                    txtCantidad.Text = "";
                    txtDocumento.Text = "";
                    txtPrecio.Text = "";
                    dt.Clear();
                    gridControl1.Refresh();
                    gridControl1.RefreshDataSource();
                    gridView1.RefreshData();
                    totalFactura = 0;
                    lblTotal.Text = totalFactura.ToString();
                    txtDocumento.Focus();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("ERROR EN LA EJECUCION "+ex.Message);
                }

            }
        }
        public void imprimirFactura(int codFactura)
        {
            MessageBox.Show("Factura Generada con Exito");
            string squery;
            DataTable encabezado = new DataTable();
            DataTable detalles = new DataTable();
            squery = "SELECT id_venta as n, fecha, documento, c.nombre as cliente, s.nombre_sucursal as sucursal, ";
            squery += " e.nombre as empleado, total, c.nit as nit FROM tblVenta as v inner ";
            squery += "join tblCliente as c on v.id_cliente=c.id_cliente inner join tblSucursal as s on ";
            squery += "v.id_sucursal=s.id_sucursal inner join tblEmpleado as e on e.id_empleado=v.id_empleado ";
            squery += " where id_venta={0}";
            squery = string.Format(squery, codFactura);
            encabezado = da.fillDataTable(squery);
            squery = "select cantidad, p.nombre_producto as nombre, precio, total from tblDetallesVenta as";
            squery += " d inner join tblAsignacionPrecio as ap on";
            squery += " d.id_asignacionprecio=ap.id_asignacionprecio inner join tblProducto as p on ";
            squery += "ap.id_producto=p.id_producto where id_venta={0}";
            squery = string.Format(squery, codFactura);
            detalles = da.fillDataTable(squery);
            FACTURA factura = new FACTURA();
            factura.lblCliente.Text = encabezado.Rows[0]["cliente"].ToString();
            factura.lblNumero.Text = encabezado.Rows[0]["documento"].ToString();
            factura.lblFecha.Text = encabezado.Rows[0]["fecha"].ToString();
            factura.lblSucursal.Text ="Sucursal:  "+ encabezado.Rows[0]["sucursal"].ToString();
            factura.lblEmpleado.Text = "Le Atendio: "+encabezado.Rows[0]["empleado"].ToString();
            factura.lblTotal.Text = "Q." + encabezado.Rows[0]["total"].ToString();
            factura.lblNit.Text = encabezado.Rows[0]["nit"].ToString();
            factura.lblArticulos.Text = detalles.Rows.Count.ToString();
            factura.DataSource = detalles;
            factura.ShowPreview();
        }
        private bool verificar()
        {
            bool respuesta=true;
            if (txtDocumento.Text == "")
            {
                respuesta = false;
                MessageBox.Show("Debe ingresar un documento");
                txtDocumento.Focus();
            }
            if (codigoCliente==0 && venta)
            {
                respuesta = false;
                MessageBox.Show("Debe Seleccionar un cliente");
                
            }
            if(codigoProveedor==0 && !venta)
            {
                respuesta = false;
                MessageBox.Show("Debe seleccionar un proveedor");
            }
            if (gridView1.DataRowCount < 1)
            {
                MessageBox.Show("Debe ingresar al menos un producto");
                respuesta = false;
            }
            return respuesta;
        }
        private void chkCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCredito.Checked == true)
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                 
            }
            else
            {
                layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool bandera=true;
            if (verificarProducto())
            {
                DataRow r = dt.NewRow();
                lPresentacion.Properties.ValueMember = "Codigo";

                if (venta)
                {

                    r["Codigo"] = verificarCodigo(Convert.ToInt16(lPresentacion.EditValue));
                    if (r["Codigo"].ToString() == "-1")
                    {
                        txtCantidad.Focus();
                        MessageBox.Show("No hay existencia sifucientes de este producto");
                        bandera = false;
                    }
                    lPresentacion.Properties.ValueMember = "Precio";
                }
                else
                {
                    r["Codigo"] = verificarAsignacion(lProductos.EditValue.ToString(),
                        Convert.ToInt16(lPresentacion.EditValue));
                }
                if (bandera)
                {

                    r["Cantidad"] = txtCantidad.Text;
                    r["Descripción"] = lProductos.Text + " " + lPresentacion.Text;
                    if (venta)
                    {
                        r["Precio/U"] = lPresentacion.EditValue;
                        totalFactura += Convert.ToInt16(txtCantidad.Text) * Convert.ToDecimal(lPresentacion.EditValue);
                        r["Total"] = Convert.ToInt16(txtCantidad.Text) * Convert.ToDecimal(lPresentacion.EditValue);
                    }
                    else
                    {
                        r["Precio/U"] = Convert.ToDecimal(txtPrecio.Text);
                        totalFactura += Convert.ToInt16(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                        r["Total"] = Convert.ToInt16(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                    }
                    lblTotal.Text = "Total: Q " + totalFactura.ToString();
                    if (!venta)
                    {
                        r["Fecha Vencimiento"] = fVencimiento.Value.Date;
                    }



                    dt.Rows.Add(r);
                    gridControl1.RefreshDataSource();
                    gridControl1.Refresh();
                    txtCantidad.Text = "";
                    lProductos.Properties.DisplayMember = "nombre_producto";
                    lProductos.Properties.ValueMember = "id_producto";
                    txtCantidad.Focus();
                }
            }
            
        }

        private int verificarCodigo(int cod)
        {
            int codigo;
            string q;
            DataTable dt = new DataTable();
            DataTable tmp = new DataTable();

            q = "SELECT SUM(cantidad) as n from tblAsignacionCantidad WHERE id_asignacionprecio={0} AND id_sucursal={1} and cantidad>0";
            q = string.Format(q, cod, sucursal);
            tmp = da.fillDataTable(q);
            if (tmp.Rows[0]["n"].ToString()=="")
            {
                return -1;
            }
            if (Convert.ToInt16(tmp.Rows[0]["n"]) < Convert.ToInt16(txtCantidad.Text))
            {
                codigo = -1;
            }
            else
            {
                q = "SELECT id_asignacionprecio, fecha_caducidad from tblAsignacionCantidad where id_asignacionprecio={0} and id_sucursal={1} AND cantidad>0";
                q += " ORDER BY fecha_caducidad ASC";
                q = string.Format(q, cod, sucursal);
                dt = da.fillDataTable(q);
                codigo = Convert.ToInt16(dt.Rows[0]["id_asignacionprecio"]);
            }
            return codigo;
        }

        private int verificarAsignacion(string codProducto, int codPresentacion)
        {
            int codigo;
            string q;
            DataTable dt = new DataTable();
            q = "SELECT id_asignacionprecio as codigo FROM tblAsignacionPrecio WHERE id_producto='{0}' AND id_presentacion={1}";
            q = string.Format(q, codProducto, codPresentacion);
            dt = da.fillDataTable(q);
            codigo = Convert.ToInt16(dt.Rows[0]["codigo"]);
            return codigo;
        }

        private bool verificarProducto()
        {
            bool resultado = true;

            if (lPresentacion.EditValue == null)
            {
                resultado = false;
                MessageBox.Show("Debe seleccionar una presentación");
            }
            if (lProductos.EditValue == null)
            {
                resultado = false;
                MessageBox.Show("Debe Seleccionar un Producto");
                lProductos.Focus();
            }

            if (txtCantidad.Text == "")
            {
                resultado = false;
                MessageBox.Show("Debe ingresar una cantidad de producto");
                txtCantidad.Focus();
            }
            
            return resultado;
        }

        private void lProductos_EditValueChanged(object sender, EventArgs e)
        {
            if (venta)
            {
                query = "SELECT id_asignacionprecio as Codigo, tipo_presentacion as Presentación, precio_venta as Precio from tblAsignacionPrecio as a INNER JOIN tblPresentacion as p on a.id_Presentacion=p.id_Presentacion";
                query += " WHERE exists(SELECT * FROM tblAsignacionCantidad WHERE id_sucursal={0} AND id_asignacionprecio=a.id_asignacionprecio AND cantidad>0) AND id_producto='{1}';";
                query = string.Format(query, sucursal, lProductos.EditValue.ToString());
            }
            else
            {
                query = "SELECT a.id_presentacion as Codigo, tipo_presentacion as Presentación from tblAsignacionPrecio as a INNER JOIN tblPresentacion as p on a.id_Presentacion=p.id_Presentacion";
                query += " WHERE id_producto='{0}';";
                query = string.Format(query, lProductos.EditValue.ToString());
            }
            lPresentacion.Properties.DataSource = da.fillDataTable(query);

            if (venta)
            {

                lPresentacion.Properties.ValueMember = "Precio";
            }
            else
            {
                lPresentacion.Properties.ValueMember = "Codigo";
            }
            lPresentacion.Properties.DisplayMember = "Presentación";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void lPresentacion_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!venta)
            {
                //proveedor
                frmNewProvider p = new frmNewProvider();
                p.txtNitProvider.Text = txtNit.Text;
                p.ShowDialog();
                cargarinformacion();
            }
            else
            {
                //cliente
                frmnewClient c = new frmnewClient();
                c.txtNit.Text = txtNit.Text;
                c.ShowDialog();
                cargarinformacion();
                
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Codigo"].ToString() == cod.ToString())
                {
                    totalFactura -= Convert.ToDecimal(dt.Rows[i]["Total"]);
                    lblTotal.Text="Total: Q " + totalFactura.ToString();
                    dt.Rows.Remove(dt.Rows[i]);
                }
            }
            gridView1.RefreshData();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            cargarinformacion();
            
        }
        public void cargarinformacion()
        {
            if (txtNit.Text.Length >= 8)
            {
                string tmps;
                if (venta)
                {
                    tmps = "SELECT * FROM tblCliente where nit={0}";
                    tmps = string.Format(tmps, Convert.ToInt32(txtNit.Text));
                    tmp = da.fillDataTable(tmps);
                    if (tmp.Rows.Count == 0)
                    {
                        txtNombre.Text = "";
                        MessageBox.Show("El NIT no esta registrado");
                        simpleButton1.Focus();
                    }
                    else
                    {
                        txtNombre.Text = "";
                        codigoCliente = Convert.ToInt16(tmp.Rows[0]["id_cliente"]);
                        txtNombre.Text = tmp.Rows[0]["nombre"].ToString() + " " + tmp.Rows[0]["apellido"].ToString();
                    }
                }
                else
                {
                    tmps = "SELECT * FROM tblProveedor where nit={0}";
                    tmps = string.Format(tmps, Convert.ToInt32(txtNit.Text));
                    tmp = da.fillDataTable(tmps);
                    if (tmp.Rows.Count == 0)
                    {
                        txtNombre.Text = "";
                        MessageBox.Show("El NIT no esta registrado");
                        simpleButton1.Focus();
                    }
                    else
                    {
                        txtNombre.Text = "";
                        codigoProveedor = Convert.ToInt16(tmp.Rows[0]["id_proveedor"]);
                        txtNombre.Text = tmp.Rows[0]["nombre_proveedor"].ToString() ;
                    }
                }
                
            }
        }

        private void txtNit_EditValueChanged(object sender, EventArgs e)
        {
            cargarinformacion();
        }
    }
}
