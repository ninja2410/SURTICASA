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
    public partial class f_abonos : DevExpress.XtraEditors.XtraForm
    {
        public bool typeAbono = true;
        int idCredit;
        decimal monto;
        DataAccess da = new DataAccess();
        int miCaja;
        // Abono de venta = true;
        // Abono de compra = false;

        public f_abonos()
        {
            InitializeComponent();
        }

        public f_abonos(int idCaja)
        {
            miCaja = idCaja;
            InitializeComponent();

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //textEdit2.Properties.Mask.EditMask = "N2";
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            try
            {
                btnPago.Enabled = true;
                textEdit2.Enabled = true;
                creditosGridControl.Enabled = false;

                if (typeAbono)
                {
                    idCredit = Convert.ToInt32(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "IDCREDITO").ToString());
                    string nombre = creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "CLIENTE").ToString();
                    string nit = creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "NIT").ToString();
                    monto = Convert.ToDecimal(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "MONTO"));
                    DateTime fecha = Convert.ToDateTime(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "FECHALIMITE"));
                    lblCreditoDetail.Text = "Cliente: "+nombre+"/nMonto: "+monto;
                }
                else
                {
                    idCredit = Convert.ToInt32(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "IDCREDITO").ToString());
                    string nombre = creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "PROVEEDOR").ToString();
                    string nit = creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "NIT").ToString();
                    monto = Convert.ToDecimal(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "MONTO").ToString());
                    DateTime fecha = Convert.ToDateTime(creditosGridView.GetRowCellValue(creditosGridView.FocusedRowHandle, "FECHALIMITE").ToString());
                    lblCreditoDetail.Text = "Cliente: " + nombre + " /n Monto: " + monto;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión. Más detalles: " + ex.Message);
            }
        }

        private void txtIdCredit_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void f_abonos_Load(object sender, EventArgs e)
        {
            textEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit2.Properties.Mask.EditMask = "N2";
            //txtIdCredit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //txtIdCredit.Properties.Mask.EditMask = "D";

            if (typeAbono)
            { 
                string consultarCreditos = "SELECT CR.id_creditos as IDCREDITO, CONCAT(CL.nombre, ' ', CL.apellido) as CLIENTE, CL.nit as NIT, CR.monto as MONTO ,CR.fecha_limite as FECHA_LIMITE FROM tblCreditos CR INNER JOIN tblCliente CL ON CR.deudor = CL.id_cliente";
                creditosGridControl.DataSource = da.fillDataTable(consultarCreditos);
                
            }
            else
            {
                string consultarCreditos = "SELECT CR.id_creditos AS IDCREDITO, PR.nombre_proveedor AS PROVEEDOR, ";
                consultarCreditos += " PR.nit AS NIT, CR.monto AS MONTO ,CR.fecha_limite AS FECHA_LIMITE";
                consultarCreditos += " FROM tblCreditos CR INNER JOIN tblProveedor PR";
                consultarCreditos += " ON CR.deudor = PR.id_proveedor";
                creditosGridControl.DataSource = da.fillDataTable(consultarCreditos);
            }
            btnPago.Enabled = false;
            textEdit2.Enabled = false;
        }

        private void btnPago_Click(object sender, EventArgs e)
        {
            try
            {
                string insertAbono, updateCredit;
                decimal abono = Convert.ToDecimal(textEdit2.Text);
                string dateTime = DateTime.Now.ToString();
                string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");

                if(abono<= monto)
                { 
                insertAbono = "INSERT INTO tblAbonos(id_creditos, monto, fecha_pago) VALUES ({0},{1},'{2}');";
                insertAbono = string.Format(insertAbono,idCredit,abono,createddate);
                da.executeCommand(insertAbono);

                updateCredit = "UPDATE tblCreditos SET  monto = (monto - {0}) where id_creditos = {1};";
                updateCredit = string.Format(updateCredit, abono, idCredit);
                da.executeCommand(updateCredit);

                //Insertando la cantidad a la caja
                if (typeAbono == true) //Si es abono de una venta al credito se suma a la caja.
                {
                    string query = "CALL SP_UPDATECAJA({0},{1})"; //Actualizamos la cantidad de caja
                    query = string.Format(query, miCaja, Convert.ToDecimal(textEdit2.Text));
                    da.executeCommand(query);
                }
                else if (typeAbono == false) // Si es una compra al credito se resta a la caja.
                {
                    string query = "CALL SP_UPDATECAJA({0},-{1})"; //Actualizamos la cantidad de caja
                    query = string.Format(query, miCaja, Convert.ToDecimal(textEdit2.Text));
                    da.executeCommand(query);
                }


                MessageBox.Show("Se ha registrado el abono con exito.");
                }
                else
                {
                    MessageBox.Show("El abono excede el total del credito.");
                    textEdit2.Text = "";
                    textEdit2.Focus();
                }

                if (typeAbono)
                {
                    string consultarCreditos = "SELECT CR.id_creditos as IDCREDITO, CONCAT(CL.nombre, ' ', CL.apellido) as CLIENTE, CL.nit as NIT, CR.monto as MONTO ,CR.fecha_limite as FECHA_LIMITE FROM tblCreditos CR INNER JOIN tblCliente CL ON CR.deudor = CL.id_cliente";
                    creditosGridControl.DataSource = da.fillDataTable(consultarCreditos);

                }
                else
                {
                    string consultarCreditos = "SELECT CR.id_creditos AS IDCREDITO, PR.nombre_proveedor AS PROVEEDOR, ";
                    consultarCreditos += " PR.nit AS NIT, CR.monto AS MONTO ,CR.fecha_limite AS FECHA_LIMITE";
                    consultarCreditos += " FROM tblCreditos CR INNER JOIN tblProveedor PR";
                    consultarCreditos += " ON CR.deudor = PR.id_proveedor";
                    creditosGridControl.DataSource = da.fillDataTable(consultarCreditos);
                }
                textEdit2.Text = "";
                btnPago.Enabled = false;
                textEdit2.Enabled = false;
                lblCreditoDetail.Text = "";
                creditosGridControl.Enabled = true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error en la conexión. Más detalles: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea cancelar?", "",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}