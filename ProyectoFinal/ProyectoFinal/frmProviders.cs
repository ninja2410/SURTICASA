using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProyectoFinal
{
    public partial class frmProviders : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        bool changes;
        int codigoProveedor;
        string nombre;
        string nit;
        string telfono;
        string contacto;
        bool estado;
        public frmProviders()
        {
            InitializeComponent();
        }

        public void Load_Data()
        {
            string query = "SELECT * FROM tblProveedor"; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridviewProvider.Columns.Clear();
            gridControlProvider.DataSource = dt;
        }
        private void frmProviders_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void btnNewProvider_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNewProvider frmNuevoProveedor = new frmNewProvider();
            frmNuevoProveedor.ShowDialog();
            Load_Data();
        }

        private void btnSaveChanges_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                codigoProveedor = Convert.ToInt32(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "id_proveedor"));
                nombre = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "nombre_proveedor"));
                nit = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "nit"));
                telfono = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "telefono"));
                contacto = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "contacto"));
                estado = Convert.ToBoolean(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "activo"));
                //Actualizamos los valores del registro seleccionado
                DataAccess da = new DataAccess();
                string sCommand;
                sCommand = "update tblProveedor set activo={0}, nombre_proveedor='{1}', nit='{2}', telefono='{3}',contacto='{4}' ";
                sCommand += "where id_proveedor={5}";
                sCommand = string.Format(sCommand, Convert.ToByte(estado), nombre, nit, telfono, contacto, codigoProveedor);
                try
                {
                    da.executeCommand(sCommand); //Enviamos el comando                    
                    MessageBox.Show("Registro Actualizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    changes = false; //cambiamos la bandera a inactiva.
                    Load_Data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to try update Provider: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to get values from gridView " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Load_Data();
            }
            
        }

        private void btnDeleteProvider_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Obteniendo el codigo del cliente
                int codigoSeleccionado = Convert.ToInt32(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "id_proveedor"));
                //Eliminando del registro:
                DataAccess da = new DataAccess();
                string sCommand;
                sCommand = "DELETE FROM tblProveedor where id_proveedor={0}";
                sCommand = string.Format(sCommand, codigoSeleccionado);

                try
                {
                    da.executeCommand(sCommand); //Enviamos el comando                    
                    MessageBox.Show("Registro eliminado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error to try Delete Provider: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error to get the selected Provider" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Load_Data();
        }

        private void gridviewProvider_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            changes = true;
            codigoProveedor = Convert.ToInt32(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "id_proveedor"));
            nombre = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "nombre_proveedor"));
            nit = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "nit"));
            telfono = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "telefono"));
            contacto = Convert.ToString(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "contacto"));
            estado = Convert.ToBoolean(gridviewProvider.GetRowCellValue(gridviewProvider.FocusedRowHandle, "activo"));

        }

        private void gridviewProvider_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (changes == true) {
                DialogResult dialogResult = MessageBox.Show("Desea guardar los cambios realizados", "Guardar...", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DataAccess da = new DataAccess();
                    string sCommand;
                    sCommand = "update tblProveedor set activo={0}, nombre_proveedor='{1}', nit='{2}', telefono='{3}',contacto='{4}' ";
                    sCommand += "where id_proveedor={5}";
                    sCommand = string.Format(sCommand, Convert.ToByte(estado), nombre, nit, telfono, contacto, codigoProveedor);
                    try
                    {
                        da.executeCommand(sCommand); //Enviamos el comando                    
                        MessageBox.Show("Registro Actualizado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        changes = false;
                        codigoProveedor = 0;
                        nit = ""; nombre = ""; telfono = ""; contacto = ""; estado = false;
                        Load_Data();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error to try update Provider: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else {
                    changes = false;
                    codigoProveedor = 0;
                    nit = "";nombre = "";telfono = "";contacto = "";estado = false;
                    Load_Data();
                }
                }
            
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Load_Data();
        }
    }
}