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
using System.Security.Cryptography;

namespace ProyectoFinal
{
    public partial class f_Usuarios : DevExpress.XtraEditors.XtraForm
    {
        public f_Usuarios()
        {
            InitializeComponent();
        }

        DataAccess da = new DataAccess();

        private void txtFirstName_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void f_Usuarios_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea cancelar esta acción?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { this.Close(); } else if (dialog == DialogResult.No) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (txtFirstName.Text.Trim() == String.Empty)
                txtFirstName.Focus();

            if (txtLastName.Text.Trim() == String.Empty)
                txtLastName.Focus();

            if (txtTel.Text.Trim() == String.Empty)
                txtTel.Focus();

            if (txtDirec.Text.Trim() == String.Empty)
                txtDirec.Focus();

            if (txtEmail.Text.Trim() == String.Empty)
                txtEmail.Focus();

            if (txtUser.Text.Trim() == String.Empty)
                txtUser.Focus();

            if (txtPass.Text.Trim() == String.Empty)
                txtPass.Focus();

            #endregion

            string firtName, lastName, telefono, dir, email, user, pass;

            firtName = txtFirstName.Text;
            lastName = txtLastName.Text;
            telefono = txtTel.Text;
            dir = txtDirec.Text;
            email = txtEmail.Text;
            user = txtUser.Text;
            pass = txtPass.Text;

            string sCommand;

            Random rd = new Random();
            int idEmpleado = rd.Next(999);

            sCommand = "insert into tblEmpleado(id_empleado,nombre,apellido,telefono,direccion,email,usuario,pass) ";
            sCommand += "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            sCommand = string.Format(sCommand,idEmpleado, firtName,lastName, telefono, dir,email,user,pass);
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Usuario ingresado con exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar ingresar un nuevo usuario, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtPass_EditValueChanged(object sender, EventArgs e)
        {
            txtPass.Properties.PasswordChar = '■';
        }
    }
}