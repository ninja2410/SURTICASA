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
    public partial class f_Notas : Form
    {
        DataAccess da = new DataAccess();
        public int sucursal;
        public bool entrada=true;
        DataTable dt = new DataTable();
        public int empleado;
        string tpres;
        public f_Notas()
        {
            InitializeComponent();
        }

        private void f_Notas_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            string query;
            if (entrada == false)
            {
                lblTitulo.Text = "Notas de Salida";
            }
            else
            {
                lblTitulo.Text = "Notas de Entrada";
            }
            
            dt.Columns.Add("Codigo Lote");
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Presentacion");
            dt.Columns.Add("Fecha Caducidad");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Existencias");
            dt.Columns.Add("Nueva Existencia");
            gridControl1.DataSource = dt;
            gridControl1.Refresh();

            //falta filtrar por sucursal
            query = "SELECT id_producto, nombre_producto from listarProductos WHERE id_sucursal={0}";
            query = string.Format(query, sucursal);
            lProducto.Properties.DataSource = da.fillDataTable(query);
            lProducto.Properties.DisplayMember = "nombre_producto";
            lProducto.Properties.ValueMember = "id_producto";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelControl2.Text = DateTime.Now.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow r = dt.NewRow();
            r["Codigo Lote"] = lProductos.EditValue;
            r["Descripción"] = lProductos.Text;
            lProductos.Properties.DisplayMember = "Presentacion";
            r["Presentacion"] = lProductos.Text;
            lProductos.Properties.DisplayMember = "fecha_caducidad";
            r["Fecha Caducidad"]=lProductos.Text;
            r["Cantidad"] = txtCantidad.Text;
            lProductos.Properties.DisplayMember = "Existencias";
            r["Existencias"] = lProductos.Text;
            if (entrada)
            {
                r["Nueva Existencia"] = Convert.ToInt16(txtCantidad.Text) + Convert.ToInt16(lProductos.Text);

            }
            else
            {
                r["Nueva Existencia"] =   Convert.ToInt16(lProductos.Text)- Convert.ToInt16(txtCantidad.Text);

            }
            dt.Rows.Add(r);
            gridControl1.Refresh();
            gridControl1.RefreshDataSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificar())
            {
                //mandar a llamar a data acces para la transaccion
                if (entrada)
                {

                }
                else
                {
                    da.regNotaSalida(txtMotivo.Text, DateTime.Now.ToString("yyyy-MM-dd"), empleado,
                        sucursal, dt);
                    MessageBox.Show("Nora resigrada con Exito");
                    txtMotivo.Text = "";
                    txtCantidad.Text = "";
                    dt.Rows.Clear();
                    gridControl1.Refresh();
                    gridControl1.RefreshDataSource();

                }
            }
        }
        private bool verificar()
        {
            bool resultado = true;
            if (txtMotivo.Text == "")
            {
                MessageBox.Show("Debe ingresar un motivo");
                txtMotivo.Focus();
                resultado = false;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar almenos un producto");
                txtCantidad.Focus();
                resultado = false;
            }
            return resultado;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lProducto_EditValueChanged(object sender, EventArgs e)
        {
            string q;
            q = "SELECT a.id_presentacion as Codigo, tipo_presentacion as Presentación from tblAsignacionPrecio as a INNER JOIN tblPresentacion as p on a.id_Presentacion=p.id_Presentacion";
            q += " WHERE id_producto='{0}'";
            q = string.Format(q, lProducto.EditValue.ToString());
            lPresentacion.Properties.DataSource = da.fillDataTable(q);
            lPresentacion.Properties.ValueMember = "Codigo";
            lPresentacion.Properties.DisplayMember = "Presentación";
        }

        private void lPresentacion_EditValueChanged(object sender, EventArgs e)
        {
            string query;
            query = "SELECT * FROM productosExistencias WHERE id_sucursal={0} AND Existencias>0 AND Descripcion='{1}' AND Presentacion='{2}'";
            query = string.Format(query, sucursal, lProducto.Text, lPresentacion.Text);
            lProductos.Properties.DataSource = da.fillDataTable(query);
            lProductos.Properties.DisplayMember = "Descripcion";
            lProductos.Properties.ValueMember = "Lote";
        }
    }
}
