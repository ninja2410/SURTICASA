using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ProyectoFinal
{
    public class ConexionPedidos
    {
        OracleDataAccess oda = new OracleDataAccess();
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
    }
}
