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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace EscritorioEC04
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }
        public void Limpiar()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
        public List<Estructura_cliente> Visualizar()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from c in bd.Cliente
                               orderby c.Nomc
                               select new Estructura_cliente
                               {
                                   Codigo = c.Codc.ToString(),
                                   Nombre = c.Nomc,
                                   Direccion = c.Dirc
                               }).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void agregarCliente(Cliente cc)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    bd.Cliente.Add(cc);
                    bd.SaveChanges();
                }
                MessageBox.Show("Cliente registrado con exito", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("EL REGISTRO YA EXISTE", "ALERTA",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void ModificarCliente(Cliente cc)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from c in bd.Cliente where c.Codc == cc.Codc select c;
                    foreach (Cliente cli in sql)
                    {
                        cli.Codc = cc.Codc;
                        cli.Nomc = cc.Nomc;
                        cli.Dirc = cc.Dirc;
                    }
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EliminarCliente(Cliente cc)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from c in bd.Cliente where c.Codc == cc.Codc select c;
                    foreach (Cliente cli in sql)
                    {
                        bd.Cliente.Remove(cli);
                    }
                    bd.SaveChanges();
                }
                MessageBox.Show("Cliente Eliminado Satisfactoriamente");
            }
            catch (Exception)
            {
                MessageBox.Show("ESTE  CLIENTE NO SE PUEDE ELIMINAR DEBIDO A QUE ESTA RELACIONADO A LA SISTEMA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = Visualizar();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodC.Text == String.Empty || txtNomC.Text == String.Empty || txtDirC.Text == String.Empty)
            {
                MessageBox.Show("Ingrese valores en todos los campos", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Cliente c = new Cliente();
                c.Codc = txtCodC.Text;
                c.Nomc = txtNomC.Text;
                c.Dirc = txtDirC.Text;
                agregarCliente(c);
                dgvClientes.DataSource = Visualizar();
                Limpiar();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
            {
                Cliente t = bd.Cliente.Find(txtCodC.Text);
                txtCodC.Text = t.Codc;
                txtNomC.Text = t.Nomc;
                txtDirC.Text = t.Dirc;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodC.Text == String.Empty || txtNomC.Text == String.Empty || txtDirC.Text == String.Empty)
            {
                MessageBox.Show("Ingrese valores en todos los campos", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
            }
            else
            {
                Cliente c = new Cliente();
                c.Codc = txtCodC.Text;
                c.Nomc = txtNomC.Text;
                c.Dirc = txtDirC.Text;
                ModificarCliente(c);
                MessageBox.Show("Cliente Modificado con exito", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvClientes.DataSource = Visualizar();
                Limpiar();
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodC.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Estas seguro de Eliminar el Cliente?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Cliente c = new Cliente();
                    c.Codc = txtCodC.Text;
                    EliminarCliente(c);
                    dgvClientes.DataSource = Visualizar();
                    Limpiar();
                    
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar el Registro a Eliminar");
                }
            }
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvClientes.CurrentRow.Selected = true;
                txtCodC.Text = dgvClientes.Rows[e.RowIndex].Cells["Codigo"].FormattedValue.ToString();
                txtNomC.Text = dgvClientes.Rows[e.RowIndex].Cells["Nombre"].FormattedValue.ToString();
                txtDirC.Text = dgvClientes.Rows[e.RowIndex].Cells["Direccion"].FormattedValue.ToString();
              
            }
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }
    }
}
