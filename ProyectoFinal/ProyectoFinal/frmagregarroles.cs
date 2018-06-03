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
    public partial class frmagregarroles : Form
    {
        public frmagregarroles()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT nombre_rol as 'Nombre Del Rol' FROM tblRoles "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmagregarroles_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void agregar()
        {
            #region validaciones

            if (textEdit1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                textEdit1.Focus();

            }

#endregion

            else
            {
                

                string nombre, activo;

                nombre = textEdit1.Text;

                if (checkEdit1.Checked)
                {
                    activo = "1";
                }
                else
                {
                    activo = "0";
                }


                string sCommand;
                sCommand = "insert into tblRoles(nombre_rol,activo) ";
                sCommand += "values('{0}',{1})";
                sCommand = string.Format(sCommand, nombre, Convert.ToByte(activo));
                try
                {
                    da.executeCommand(sCommand);
                    MessageBox.Show("Se Ingreso el ROL " + nombre + " Con Exito");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar ingresar un Nuevo ROL, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Ingresar este ROL?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { agregar(); } else if (dialog == DialogResult.No) { }
        }
    }
}
