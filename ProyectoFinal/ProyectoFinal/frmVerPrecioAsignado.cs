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
    public partial class frmVerPrecioAsignado : Form
    {
        DataAccess da = new DataAccess();
        public frmVerPrecioAsignado()
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
        private void frmVerPrecioAsignado_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmAsignarPrecio a = new frmAsignarPrecio();
            a.ShowDialog();
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
