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
       
    public partial class frmnewClient : DevExpress.XtraEditors.XtraForm
    {
        public frmnewClient()
        {
            InitializeComponent();
        }
        string sCommand;
        string sQuery;

        private void btnSave_Click(object sender, EventArgs e)
        {
            //creamos el acceso a la base
            DataAccess da = new DataAccess();
            Random rd= new Random();
            int codeClient = rd.Next(999);

            sCommand = "insert into tblCliente(id_cliente,nit,nombre,apellido,telefono) ";
            sCommand += "values({0},'{1}','{2}','{3}','{4}')";
            sCommand = string.Format(sCommand,codeClient.ToString(), txtNit.Text,txtNombre.Text, txtApellido.Text, txtTelefono.Text);
            try
            {
                da.executeCommand(sCommand);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to try add a new Client: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmnewClient_Load(object sender, EventArgs e)
        {

        }
    }
}