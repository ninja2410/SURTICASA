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
    public partial class frmDelAsignacionprecio : Form
    {
        public frmDelAsignacionprecio()
        {
            InitializeComponent();
        }
        void cargar()
        {
            string query = "SELECT id_asignacionprecio as 'Codigo Asignacion',id_Presentacion as 'Codigo Presentacion', (select tipo_presentacion  from tblPresentacion where id_Presentacion=tblAsignacionPrecio.id_Presentacion) as 'Nombre Presentacion',id_producto as 'Codigo Producto', (select nombre_producto  from tblProducto where id_producto=tblAsignacionPrecio.id_producto) as 'Nombre Producto',precio_compra as 'Precio Compra Del Producto', precio_venta as 'Precio Venta Del Producto'  FROM tblAsignacionPrecio "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmDelAsignacionprecio_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataAccess da = new DataAccess();
        void eliminar(string cod)
        {
            string sCommand;
            sCommand = "delete from tblAsignacionPrecio where id_asignacionprecio='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino La Asignacion Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar La Asignacion, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                 string cod = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Asignacion"));

                DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar esta Asignacion del  Producto? " + cod, "Cancelar", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) { eliminar(cod); } else if (dialog == DialogResult.No) { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }
    }
}
