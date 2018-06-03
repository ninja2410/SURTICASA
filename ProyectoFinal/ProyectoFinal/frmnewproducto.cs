using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProyectoFinal
{
    public partial class frmnewproducto : Form
    {
        public frmnewproducto()
        {
            InitializeComponent();
        }
        DataAccess da = new DataAccess();
        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        public int p = 0;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void agregar()
        {
            bool img = false;
            int cod_presentacion=0;
            #region validaciones

            if (presentacion)
            {
                cod_presentacion = Convert.ToInt32(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Cod Presentacion"));

                if (textEdit5.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                    textEdit5.Focus();
                }
                if (textEdit6.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                    textEdit5.Focus();
                }
                if (cod_presentacion == 0)
                {
                    MessageBox.Show("Debe Seleccionar Una Presentacion Para Guardarlo");
                }
            }
           
            
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
               
                else if (textEdit3.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("Debe Llenar Este Campo Para Guardarlo");
                    textEdit3.Focus();

                }
                else if((imagen==null)&&(img==false))
                  {
                DialogResult dialog = MessageBox.Show("¿Esta seguro que desea continuar guardando este Producto sin imagen?", "Cancelar", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) { img = true;agregar(); } else if (dialog == DialogResult.No) {  }
            }
                else
                {
                    #endregion

                    string nombre, codigo,descripcion,codigo_pro;
                string activo;
                string f_caducidad;
                    int marca, categoria;
                    
                    nombre = textEdit1.Text;
                    codigo = textEdit3.Text;
                    descripcion = textEdit2.Text;
             
                   categoria =  Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cod Categoria"));
                    marca = Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Cod Marca"));
                codigo_pro = textEdit4.Text;
                
                if (checkEdit1.Checked)
                {
                    activo = "1";
                }
                else
                { activo = "0"; }
                string sCommand;
                string sCommand1="";

                    sCommand = "insert into tblProducto(id_producto,nombre_producto,codigo_barras,activo,foto,descripcion,id_marca,id_categoria) ";
                    sCommand += "values('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}')";
                    sCommand = string.Format(sCommand, codigo_pro, nombre, codigo, Convert.ToByte(activo),ruta, descripcion, marca,categoria);
                if(presentacion)
                    { 
                        sCommand1 = "insert into tblAsignacionPrecio(id_Presentacion,id_producto,precio_Venta,precio_Compra) ";
                        sCommand1 += "values('{0}','{1}','{2}','{3}')";
                        sCommand1 = string.Format(sCommand1, cod_presentacion, codigo_pro, Convert.ToDecimal(textEdit5.Text), Convert.ToDecimal(textEdit6.Text));
                    }
                try
                {
                        da.executeCommand(sCommand);
                        MessageBox.Show("Se Ingreso El Producto " + nombre + " Con Exito");
                    File.Copy(imagen, ruta);
                    if (presentacion)
                        {
                        da.executeCommand(sCommand1);
                        MessageBox.Show("Se Asigno El Precio Con Exito");
                         }
                    p = 1;
                    this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al intentar ingresar un nuevo Producto, más detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Esta seguro que desea Ingresar este Producto?", "Cancelar", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) { agregar(); } else if (dialog == DialogResult.No) { }
        }
        void cargar_cat()
        {
            string query = "SELECT id_categoria as 'Cod Categoria',nombre_categoria as 'Nombre Categoria' FROM tblCategoria "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
        }
        void cargar_marcas()
        {
            string query = "SELECT id_marca as 'Cod Marca', nombre_marca as 'Nombre Marca' FROM tblMarca "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView2.Columns.Clear();
            gridControl2.DataSource = dt;
        }
        void cargar_presentaciones()
        {
            string query = "SELECT id_Presentacion as 'Cod Presentacion', tipo_presentacion as 'Tipo Presentacion' FROM tblPresentacion "; //Consulta que se enviara al servidor de la base
            DataTable dt = new DataTable();           // creando una nueva tabla
            dt = da.fillDataTable(query); //Obteniendo los datos para llenar la tabla de clientes registrados
            gridView3.Columns.Clear();
            gridControl3.DataSource = dt;
        }
        private void frmnewproducto_Load(object sender, EventArgs e)
        {
            cargar_cat();
            cargar_marcas();
            
        }
        string imagen = null;
        string ruta=null;
        
        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    imagen = openFileDialog1.FileName;
                    string ext = Path.GetExtension(openFileDialog1.FileName);
                    pictureEdit1.Image = Image.FromFile(imagen);
                    
                    ruta = "images/" + textEdit4.Text+ext;
                    
                   
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido "+ex.ToString());
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }
        public int j = 0;
        public int j1 = 0;
        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmnewcategoria a = new frmnewcategoria();

            a.ShowDialog();
            j1 = a.u;
            if (j1 == 1)
            {
                cargar_cat();
            }
        }

        private void gridControl2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmnewmarca a = new frmnewmarca();

            a.ShowDialog();
            j = a.u;
            if (j == 1)
            {
                cargar_marcas();
            }
        }

        private void gridControl3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void gridControl3_MouseClick(object sender, MouseEventArgs e)
        {

        }
        bool presentacion = false;

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            presentacion = !presentacion;
            if(presentacion)
            {
                cargar_presentaciones();
                gridControl3.Enabled = true;
                textEdit5.Enabled = true;
                textEdit6.Enabled = true;
                simpleButton4.Text = "Cancelar";
            }
            else
            {
                simpleButton4.Text = "Asignar Presentacion";
                gridControl3.Enabled = false;
                textEdit5.Enabled = false;
                textEdit6.Enabled = false;
            }
        }

        private void gridControl3_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            agregarpresentacion a = new agregarpresentacion();
            a.ShowDialog();
            j1 = a.u;
            if (j1 == 1)
            {
                cargar_presentaciones();
            }
        }
    }
}
