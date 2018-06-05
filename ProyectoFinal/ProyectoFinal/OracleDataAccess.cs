using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;

namespace ProyectoFinal
{
    internal class OracleDataAccess //clase publica dentro del proyecto pero oculta en otros como bisness
    {
        #region attributes
        private OracleConnection cnConnection { get; set; }
        private string ConnectionString = string.Empty;//inicializado cadena vacia
        #endregion


        #region buillder
        public OracleDataAccess()
        {
            ConnectionString = "Data Source=localhost;User Id=HR; Password=database3;";
            //ConnectionString = "Server = arquitecturameso.ddns.net; Database = dbCompra2017; User Id = sa; Password = database;";


            cnConnection = new OracleConnection(ConnectionString);
            
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
                
                OracleDataAdapter da = new OracleDataAdapter(sQuery, cnConnection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Select detail error: "+ex.Message+"\n");
            }
            return dt;
        }
        #endregion
        public DataSet imagen(string sQuery)
        {
            DataSet ds = new DataSet("tblProducto");
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(sQuery, cnConnection);
                da.Fill(ds,"tblProducto");
            }
            catch(Exception ex)
            {
                throw new Exception("Error to read image: " + ex.Message);
            }
            return ds;
        }

        public int executeCommand(string sCommand)//funcion para ejecutar un comando Oracle
        {
            OracleCommand cm = new OracleCommand(sCommand, cnConnection);
            //en el Oracle comand hay que abrir y cerrar conexion
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
        

    }
}
