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
    public partial class frmverproducto : Form
    {
        DataAccess da = new DataAccess();
        public frmverproducto()
        {
            InitializeComponent();
        }
       
            void cargar()
        {
                string query = "SELECT id_producto as 'Codigo Producto',nombre_producto as 'Nombre Del Producto', codigo_barras as 'Codigo De Barras', activo,foto as 'Direccion Portada',descripcion, f_caducidad as 'Fecha De Caducidad', id_marca as 'Codigo Marca',(select nombre_marca  from tblMarca where id_marca=tblProducto.id_marca) as 'Nombre Marca' , id_categoria as 'Codigo Categoria',(select nombre_categoria  from tblCategoria where id_categoria=tblProducto.id_categoria) as 'Nombre Categoria'   FROM tblProducto "; //Consulta que se enviara al servidor de la base
                DataTable dt = new DataTable();           // creando una nueva tabla
                dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
                gridView1.Columns.Clear();
                gridControl1.DataSource = dt;
            }
        
        private void frmverproducto_Load(object sender, EventArgs e)
        {
            cargar();
        }
    }
}
