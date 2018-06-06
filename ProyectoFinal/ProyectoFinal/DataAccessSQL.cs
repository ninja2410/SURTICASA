using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    public class DataAccessSQL
    {
        #region attributes
        private SqlConnection cnConnection { get; set; }
        private string connectionString = string.Empty;
        #endregion
        #region builder
        public DataAccessSQL()
        {
            //connectionString = "Data Source=DESKTOP-36SIUND;Initial Catalog=bitcoradb;Persist Security Info=True;User ID=sa; Password=database;";
            connectionString = "Data Source=LAPTOP-4F9A2N1N;Initial Catalog=bitacoradb;Persist Security Info=True;User ID=sa; Password=database;";
            cnConnection = new SqlConnection(connectionString);
        }
        #endregion
        #region method
        public void openConnection()
        {
            try
            {
                if (cnConnection.State != ConnectionState.Open)
                {
                    cnConnection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error opening connection, Error detail: " + ex.Message + "\n");
            }
        }
        public void closeConnection()
        {
            try
            {
                if (cnConnection.State != ConnectionState.Closed)
                {
                    cnConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error closing connection, Error detail:" + ex.Message + "\n");
            }
        }

        public DataTable fillDataTable(string sQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sQuery, cnConnection);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error querying data, detail error:" + ex.Message);
            }
            return dt;
        }
        public int executeCommand(string sCommand)
        {
            SqlCommand cm = new SqlCommand(sCommand, cnConnection);
            try
            {
                cm.Connection.Open();
                return cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing command: " + ex.Message);
            }
            finally
            {
                cm.Connection.Close();
            }
        }

        #endregion

    }
}
