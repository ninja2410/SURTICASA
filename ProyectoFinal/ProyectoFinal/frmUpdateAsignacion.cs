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
    public partial class frmUpdateAsignacion : Form
    {
        DataAccess da = new DataAccess();
        public frmUpdateAsignacion()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                simpleButton2.Enabled = true;
                lookUpEdit1.Enabled = true;
                lookUpEdit2.Enabled = true;

                textEdit1.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Precio Compra Del Producto"));
                textEdit2.Text = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Precio Venta Del Producto"));
                lookUpEdit1.EditValue = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Producto"));
                lookUpEdit2.EditValue = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Presentacion"));
                cod = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Asignacion"));

            }
            catch (Exception ex)
            {

                
            }
              }
        void cargar_combo2()
        {
            string query = "select id_Presentacion,tipo_presentacion from tblPresentacion"; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            lookUpEdit2.Properties.DataSource = dt;
            lookUpEdit2.Properties.DisplayMember = "tipo_presentacion";
            lookUpEdit2.Properties.ValueMember = "id_Presentacion";


        }
        void cargar_combo1()
        {
            string query = "select id_producto,nombre_producto from tblProducto"; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.DisplayMember = "nombre_producto";
            lookUpEdit1.Properties.ValueMember = "id_producto";


        }
        int cod=0;
        void mod()
        {
            string cod_producto, cod_presentacion;
            decimal pcompra, pventa;
            pventa = Convert.ToDecimal(textEdit2.Text);
            pcompra =Convert.ToDecimal( textEdit1.Text);
           
            cod_producto = lookUpEdit1.EditValue.ToString();
            cod_presentacion = lookUpEdit2.EditValue.ToString();
            
            string sCommand;
            sCommand = "UPDATE tblAsignacionPrecio SET id_Presentacion='" + cod_presentacion + "', id_producto='" + cod_producto + "',precio_venta='" + pventa + "',precio_compra='" + pcompra + "' WHERE id_asignacionprecio='" + cod + "'";

            try
            {
                da.executeCommand(sCommand);
                MessageBox.Show("Se Modifico la Asignacion Con Exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Modificar la Asignacion del producto, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void cargar()
        {
            string query = "SELECT id_asignacionprecio as 'Codigo Asignacion',id_Presentacion as 'Codigo Presentacion', (select tipo_presentacion  from tblPresentacion where id_Presentacion=tblAsignacionPrecio.id_Presentacion) as 'Nombre Presentacion',id_producto as 'Codigo Producto', (select nombre_producto  from tblProducto where id_producto=tblAsignacionPrecio.id_producto) as 'Nombre Producto',precio_compra as 'Precio Compra Del Producto', precio_venta as 'Precio Venta Del Producto'  FROM tblAsignacionPrecio "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        private void frmUpdateAsignacion_Load(object sender, EventArgs e)
        {
            cargar();
            cargar_combo1();
            cargar_combo2();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Modificar esta Asignacion?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { mod(); } else if (dialog == DialogResult.No) { }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
