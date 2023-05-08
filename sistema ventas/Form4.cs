using sistema_ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscritorioEC04
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }
        void limpiar()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            txtNomP.Focus();
        }

        private string simularcodigov()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from p in bd.Producto select p.Codp).Max();
                    string primerasletras = sql.Substring(0, 4);
                    string ultima = sql.Substring(sql.Length - 1);
                    int numero = int.Parse(ultima) + 1;
                    string conver = numero.ToString();
                    string unir = primerasletras + conver;
                    return unir;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProgramarP> Visualizar()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from n in bd.Producto
                               orderby n.Codp
                               select new ProgramarP
                               {
                                   c = n.Codp,
                                   d = n.Nomp,
                                   p = n.Punp.ToString(),
                                   s = n.Stockp.ToString()
                               }).ToList();
                    return sql;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AgregarProducto(Producto pr)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    bd.Producto.Add(pr);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ModificarProducto(Producto pr)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from n in bd.Producto where n.Codp == pr.Codp select n;
                    foreach (Producto pro in sql)
                    {
                        pro.Codp = pr.Codp;
                        pro.Nomp = pr.Nomp;
                        pro.Punp = pr.Punp;
                        pro.Stockp = pr.Stockp;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void EliminarProducto(Producto pr)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from n in bd.Producto where n.Codp == pr.Codp select n;
                    foreach (Producto nn in sql)
                    {
                        bd.Producto.Remove(nn);
                    }
                    bd.SaveChanges();
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            txtCodP.Text = simularcodigov();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            dgvProductos.DataSource = Visualizar();
            limpiar();
            txtCodP.Text = simularcodigov();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();
            p.Codp = txtCodP.Text;
            p.Nomp = txtNomP.Text;
            p.Punp = float.Parse(txtPunP.Text);
            p.Stockp = int.Parse(txtStockP.Text);
            AgregarProducto(p);
            MessageBox.Show("Registro Guardado");
            dgvProductos.DataSource = Visualizar();
            txtCodP.Text = simularcodigov();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
            {
                Producto p = bd.Producto.Find(txtCodP.Text);
                txtCodP.Text = p.Codp;
                txtNomP.Text = p.Nomp;
                txtPunP.Text = p.Punp.ToString();
                txtStockP.Text = p.Stockp.ToString();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();
            p.Codp = txtCodP.Text;
            p.Nomp = txtNomP.Text;
            p.Punp = float.Parse(txtPunP.Text);
            p.Stockp = int.Parse(txtStockP.Text);
            ModificarProducto(p);
            MessageBox.Show("Registro Modificado");
            dgvProductos.DataSource = Visualizar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodP.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Quiere eliminar el producto", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Producto p = new Producto();
                    p.Codp = txtCodP.Text;
                    EliminarProducto(p);
                    dgvProductos.DataSource = Visualizar();
                    limpiar();
                    MessageBox.Show("Producto eliminado");
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar el Producto a Eliminar");
                }

            }
        }
    
    }
}
