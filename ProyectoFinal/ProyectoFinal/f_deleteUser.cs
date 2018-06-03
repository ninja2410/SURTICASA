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
    public partial class f_deleteUser : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public f_deleteUser()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToByte(cmbDelUser.EditValue);
            if (MessageBox.Show("¿Esta seguro que desea eliminar ha este usuario?", "Advertencia",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                string sQuery = "delete tblEmpleado where id_empleado = " + id;
                da.executeCommand(sQuery);
                MessageBox.Show("Elemnto eliminado.", "Eliminación terminada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cancelar?", "",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}