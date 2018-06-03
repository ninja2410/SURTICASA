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
    public partial class frmAsignarPrecio : Form
    {
        DataAccess da = new DataAccess();
        public frmAsignarPrecio()
        {
            InitializeComponent();
        }
        void cargar_producto()
        {
            string query = "SELECT id_producto as 'Codigo Producto',nombre_producto as 'Nombre Del Producto' FROM tblProducto "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        public int u=0, o=0;
        void cargar_presentacion()
        {
            string query = "SELECT id_Presentacion as 'Codigo Presentacion',tipo_presentacion as 'Tipo De Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView2.Columns.Clear();
            gridControl2.DataSource = dt;
        }
        private void frmAsignarPrecio_Load(object sender, EventArgs e)
        {
            cargar_presentacion();
            cargar_producto();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void agregar()
        {

            #region validaciones
            if (textEdit2.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                textEdit2.Focus();

            }

            else if (textEdit1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                textEdit1.Focus();

            }
            else
            {
                #endregion

                string cod_producto,cod_presentacion;
                decimal pcompra, pventa;
                

                pcompra = Convert.ToDecimal(textEdit1.Text);
                pventa = Convert.ToDecimal(textEdit2.Text);
               

                cod_producto = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo Producto"));
                cod_presentacion = Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Codigo Presentacion"));
                
                string sCommand;
                

                sCommand = "insert into tblAsignacionPrecio(id_Presentacion,id_producto,precio_venta,precio_compra) ";
                sCommand += "values('{0}','{1}','{2}','{3}')";
                sCommand = string.Format(sCommand, cod_presentacion, cod_producto, pventa, pcompra);
               
                try
                {
                    da.executeCommand(sCommand);
                    MessageBox.Show("Se Asigno El Precio Con Exito ");
                   
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar Asignaar un precio Al Producto, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmnewproducto a = new frmnewproducto();
            a.ShowDialog();
            u = a.p;
            if(u==1)
            {
                cargar_producto();
            }
        }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            agregarpresentacion a = new agregarpresentacion();
            a.ShowDialog();
            o = a.u;
            if (o == 1)
            {
                cargar_presentacion();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string presentacion1 = Convert.ToString(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Tipo De Presentacion"));
            string producto = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nombre Del Producto"));

            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Asignar esta Presentacion "+ presentacion1+" a este Producto "+producto+"?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { agregar(); } else if (dialog == DialogResult.No) { }

        }
    }
}
