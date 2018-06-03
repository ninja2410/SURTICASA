using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public class DataAccess //clase publica dentro del proyecto pero oculta en otros como bisness
    {
        #region attributes
        private MySqlConnection cnConnection { get; set; }
        private string ConnectionString = string.Empty;//inicializado cadena vacia
        string x = "",query="";
        #endregion


        #region buillder
        public DataAccess()
        {
            ConnectionString = "Server=185.224.137.20;Database=u983648979_dbsur; Uid =u983648979_loto;Pwd=3McfvgblzEpj; SslMode=none;";
            //ConnectionString = "Server=127.0.0.1;Database=dbsurticasa;Uid=root;Pwd=database;";
            //ConnectionString = "Server=localhost;Database=dbsurticasa;Uid=root;Pwd=s3xo!=am0r;";

            cnConnection = new MySqlConnection(ConnectionString);
            
        }
        #endregion

        #region method
        public void openConnection()
        {
            try
            {
                if(cnConnection.State!=ConnectionState.Open)
                {
                    cnConnection.Open();//si esta cerrada la conexion, abrir la conexion
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error opening connection, error detail: " + ex.Message+"\n");
            }
        }

        public void closeConnection()
        {
            try
            {
                if (cnConnection.State != ConnectionState.Closed)
                {
                    cnConnection.Close();//si esta abierta la conexion, cerrar la conexion la conexion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error opening connection, error detail: " + ex.Message + "\n");
            }
        }

        public DataTable fillDataTable(string sQuery) //va a retornar una tabla de datos, consulta
        {
            DataTable dt = new DataTable();
            try
            {
                
                MySqlDataAdapter da = new MySqlDataAdapter(sQuery, cnConnection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Select detail error: "+ex.Message+"\n");
            }
            return dt;
        }
        #endregion


        public int executeCommand(string sCommand)//funcion para ejecutar un comando sql
        {
            MySqlCommand cm = new MySqlCommand(sCommand, cnConnection);
            //en el sql comand hay que abrir y cerrar conexion
            try
            {
                cm.Connection.Open();
                return cm.ExecuteNonQuery();//aqui se retorna un int con la cantidad de rows afectadas
               
            }
            catch(Exception ex)
            {
                throw new Exception("Error for execute command:" + ex.Message);
            }
            finally//siempre se ejecuta independiente de todo
            {
                cm.Connection.Close();
            }
        }

        public object executeScalar(string sCommand)
        {
            MySqlCommand cm = new MySqlCommand(sCommand, cnConnection);
            object value;
            try
            {
                cm.Connection.Open();
                return value=cm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error for execute command:" + ex.Message);

            }
            finally
            {
                cm.Connection.Close(); // Cerramos la conexion
            }

        }

        public void transact(DataTable detalles, string documento, int codEmpleado, bool codTipoVenta, int codCliente, int idSucursal, decimal total)
        {
            cnConnection.Open();
            // Start a local transaction.
            MySqlTransaction sqlTran = cnConnection.BeginTransaction();

            // Enlist a command in the current transaction.
            MySqlCommand command = cnConnection.CreateCommand();
            command.Transaction = sqlTran;

            try
            {
                query = "INSERT INTO tblVenta(fecha, documento, codigo_cliente, id_cliente, id_empleado, id_sucursal, tipo_venta, total) ";
                query += "VALUES('{0}','{1}',{2},{3},{4},{5},{6},{7})";
                query = string.Format(query,DateTime.Today.Date.ToString("yyyy-MM-dd"),documento,codCliente,codCliente.ToString(), codEmpleado, idSucursal, codTipoVenta, total);
                // Execute two separate commands.
                command.CommandText = query;
                command.ExecuteNonQuery();
                DataTable tmp = new DataTable();
                tmp = fillDataTable("select Max(id_venta) as Cod FROM tblVenta ");
                foreach (DataRow r in detalles.Rows)
                {
                    x = "";
                    x = "INSERT INTO tblDetallesVenta(id_asignacion, id_venta, cantidad, precio, total) ";
                    x += "VALUES({0},{1},{2},{3},{4})";
                    x = string.Format(x,Convert.ToInt16(r["Codigo"]), Convert.ToInt16(tmp.Rows[0]["Cod"]),
                        Convert.ToInt16(r["Cantidad"]), Convert.ToDecimal(r["Precio/U"]), Convert.ToDecimal(r["Total"]));
                    command.CommandText = x;
                    command.ExecuteNonQuery();
                }
                
                // Commit the transaction.
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                // Handle the exception if the transaction fails to commit.

                try
                {
                    // Attempt to roll back the transaction.
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    MessageBox.Show("Error " + exRollback + "Info Adicional" + ex.Message);
                }
                throw new Exception("ERROR EN LA TRANSACCION " + ex.Message);
            }

            cnConnection.Close();
        }

        public void tansactCompra(DataTable detalles, string documento, int codEmpleado, bool codTipoVenta, int codProveedor, int idSucursal, decimal total)
        {
            cnConnection.Open();
            // Start a local transaction.
            MySqlTransaction sqlTran = cnConnection.BeginTransaction();

            // Enlist a command in the current transaction.
            MySqlCommand command = cnConnection.CreateCommand();
            command.Transaction = sqlTran;

            try
            {
                query = "INSERT INTO tblCompra(fecha, total_documento, id_proveedor, id_sucursal, id_empleado, genera_credito)";
                query += "VALUES('{0}',{1},{2},{3},{4},{5})";
                query = string.Format(query, DateTime.Today.Date.ToString("yyyy-MM-dd"), total, codProveedor, idSucursal, codEmpleado, codTipoVenta);
                // Execute two separate commands.
                command.CommandText = query;
                command.ExecuteNonQuery();
                DataTable tmp = new DataTable();
                tmp = fillDataTable("select Max(id_compra) as Cod FROM tblCompra ");
                foreach (DataRow r in detalles.Rows)
                {
                    x = "";
                    x = "CALL updtCompra({0},{1},{2},{3},{4},'{5}',{6})";
                    x = string.Format(x, Convert.ToInt16(r["Cantidad"]), Convert.ToDecimal(r["Precio/U"]),
                        Convert.ToDecimal(r["Total"]), Convert.ToInt16(tmp.Rows[0]["Cod"]), Convert.ToInt16(r["Codigo"]),
                        Convert.ToDateTime(r["Fecha Vencimiento"]).ToString("yyyy-MM-dd"), idSucursal);
                    command.CommandText = x;
                    command.ExecuteNonQuery();
                }

                // Commit the transaction.
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                // Handle the exception if the transaction fails to commit.

                try
                {
                    // Attempt to roll back the transaction.
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    MessageBox.Show("Error " + exRollback + "Info Adicional" + ex.Message);
                }
                throw new Exception("ERROR EN LA TRANSACCION COMPRA: " + ex.Message);
            }

            cnConnection.Close();
        }
        public void regNotaSalida(string motivo, string fecha, int codEmpleado, int sucursal, DataTable detalles)
        {
            cnConnection.Open();
            // Start a local transaction.
            MySqlTransaction sqlTran = cnConnection.BeginTransaction();

            // Enlist a command in the current transaction.
            MySqlCommand command = cnConnection.CreateCommand();
            command.Transaction = sqlTran;

            try
            {
                query = "INSERT INTO tblNotas(motivo, fecha, id_empleado, tipo) ";
                query += "VALUES('{0}','{1}',{2},{3})";
                query = string.Format(query, motivo, fecha, codEmpleado, 1);
                // Execute two separate commands.
                command.CommandText = query;
                command.ExecuteNonQuery();
                DataTable tmp = new DataTable();
                tmp = fillDataTable("select Max(id_Notas) as Cod FROM tblNotas ");
                foreach (DataRow r in detalles.Rows)
                {
                    x = "";
                    x = "CALL notasSalida({0},{1},{2},{3},{4},{5})";
                    x = string.Format(x,Convert.ToInt16(r["Cantidad"]), Convert.ToInt16(r["Existencias"]),
                        Convert.ToInt16(r["Nueva Existencia"]), Convert.ToInt16(tmp.Rows[0]["Cod"]), 
                        Convert.ToInt16(r["Codigo Lote"]), sucursal);
                    command.CommandText = x;
                    command.ExecuteNonQuery();
                }

                // Commit the transaction.
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                // Handle the exception if the transaction fails to commit.

                try
                {
                    // Attempt to roll back the transaction.
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    MessageBox.Show("Error " + exRollback + "Info Adicional" + ex.Message);
                }
                throw new Exception("ERROR EN LA TRANSACCION " + ex.Message);
            }

            cnConnection.Close();
        }

        public void regNotaEntrada(string motivo, string fecha, int codEmpleado, int sucursal, DataTable detalles)
        {
            cnConnection.Open();
            // Start a local transaction.
            MySqlTransaction sqlTran = cnConnection.BeginTransaction();
            
            
            // Enlist a command in the current transaction.
            MySqlCommand command = cnConnection.CreateCommand();
            command.Transaction = sqlTran;

            try
            {
                query = "INSERT INTO tblNotas(motivo, fecha, id_empleado, tipo) ";
                query += "VALUES('{0}','{1}',{2},{3})";
                query = string.Format(query, motivo, fecha, codEmpleado, 0);
                // Execute two separate commands.
                command.CommandText = query;
                command.ExecuteNonQuery();
                DataTable tmp = new DataTable();
                tmp = fillDataTable("select Max(id_Notas) as Cod FROM tblNotas ");
                foreach (DataRow r in detalles.Rows)
                {
                    string stmp;
                    string[] codigos;
                    stmp = r["Codigo Producto"].ToString();
                    codigos = stmp.Split('-');

                    MessageBox.Show("Cp:" + codigos[0] + "Cpr:" + codigos[1] + "-"+r["Fecha Caducidad"].ToString());
                    x = "";
                    x = "CALL notasEntrada({0},'{1}',{2},{3},'{4}',{5})";
                    x = string.Format(x, Convert.ToInt16(r["Cantidad"]), codigos[0],
                        Convert.ToInt16(codigos[1]), sucursal,r["Fecha Caducidad"].ToString(),
                        Convert.ToInt16(tmp.Rows[0]["Cod"]));
                    try
                    {
                        command.CommandText = x;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("ERR:"+ex.Message);
                    }
                }

                // Commit the transaction.
                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                // Handle the exception if the transaction fails to commit.

                try
                {
                    // Attempt to roll back the transaction.
                    sqlTran.Rollback();
                }
                catch (Exception exRollback)
                {
                    // Throws an InvalidOperationException if the connection 
                    // is closed or the transaction has already been rolled 
                    // back on the server.
                    MessageBox.Show("Error " + exRollback + "Info Adicional" + ex.Message);
                }
                throw new Exception("ERROR EN LA TRANSACCION " + ex.Message);
            }

            cnConnection.Close();
        }
    }
}
