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
    public partial class frmKardexProduct : DevExpress.XtraEditors.XtraForm
    {
        DataAccess da = new DataAccess();
        public frmKardexProduct()
        {
            InitializeComponent();
        }

        private void frmKardexProduct_Load(object sender, EventArgs e)
        {
            initData();
        }

        void initData()
        {
            //Cargamos las sucursales disponibles
            
             //CARGAMOS LOS PROVEEDORES DE LAS COMPRAS REALIZADAS
                gridLookUpEdit1.Properties.View.Columns.Clear();
                gridLookUpEdit1.Properties.DataSource = da.fillDataTable("select id_sucursal, nombre_sucursal from tblSucursal");
                gridLookUpEdit1.Properties.ValueMember = "id_sucursal";
                gridLookUpEdit1.Properties.DisplayMember = "nombre_sucursal";

        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            int idSucursal = Convert.ToInt16( gridLookUpEdit1.EditValue);
            //CARGAMOS LOS PRODUCTOS EXISTENTES.
            string query = "SELECT tblProducto.id_producto, tblProducto.nombre_producto from tblKardex ";
            query += "inner join tblAsignacionPrecio ON tblAsignacionPrecio.id_asignacionprecio = tblKardex.id_asignacionprecio ";
            query += "inner join tblProducto ON tblProducto.id_producto = tblAsignacionPrecio.id_producto where tblKardex.id_sucursal = {0} group by id_producto";
            query = string.Format(query, idSucursal);
            gridLookUpEdit2.Properties.DataSource = da.fillDataTable(query);
            gridLookUpEdit2.Properties.ValueMember = "id_producto";
            gridLookUpEdit2.Properties.DisplayMember = "nombre_producto";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string sucursal = gridLookUpEdit1.EditValue.ToString();
            string miQuery = "select nombre_sucursal from tblSucursal where id_sucursal = {0}";
            miQuery = string.Format(miQuery, sucursal);
            

            
            string idProducto= gridLookUpEdit2.EditValue.ToString();

            reporteKardex miReporte = new reporteKardex();
            documentViewer1.DocumentSource = miReporte;
            miReporte.InitData(da.executeScalar(miQuery).ToString() , idProducto);
            miReporte.CreateDocument();
        }
    }
}