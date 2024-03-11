using sistema_ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscritorioEC04
{
    public partial class frmFacturacion : Form
    {
        Extension ext = new Extension();//OBJETO GENERAL DE LA CLAS EXTENSION

        List<clasedetalle> infor = new List<clasedetalle>();

        public frmFacturacion()
        {
            InitializeComponent();
            
        }
        #region  CARGA DE DATOS Combobox

        public List<combocliente> cargarcombocliente()
        {
            try
            {
                using(BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from c in bd.Cliente
                               orderby c.Nomc
                               select new combocliente
                               {
                                   idcli =c.Codc,
                                   combcli = c.Nomc,
                               }).ToList();
                    return sql;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<combovendedor> cargar_combo_vendedor()
        {
            try
            {
                using(BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from v in bd.Vendedor
                               orderby v.Datv
                               select new combovendedor
                               {
                                   idven = v.Codv,
                                   vend = v.Datv,
                               }).ToList();
                    return sql;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<comboproducto> cargar_combo_producto()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from p in bd.Producto
                               orderby p.Nomp
                               select new comboproducto
                               {
                                   id=p.Codp,
                                   prod = p.Nomp,
                               }).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

       

        #region METODOS

        private void frmFacturacion_Load(object sender, EventArgs e)
        {
            cmbCliente.DataSource = cargarcombocliente();
            cmbCliente.DisplayMember = "combcli";
            cmbCliente.ValueMember = "idcli";
            cmbVendedor.DataSource = cargar_combo_vendedor();
            cmbVendedor.DisplayMember = "vend";
            cmbVendedor.ValueMember = "idven";
            cmbProducto.DataSource = cargar_combo_producto();
            cmbProducto.DisplayMember = "prod";
            cmbProducto.ValueMember = "id";
            txtNumFac.Text = ext.simularcodigo();
            datFecha.Value = DateTime.Now;
        }

        public void limpiarDatos()
        {
            infor.Clear();
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
                txtNumFac.Text = ext.simularcodigo();
                dgvFactura.DataSource = null;
                datFecha.Value = DateTime.Now;
            }
        }

        public List<datosdelDetalle> mostrarconsulta()
        {
            using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
            {
                var sql = (from d in bd.DetalleFactura
                           join prod in bd.Producto on d.Codp equals prod.Codp
                           where d.NumFac == txtNumFac.Text
                           select new datosdelDetalle
                           {
                               codigo = d.Codp,
                               Descripcion = prod.Nomp,
                               Preci_Unitario = d.PUnitario.ToString(),
                               Cantidad = d.Cantidad.ToString(),
                               Importe = d.Importe.ToString(),
                           }).ToList();
                return sql;
            }
        }

        #endregion

        #region EVENTOS COMBOBOX

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {  
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from p in bd.Producto
                               where p.Codp == cmbProducto.SelectedValue.ToString()
                               select new
                               {
                                   p.Punp
                               }).FirstOrDefault();

                    txtPUni.Text = sql.Punp.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from c in bd.Cliente
                               where c.Codc == cmbCliente.SelectedValue.ToString()
                               select new
                               {
                                   c.Dirc
                               }).FirstOrDefault();

                    txtDireccion.Text = sql.Dirc.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR DEL SISTEMA " + ex);
            }
        }
        #endregion

        

        #region BOTONES


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnGProducto_Click(object sender, EventArgs e)
        {
            
            clasedetalle DT = new clasedetalle(cmbProducto.SelectedValue.ToString(),cmbProducto.Text,float.Parse(txtPUni.Text),int.Parse(txtCantidad.Text),(float.Parse(txtCantidad.Text)*float.Parse(txtPUni.Text)));
            //infor es la lista de tipo detalle que esta al inicio de este archivo 
            infor.Add(DT);

            dgvFactura.DataSource=null;
            dgvFactura.DataSource= infor;

            double valor = 0;
            foreach(var suma in infor)
            {
                valor = valor + suma.Imp_d;

            }
            double igv = valor * 0.18;
            double tcancel = valor + igv;

            txtTBruto.Text = valor.ToString();
            txtIgv.Text = igv.ToString();
            txtTotal.Text = tcancel.ToString();
            txtCantidad.Text = "";

        }

        private void btnGestionar_Click(object sender, EventArgs e)
        {
            //registrar factura
            ext.validarvacios_factura(txtNumFac.Text, datFecha.Value, txtTBruto.Text,
                txtIgv.Text, txtTotal.Text, cmbCliente.SelectedValue.ToString(), cmbVendedor.SelectedValue.ToString());

            foreach(var dt in infor)
            {
                ext.registrarDetalle(txtNumFac.Text, dt.Cod_d, dt.Cant_d.ToString(), dt.Uni_p_d.ToString(), dt.Imp_d.ToString());
            }
            infor.Clear();
            dgvFactura.DataSource = null;
            txtNumFac.Text = ext.simularcodigo();
            limpiarDatos();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        { 
            try
            {

                infor.Clear();
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    
                    
                    Factura f = bd.Factura.Find(txtNumFac.Text);
                    if (f != null)
                    {
                        datFecha.Value = (DateTime)f.FechaFac;
                        txtTBruto.Text = f.TotalFac.ToString();
                        txtIgv.Text = f.IgvFac.ToString();
                        txtTotal.Text = f.TotalPago.ToString();
                        cmbCliente.SelectedValue = f.Codc;
                        cmbVendedor.SelectedValue = f.Codv;

                        dgvFactura.DataSource = null;
                        dgvFactura.DataSource = mostrarconsulta();
                        txtNumFac.Text = ext.simularcodigo();
                    }
                    else
                    {
                        MessageBox.Show("NO SE ENCONTRO REGISTROS CON EL NUMERO DE FACTURA INDICADO", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvFactura.DataSource = null;
                        txtNumFac.Text = ext.simularcodigo();
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            limpiarDatos();

        }

        #endregion

        #region DESECHABLE


        private void txtPUni_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbProducto_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
