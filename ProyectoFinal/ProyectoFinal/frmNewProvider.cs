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
    public partial class frmNewProvider : DevExpress.XtraEditors.XtraForm
    {
        public frmNewProvider()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            string nit = txtNitProvider.Text;
            string nombre = txtNameProvider.Text;
            string telefono = txtPhoneProvider.Text;
            string contacto = txtContact.Text;
            bool state = chkActivo.Checked;

            string sCommand = "insert into tblProveedor(nit,nombre_proveedor,telefono,contacto,activo) ";
            sCommand += "values('{0}','{1}','{2}','{3}',{4})";
            sCommand = string.Format(sCommand, nit, nombre, telefono, contacto, Convert.ToByte(state));
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Proveedor almacenado correctamente", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to try add a new Provider: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}