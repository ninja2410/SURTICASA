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
    public partial class frmdelproducto : Form
    {
        DataAccess da = new DataAccess();
        public frmdelproducto()
        {
            InitializeComponent();
        }
        void cargar()
        {
            string query = "SELECT id_producto as 'Codigo Producto',nombre_producto as 'Nombre Del Producto', codigo_barras as 'Codigo De Barras', activo,descripcion, id_marca as 'Codigo Marca',(select nombre_marca  from tblMarca where id_marca=tblProducto.id_marca) as 'Nombre Marca' , id_categoria as 'Codigo Categoria',(select nombre_categoria  from tblCategoria where id_categoria=tblProducto.id_categoria) as 'Nombre Categoria'   FROM tblProducto "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmdelproducto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void eliminar(string cod, string nombre)
        {
            string sCommand;
            sCommand = "delete from tblProducto where id_producto='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Elimino El producto " + nombre + " Con Exito");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar El Producto, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Del Producto"));
                string cod = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Producto"));

                DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Eliminar este Producto? " + nombre, "Cancelar", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) { eliminar(cod, nombre); } else if (dialog == DialogResult.No) { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
