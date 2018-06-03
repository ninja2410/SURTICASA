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
    public partial class agregarpresentacion : Form
    {
        public agregarpresentacion()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        DataAccess da = new DataAccess();
        void cargar()
        {
            string query = "SELECT tipo_presentacion as 'Tipo De Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        public int u = 0;
        private void agregarpresentacion_Load(object sender, EventArgs e)
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



            else
            {
                #endregion

                string nombre;

                nombre = textEdit1.Text;

               


                string sCommand;
                sCommand = "insert into tblPresentacion(tipo_presentacion) ";
                sCommand += "values('{0}')";
                sCommand = string.Format(sCommand, nombre);
                try
                {
                    da.executeCommand(sCommand);
                    MessageBox.Show("Se Ingreso La Presentacion " + nombre + " Con Exito");
                    u = 1;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar ingresar una Nueva Presentacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Ingresar esta Presentacion?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { agregar(); } else if (dialog == DialogResult.No) { }

        }
    }
}
