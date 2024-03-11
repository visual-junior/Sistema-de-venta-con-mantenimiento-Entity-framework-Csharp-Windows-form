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
    public partial class frmSistema : Form
    {
        public frmSistema()
        {
            InitializeComponent();
        }

        private void salidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes c = new frmClientes();
            c.Show();
        }

        private void vendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVendedores v = new frmVendedores();
            v.Show();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducto p = new frmProducto();
            p.Show();
        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFacturacion f = new frmFacturacion();
            f.Show();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmClientes c = new frmClientes();
            c.Show();
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            frmVendedores v = new frmVendedores();
            v.Show();
        }

        private void btnProucto_Click(object sender, EventArgs e)
        {
            frmProducto p = new frmProducto();
            p.Show();
        }

        private void btnFacturacion_Click(object sender, EventArgs e)
        {
            frmFacturacion f = new frmFacturacion();
            f.Show();
        }
    }
}
