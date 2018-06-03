using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ProyectoFinal
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataAccess da = new DataAccess();
        //VARIABLES GLOBALES
        //ID CAJA
        int idCaja=0;
        public int codSucursal;
        public int codEmpleado=1;
        public string empleado;
        public string sucursal;
        public Login anterior=new Login();
        public Form1()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_ventas v = new f_ventas();
            v.MdiParent = this;
            v.sucursal = codSucursal;
            v.empleado = codEmpleado;
            v.venta = true;
            v.Show();
        }

        

        private void btnNewclient_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewClient frmclientenuevo = new frmnewClient();
            frmclientenuevo.ShowDialog();
        }    

     
        
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_ventas c = new f_ventas();
            c.empleado = codEmpleado;
            c.sucursal = codSucursal;
            c.venta = false;
            c.MdiParent = this;
            c.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_NotasEntrada n = new f_NotasEntrada();
            n.sucursal = codSucursal;
            n.empleado = codEmpleado;
            n.MdiParent = this;
            n.Show();
        }

        private void btnClientes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmClientes miformCliente = new frmClientes();
            miformCliente.MdiParent = this;
            miformCliente.Show();
        }

        private void btnProveedores_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProviders miFormProveedor = new frmProviders();
            miFormProveedor.MdiParent = this;
            miFormProveedor.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_Notas nota = new f_Notas();
            nota.empleado = codEmpleado;
            nota.sucursal = codSucursal;
            nota.entrada = false;
            nota.MdiParent = this;
            nota.Show();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_EditUser u = new f_EditUser();
            u.MdiParent = this;
            u.Show();
        }
        
        private void btnnewsucursal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewsucursal a = new frmnewsucursal();
            a.MdiParent = this;
            a.Show();
        }

        private void btnversucursal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmversucursal a = new frmversucursal();
            a.MdiParent = this;
            a.Show();
        }

        private void btnupdatesucursal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatesucursal a = new frmupdatesucursal();
            a.MdiParent = this;
            a.Show();
        }

        private void btndelsucursal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdelsucursal a = new frmdelsucursal();
            a.MdiParent = this;
            a.Show();
        }

        private void btnnewproducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewproducto a = new frmnewproducto();
            a.MdiParent = this;
            a.Show();
        }

        private void btnverproducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmverproducto a = new frmverproducto();
            a.MdiParent = this;
            a.Show();
        }

        private void btnupdateproducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdateproducto a = new frmupdateproducto();
            a.MdiParent = this;
            a.Show();
        }

        private void btndelproducto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdelproducto a = new frmdelproducto();
            a.MdiParent = this;
            a.Show();
        }

        private void btnnewcate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewcategoria a = new frmnewcategoria();
            a.MdiParent = this;
            a.Show();
        }

        private void btnvercategoria_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmvercategoria a = new frmvercategoria();
            a.MdiParent = this;
            a.Show();
        }

        private void bar(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatecategoria a = new frmupdatecategoria();
            a.MdiParent = this;
            a.Show();
        }

        private void btndelcategoria_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdelcategoria a = new frmdelcategoria();
            a.MdiParent = this;
            a.Show();
        }

        private void btnnewmarca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmnewmarca a = new frmnewmarca();
            a.MdiParent = this;
            a.Show();
        }

        private void btnvermarca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmvermarca a = new frmvermarca();
            a.MdiParent = this;
            a.Show();
        }

        private void btnupdatemarca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatemarca a = new frmupdatemarca();
            a.MdiParent = this;
            a.Show();
        }

        private void btndelmarca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdelmarca a = new frmdelmarca();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_deleteUser delUser = new f_deleteUser();
            delUser.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_Usuarios newuser = new f_Usuarios();
            newuser.MdiParent = this;
            newuser.Show();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_ListUsers luser = new f_ListUsers();
            luser.MdiParent = this;
            luser.Show();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnupdatesucursal_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatesucursal a = new frmupdatesucursal();
            a.MdiParent = this;
            a.Show();
        }

        private void btnupdatecate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatecategoria a = new frmupdatecategoria();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            agregarpresentacion a = new agregarpresentacion();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmverpresentacion a = new frmverpresentacion();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmupdatepresentacion a = new frmupdatepresentacion();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmdelpresentacion a = new frmdelpresentacion();
            a.MdiParent = this;
            a.Show();
        }

        private void btnSalir_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            anterior.anterior = this;
            anterior.bandera = true;
            anterior.recargar();
            anterior.Show();
        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_abonos aC = new f_abonos();
            aC.typeAbono = false;
            aC.MdiParent = this;
            aC.Show();
        }

        private void btnAddAbono_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_abonos aV = new f_abonos();
            aV.MdiParent = this;
            aV.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAsignProducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAsignProducts miAsignacion = new frmAsignProducts();
            miAsignacion.Show();
        }

        private void btnReportSales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVentascs miReporte = new frmVentascs();
            miReporte.Show(); 
            
        }

        private void btnTop10Sales_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            frmPrintReport miReporte = new frmPrintReport();
            miReporte.PrintReportTop10();
            miReporte.Show();
        }

        private void btnAgregarPresentacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAsignarPrecio a = new frmAsignarPrecio();
            a.MdiParent = this;
            a.Show();
        }

        private void btnVerPrecios_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVerPrecioAsignado a = new frmVerPrecioAsignado();
            a.MdiParent = this;
            a.Show();
        }

        private void btnmodificarAsignacionPrecio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUpdateAsignacion a = new frmUpdateAsignacion();
            a.MdiParent = this;
            a.Show();
        }

        private void btneliminarasignacion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDelAsignacionprecio a = new frmDelAsignacionprecio();
            a.MdiParent = this;
            a.Show();
        }

        private void btnAsignarRoles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAsignarPermisos a = new frmAsignarPermisos();
            a.MdiParent = this;
            a.Show();
        }

        private void barButtonItem5_ItemClick_3(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmModificarAsignacion a = new frmModificarAsignacion();
            a.MdiParent = this;
            a.Show();
        }

        private void btnAgregarRoles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmagregarroles a = new frmagregarroles();
            a.MdiParent = this;
            a.Show();
        }

        private void btnVerRoles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVerAsignacionPermisos a = new frmVerAsignacionPermisos();
            a.MdiParent = this;
            a.Show();
        }

        private void btnDelRoles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDelAsignacionRoles a = new frmDelAsignacionRoles();
            a.MdiParent = this;
            a.Show();
        }

        private void btnNewCaja_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int idsucursal = 1; //reemplazar con el id de sucursal segun login para enviar a sucursal correcta
            try
            {
                //Generamos la nueva caja en la base
                string query = "CALL SP_NEWCAJA({0})";
                query = string.Format(query, idsucursal);
                da.executeCommand(query);

                //obtenemos su ID para utilizarlo y enviarlo a los diferentes forms donde hay ingreso o egreso de dinero.
                query = "SELECT FN_GETLASTCAJA({0})";
                query = string.Format(query, idsucursal);
                idCaja = Convert.ToInt16( da.executeScalar(query));
                //si se ejecuto todo correcamente se activan los botones de ventas y compras abonos de compras y ventas.
                //Adicional habilitamos el boton de cerrar caja.
                btnNewVenta.Enabled = true;
                btnCloseCaja.Enabled = true;
                btnNewCompra.Enabled = true;
                btnAbonoC.Enabled = true;
                btnAbonoV.Enabled = true;
                btnNewCaja.Enabled = false;

                MessageBox.Show("Caja aperturada correctamente.","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
                }
            catch (Exception ex)
            {
                MessageBox.Show("Oops.! ocurrio un error durante la creacion de la caja \n Detalles: "+ex.Message,"Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                
            }
            
            
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            anterior.Hide();
            nombreEmpleado.Caption = empleado;
            nombreSucursal.Caption = sucursal;
            cargar_Permisos();
        }
        private void cargar_Permisos()
        {
            DataTable permisos = new DataTable();
            string query;
            query = "SELECT * FROM tblPermiso where id_empleado={0}";
            query = string.Format(query, codEmpleado);
            permisos = da.fillDataTable(query);

            foreach (object item in ribbonControl1.Pages)
            {
                if (item is DevExpress.XtraBars.Ribbon.RibbonPage)
                {
                    DevExpress.XtraBars.Ribbon.RibbonPage p = item as DevExpress.XtraBars.Ribbon.RibbonPage;

                    //RECORRER GROUPS DEL RIBON

                    foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup grupo in p.Groups)
                    {
                        grupo.Visible = false;
                    }

                    foreach (DevExpress.XtraBars.Ribbon.RibbonPageGroup grupo in p.Groups)
                    {
                        //MessageBox.Show(grupo.Text);
                        foreach (DataRow r in permisos.Rows)
                        {
                            if (r["id_rol"].ToString() == grupo.Tag.ToString()) {
                                grupo.Visible = true;

                                if (Convert.ToInt16(grupo.Tag) <= 4)
                                {
                                    ribbonPageTranscts.Visible = true;
                                }
                                else if(Convert.ToInt16(grupo.Tag)<= 7)
                                {
                                    ribbonPageclientes.Visible = true;
                                } else if (Convert.ToInt16(grupo.Tag) <= 12)
                                {
                                    ribbonPageProductos.Visible = true;
                                }else if (Convert.ToInt16(grupo.Tag) <= 13)
                                {
                                    ribbonPageSucursales.Visible = true;
                                }else if (Convert.ToInt16(grupo.Tag) <= 14)
                                {
                                    ribbonPageReports.Visible = true;
                                }else if (Convert.ToInt16(grupo.Tag) <= 15)
                                {
                                    ribbonPageCaja.Visible = true;
                                }



                                break;
                            }
                        }
                    }
                }
            }
        }
        private void btnCloseCaja_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //cerramos la caja actual
            try
            {
                string query = "call SP_CLOSECAJA({0})";
                query = string.Format(query, idCaja);
                da.executeCommand(query);
                //Habilitamos nuevamente el boton de nueva caja y deshabilitamos los botones de abonos, compras y ventas
                //Adicionalmente deshabilitamos este boton para evitar un cierre de una caja que no exista.
                btnCloseCaja.Enabled = false;
                btnNewVenta.Enabled = false;
                btnNewCompra.Enabled = false;                
                btnAbonoC.Enabled = false;
                btnAbonoV.Enabled = false;
                btnNewCaja.Enabled = true;
                MessageBox.Show("Cierre de caja Correcto!","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //Dejamos todo igual si ocurre alguna baja de internet o no se puede cerrar la caja.
                MessageBox.Show("Error al intentar cerrar la caja. \n Detalles: " + ex.Message, "Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            
        }

        private void btnNewVenta_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Enviamos la caja actual al formulario de ventas para que la caja a afectar sea la misma y
            //evitar que se traslapen los saldos de las cajas en cierta sucursal.
            f_ventas miFormVenta = new f_ventas(idCaja); 
            miFormVenta.MdiParent = this;
            miFormVenta.venta = true;
            miFormVenta.Show();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnNewCompra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_ventas miFormCompras = new f_ventas(idCaja);
            miFormCompras.venta = false;
            miFormCompras.MdiParent = this;
            miFormCompras.Show();

        }

        private void btnAbonoC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_abonos miFrmAbonoProv = new f_abonos(idCaja);
            miFrmAbonoProv.typeAbono = false;
            miFrmAbonoProv.MdiParent = this;
            miFrmAbonoProv.Show();
        }

        private void btnAbonoV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_abonos miFrmAbonoClient = new f_abonos(idCaja);
            miFrmAbonoClient.typeAbono = true;
            miFrmAbonoClient.MdiParent = this;
            miFrmAbonoClient.Show();

        }

        private void btnTrasladosBod_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            f_traslados traslados = new f_traslados();
            traslados.MdiParent = this;
            traslados.Show();
        }
    }
}
