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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();

        int codigoSeleccionado;
        string nit;
        string nombre;
        string apellido;
        string telefono;
        string codigoMayorista;


        bool changes = false;

        public void Load_Data()
        {
            string query = "SELECT * FROM tblCliente "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();     
            gridControlCliente.DataSource = dt;
        }
        private void frmClientes_Load(object sender, EventArgs e)
        {
            Load_Data(); // Cargamos los datos al grid view;
        }

        private void btnNewClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewClient miNuevocliente = new frmnewClient();
            miNuevocliente.ShowDialog();
            Load_Data();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            changes = true; //Bandera que indica que se hicieron cambios y obtenemos los datos de toda la fila.
            codigoSeleccionado = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id_cliente"));
            nit = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "nit"));
            nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "nombre"));
            apellido = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "apellido"));
            telefono = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "telefono"));
            codigoMayorista = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "codigoMayorista"));

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (changes == true) //si hay cambios hechos preguntar si desea guardarlos o no.
            {
                DialogResult dialogResult = MessageBox.Show("Desea guardar los cambios realizados", "Guardar...", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DataAccess da = new DataAccess();
                    string sCommand;
                    sCommand = "update tblCliente set nit='{0}', nombre='{1}', apellido='{2}', telefono='{3}', codigoMayorista='{4}' ";
                    sCommand += "where id_cliente={5}";
                    sCommand = string.Format(sCommand, nit, nombre, apellido, telefono, codigoMayorista, codigoSeleccionado);
                    try
                    {
                        da.executeCommand(sCommand); //Enviamos el comando               
                        changes = false;
                        codigoSeleccionado = 0;
                        nit = ""; nombre = ""; apellido = "";telefono = "";codigoMayorista = "";
                        MessageBox.Show("Registro Actualizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_Data();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error to try update Client: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Load_Data();
                    }
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    // Si no desea guardar cambios.
                    changes = false;
                    codigoSeleccionado = 0;
                    nit = ""; nombre = ""; apellido = ""; telefono = ""; codigoMayorista = "";
                    Load_Data();
                }
                
            }
            

        }

        private void btnDeleteClient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Obteniendo el codigo del cliente
                int codigoSeleccionado = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id_cliente"));
                //Eliminando del registro:
                DataAccess da = new DataAccess();
                string sCommand;
                sCommand = "DELETE FROM tblCliente where id_cliente={0}";
                sCommand = string.Format(sCommand, codigoSeleccionado);

                try
                {
                    da.executeCommand(sCommand); //Enviamos el comando                    
                    MessageBox.Show("Registro eliminado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to try Delete Client: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Load_Data();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error to get the selected Client" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveChanges_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int codigoSeleccionado = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "id_cliente"));
                string nit = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "nit"));
                string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "nombre"));
                string apellido = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "apellido"));
                string telefono = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "telefono"));
                string codigoMayorista = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "codigoMayorista"));
                //Actualizamos los valores del registro seleccionado
                DataAccess da = new DataAccess();
                string sCommand;
                sCommand = "update tblCliente set nit='{0}', nombre='{1}', apellido='{2}', telefono='{3}', codigoMayorista='{4}' ";
                sCommand += "where id_cliente={5}";
                sCommand = string.Format(sCommand, nit, nombre, apellido, telefono, codigoMayorista, codigoSeleccionado);
                try
                {
                    da.executeCommand(sCommand); //Enviamos el comando                    
                    MessageBox.Show("Registro Actualizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    changes = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to try update Client: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to get values from gridView " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Load_Data();
        }
    }
}
