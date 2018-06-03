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
    public partial class frmnewsucursal : Form
    {
        DataAccess da = new DataAccess();
        public frmnewsucursal()
        {
            InitializeComponent();
        }
        void agregar()
        {
            if (seleccion == 0)
            {
                MessageBox.Show("DEBE SELECIONAR UN EMPLEADO ANTES DE CONTINUAR");
            }
            else
            { 
            #region validaciones

            if (textEdit2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                textEdit2.Focus();

            }

           else if (textEdit1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                textEdit1.Focus();

            }
           
            else
                { 
            #endregion

            string nombre, direccion;
            int encargado; 
            string activo;
            nombre = textEdit2.Text;
            direccion = textEdit1.Text;
            if(checkEdit1.Checked)
                    {
                        activo = "1";
                    }
            else
                        { activo = "0"; }
            encargado =seleccion;
                   
                    string sCommand;
            sCommand = "insert into tblSucursal(nombre_sucursal,activo,direccion,id_empleado) ";
            sCommand += "values('{0}',{1},'{2}','{3}')";
            sCommand = string.Format(sCommand, nombre, Convert.ToByte(activo), direccion,encargado);
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Ingreso La Sucursal "+nombre+" Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar ingresar una nueva Sucursal, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Ingresar esta Sucursal?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { agregar(); } else if (dialog == DialogResult.No) { }
        }
        void cargar()
        {
            string query = "SELECT id_empleado as 'COD EMPLEADO',nombre as 'Nombre Del Empleado',apellido as 'Apellido Del Empleado',direccion as 'Direccion Del Empleado' FROM tblEmpleado "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmnewsucursal_Load(object sender, EventArgs e)
        {
            cargar();
        }
        
        int seleccion = 0;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(simpleButton2.Text== "SELECCIONAR")
            { 
                seleccion = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "COD EMPLEADO"));
                simpleButton2.Text = "DESELECCIONAR";
                gridControl1.Enabled = false;
                labelControl1.Text = "EMPLEADO SELECCIONADO : " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Del Empleado"));
              }
            else
            {
                seleccion = 0;
                simpleButton2.Text = "SELECCIONAR";
                gridControl1.Enabled = true;
                labelControl1.Text = "Ningun Empleado Seleccionado";

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
