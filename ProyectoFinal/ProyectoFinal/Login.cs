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
    public partial class Login : Form
    {
        DataAccess da = new DataAccess();
        public Form1 anterior;
        public bool bandera;
        public Login()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            login();
        }

        private void login()
        {
            DataTable dt = new DataTable();
            string query;
            query = "SELECT tblEmpleado.id_empleado, tblEmpleado.id_sucursal, nombre, nombre_sucursal";
            query += " from tblEmpleado INNER JOIN tblSucursal ON tblEmpleado.id_sucursal=tblSucursal.id_sucursal";
            query+=" WHERE usuario='{0}' AND pass='{1}'";
            query = string.Format(query, textEdit1.Text, textEdit2.Text);
            try
            {
                dt = da.fillDataTable(query);
            }
            catch (Exception ex)
            {

                throw new Exception("Error en Login: " + ex.Message);
            }

            if (dt.Rows[0]["id_empleado"].ToString()=="")
            {
                MessageBox.Show("El usuario o la contraseña son incorrectos");
            }
            else
            {
                MessageBox.Show("Bienvenido al sistema");
                Form1 inicio = new Form1();
                inicio.codEmpleado = Convert.ToInt16(dt.Rows[0]["id_empleado"]);
                inicio.codSucursal = Convert.ToInt16(dt.Rows[0]["id_sucursal"]);
                inicio.sucursal = dt.Rows[0]["nombre_sucursal"].ToString();
                inicio.empleado = dt.Rows[0]["nombre"].ToString();
                inicio.anterior = this;
                inicio.Show();
                bandera = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
            
        }
        public void recargar()
        {
            if (bandera)
            {
                anterior.Close();
            }
            bandera = false;
        }
    }
}
