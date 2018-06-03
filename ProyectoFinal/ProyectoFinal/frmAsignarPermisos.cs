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
    public partial class frmAsignarPermisos : Form
    {
        int[] codigonuevo = new int[100];
        public frmAsignarPermisos()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        void cargar_empleados()
        {
           
            string query = "SELECT id_empleado, usuario, nombre FROM tblEmpleado";
            lookUpEdit1.Properties.DataSource = da.fillDataTable(query);
            lookUpEdit1.Properties.ValueMember = "id_empleado";
            lookUpEdit1.Properties.DisplayMember = "nombre";
        }
        void cargar()
        {
            
            string query = "SELECT id_rol as 'codigo rol',nombre_rol as 'Nombre Del Rol' FROM tblRoles "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;

        }
        private void frmAsignarPermisos_Load(object sender, EventArgs e)
        {
           
            cargar_empleados();
            
        }
        void cargar_permisos_asignados(int cod)
        {
           
            string query = "SELECT id_rol as 'codigo rol',(select nombre_rol  from tblRoles where (id_rol=tblPermiso.id_rol) AND (activo=1)) as 'Nombre Rol' FROM tblPermiso where id_empleado='" + cod + "'";
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query);
            gridView2.Columns.Clear();
            gridControl2.DataSource = dt;



        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            
            cargar_permisos_asignados(Convert.ToInt32(lookUpEdit1.EditValue));
            cod2 = Convert.ToInt32(lookUpEdit1.EditValue);
            cargar();
        }
        void asignar(int cod_empleado,int cod_rol)
        {
            
            string sCommand;
            sCommand = "insert into tblPermiso(id_empleado,id_rol) Values('{0}','{1}') ";
            sCommand = string.Format(sCommand, cod_empleado,cod_rol);
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se asigno Correctamente Con Exito");
                cargar_permisos_asignados(cod_empleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Ya Se Encuentra Asignado El Rol");
                
            }
            simpleButton1.Enabled = false;
            layoutControlItem2.Text = "Seleccionado Actualmente: NINGUNO";
        }
        int cod1 = 0,cod2=0;

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                simpleButton1.Enabled = true;
                cod1 = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "codigo rol"));
                layoutControlItem2.Text = "Seleccionado Actualmente: " + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Del Rol").ToString();

            }
            catch (Exception)
            {

               
            }
       }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                quitarasign.Enabled = true;
                cod1 = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "codigo rol"));
                layoutControlItem3.Text = "Seleccionado Actualmente: " + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Nombre Rol").ToString();

            }
            catch (Exception)
            {

               
            }
              }
        void quitar(int cod_empleado,int cod_rol)
        {
            string sCommand;
            sCommand = "delete from tblPermiso where (id_rol='" + cod_rol + "')AND (id_empleado='" + cod_empleado + "')";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Asignacion Con Exito");
                cargar_permisos_asignados(cod_empleado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar la Asignacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            quitarasign.Enabled = false;
            layoutControlItem3.Text = "Seleccionado Actualmente: NINGUNO";
        }
        private void quitarasign_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Quitar este Permiso?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { quitar(cod2, cod1); } else if (dialog == DialogResult.No) { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Asignar este Permiso?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { asignar(cod2,cod1); } else if (dialog == DialogResult.No) { }
        }
    }
}
