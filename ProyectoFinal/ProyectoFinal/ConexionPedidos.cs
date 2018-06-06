using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace ProyectoFinal
{
    public class ConexionPedidos
    {
        OracleDataAccess oda = new OracleDataAccess();
        DataAccess da = new DataAccess();
        string query;
        public DataTable detallesPedido(int codPedido)
        {
            DataTable dt = new DataTable();
            query = "SELECT * FROM DETALLEPEDIDO WHERE ID_PEDIDO={0}";
            query = string.Format(query);
            return dt = oda.fillDataTable(query);
        }

        public DataTable pedidosPendientes()
        {
            DataTable dt = new DataTable();
            query = "SELECT * FROM PEDIDO WHERE ESTADO='{0}'";
            query = string.Format(query, "0");
            return dt = oda.fillDataTable(query);
        }

        public int actualizarPedido(int codPedido)
        {
            query = "UPDDATE PEDIDO SET estado='{0}' WHERE ID_PEDIDO={1}";
            query = string.Format(query, "1", codPedido);
            return oda.executeCommand(query);
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
            factura.lblSucursal.Text = "Sucursal:  " + encabezado.Rows[0]["sucursal"].ToString();
            factura.lblEmpleado.Text = "Le Atendio: " + encabezado.Rows[0]["empleado"].ToString();
            factura.lblTotal.Text = "Q." + encabezado.Rows[0]["total"].ToString();
            factura.lblNit.Text = encabezado.Rows[0]["nit"].ToString();
            factura.lblArticulos.Text = detalles.Rows.Count.ToString();
            factura.DataSource = detalles;
            factura.ShowPreview();
        }
    }
}
