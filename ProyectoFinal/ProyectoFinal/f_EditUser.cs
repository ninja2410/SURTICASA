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
    public partial class f_EditUser : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        int idEmpleado;

        public f_EditUser()
        {
            InitializeComponent();
        }

        private void f_EditUser_Load(object sender, EventArgs e)
        {
            cmbEditUser.Properties.DataSource = da.fillDataTable("SELECT * FROM tblEmpleado");
            cmbEditUser.Properties.DisplayMember = "nombre";
            cmbEditUser.Properties.ValueMember = "id_empleado";

            btnUpdate.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (txtFirstName.Text.Trim() == String.Empty)
                txtFirstName.Focus();

            if (txtLastName.Text.Trim() == String.Empty)
                txtLastName.Focus();

            if (txtTel.Text.Trim() == String.Empty)
                txtTel.Focus();

            if (txtDir.Text.Trim() == String.Empty)
                txtDir.Focus();

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
            dir = txtDir.Text;
            email = txtEmail.Text;
            user = txtUser.Text;
            pass = txtPass.Text;

           // idEmpleado = Convert.ToByte(cmbEditUser.EditValue);
            string sCommand;

           

            sCommand = "update tblEmpleado set nombre = '{0}', apellido = '{1}' , telefono = '{2}', direccion = '{3}', email = '{4}', usuario = '{5}', pass = '{6}' ";
            sCommand += " where id_empleado = {7} ";
            sCommand = string.Format(sCommand, firtName, lastName, telefono, dir, email, user, pass, idEmpleado);
            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Usuario actualizado con exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar actualizar el usuario, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPass_EditValueChanged(object sender, EventArgs e)
        {
            txtPass.Properties.PasswordChar = '■';
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            idEmpleado = Convert.ToInt16(cmbEditUser.EditValue);
            string sQuery = "select * from tblEmpleado where id_empleado = " + idEmpleado;
            DataTable dt = da.fillDataTable(sQuery);

            if (dt.Rows.Count > 0)
            {
                txtFirstName.Text = Convert.ToString(dt.Rows[0]["nombre"]);
                txtLastName.Text = Convert.ToString(dt.Rows[0]["apellido"]);
                txtTel.Text = Convert.ToString(dt.Rows[0]["telefono"]);
                txtDir.Text = Convert.ToString(dt.Rows[0]["direccion"]);
                txtEmail.Text = Convert.ToString(dt.Rows[0]["email"]);
                txtUser.Text = Convert.ToString(dt.Rows[0]["usuario"]);
                txtPass.Text = Convert.ToString(dt.Rows[0]["pass"]);

                btnUpdate.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cancelar?", "",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}