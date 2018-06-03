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
    public partial class f_NotasEntrada : Form
    {
        public int sucursal;
        public int empleado;
        string x;
        DataTable dt = new DataTable();
        DataAccess da = new DataAccess();
        public f_NotasEntrada()
        {
            InitializeComponent();
        }

        private void f_NotasEntrada_Load(object sender, EventArgs e)
        {

            timer1.Enabled = true;
            x = "SELECT id_producto, nombre_producto from listarProductos where id_sucursal={0}";
            x = string.Format(x, sucursal);
            lProducto.Properties.DataSource = da.fillDataTable(x);
            lProducto.Properties.DisplayMember = "nombre_producto";
            lProducto.Properties.ValueMember = "id_producto";
            dt.Columns.Add("Codigo Producto");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Presentacion");
            dt.Columns.Add("Fecha Caducidad");

            gridControl1.DataSource = dt;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhora.Text = DateTime.Now.ToString();
        }

        private void lProducto_EditValueChanged(object sender, EventArgs e)
        {
            x = "SELECT id_asignacionprecio, tipo_presentacion ";
            x+=" from tblAsignacionPrecio as a INNER JOIN tblPresentacion as b";
            x += " ON a.id_Presentacion=b.id_Presentacion WHERE id_producto='{0}'";
            x = string.Format(x, lProducto.EditValue.ToString());
            lPresentacion.Properties.DataSource = da.fillDataTable(x);
            lPresentacion.Properties.DisplayMember = "tipo_presentacion";
            lPresentacion.Properties.ValueMember = "id_asignacionprecio";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (verificarDetalle())
            {
                DataRow r = dt.NewRow();
                r["Codigo Producto"] = lProducto.EditValue.ToString() + "-" + lPresentacion.EditValue.ToString();
                r["Cantidad"] = txtCantidad.Text;
                r["Producto"] = lProducto.Text;
                r["Presentacion"] = lPresentacion.Text;
                r["Fecha Caducidad"] = dFecha.Value.ToString("yyyy-MM-dd");
                dt.Rows.Add(r);
                gridControl1.Refresh();
                gridControl1.RefreshDataSource();
            }
        }
        private bool verificarDetalle()
        {
            bool respuesta = true;
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("Debe ingresar una cantidad");
                txtCantidad.Focus();
                respuesta = false;
            }
            if (lProducto.EditValue.ToString() == "")
            {
                MessageBox.Show("Debe seleccionar un producto");
                lProducto.Focus();
                respuesta = false;
            }
            if (lPresentacion.EditValue.ToString() == "")
            {
                MessageBox.Show("Debe seleccionar una presentacion");
                lPresentacion.Focus();
                respuesta = false;
            }
            return respuesta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verificar())
            {
                try
                {
                    da.regNotaEntrada(txtMotivo.Text, DateTime.Now.ToString("yyyy-MM-dd"), empleado, sucursal, dt);
                    MessageBox.Show("Nota de entrada registrada correctamente");
                    txtMotivo.Text = "";
                    txtCantidad.Text = "";
                    dt.Rows.Clear();
                }
                catch (Exception ex)
                {

                    throw new Exception("Error en Transaccion notas entrada " + ex.Message);
                }
            }
        }
        private bool verificar()
        {
            bool respuesta = true;
            if (txtMotivo.Text == "")
            {
                respuesta = false;
                MessageBox.Show("Debe ingresar un motivo de nota");
            }
            if (dt.Rows.Count == 0)
            {
                respuesta = false;
                MessageBox.Show("Debe seleccionar al menos un producto");
            }
            return respuesta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
