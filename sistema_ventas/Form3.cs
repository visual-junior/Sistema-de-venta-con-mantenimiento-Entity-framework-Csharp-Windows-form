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
    public partial class frmVendedores : Form
    {
        public frmVendedores()
        {
            InitializeComponent();
        }
        private void limpiar()
        {
            foreach (Control c in this.Controls)
            {
                if (c.TabIndex != 5)
                {
                    if (c is TextBox)
                    {
                        c.Text = "";
                    }
                }
            }
            cmbSexV.Text = "";
            txtDatV.Focus();
        }


        private string simularcodigov()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from p in bd.Vendedor select p.Codv).Max();
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



        public List<Estructura_vendedor> Visualizar()
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from n in bd.Vendedor
                               orderby n.Codv
                               select new Estructura_vendedor
                               {
                                   cod = n.Codv,
                                   dat = n.Datv,
                                   dir = n.Dirv,
                                   dni = n.Dniv,
                                   sex = n.Sexv,
                               }).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AgregarVendedor(Vendedor ve)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    bd.Vendedor.Add(ve);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModificarVendedor(Vendedor ve)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from v in bd.Vendedor where v.Codv == ve.Codv select v;
                    foreach (Vendedor ven in sql)
                    {
                        ven.Codv = ve.Codv;
                        ven.Datv = ve.Datv;
                        ven.Dirv = ve.Dirv;
                        ven.Dniv = ve.Dniv;
                        ven.Sexv = ve.Sexv;
                    }
                    bd.SaveChanges();
                }

            }

            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarVendedor(Vendedor ve)
        {
            try
            {
                using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = from v in bd.Vendedor where v.Codv == ve.Codv select v;
                    foreach (Vendedor vv in sql)
                    {
                        bd.Vendedor.Remove(vv);
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
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void frmVendedores_Load(object sender, EventArgs e)
        {
            dgvVendedores.DataSource = Visualizar();
            dgvVendedores.Columns[0].HeaderText = "CODIGO";
            dgvVendedores.Columns[1].HeaderText = "DATOS";
            dgvVendedores.Columns[2].HeaderText = "DIRECCION";
            dgvVendedores.Columns[3].HeaderText = "DNI";
            dgvVendedores.Columns[4].HeaderText = "SEXO";
            dgvVendedores.Columns[1].Width = 105;
            dgvVendedores.Columns[2].Width = 130;
            btnModificar.Enabled=false;
            btnEliminar.Enabled=false;
            txtCodV.Text = simularcodigov();
            txtDatV.Focus();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Vendedor v = new Vendedor();
            v.Codv = txtCodV.Text;
            v.Datv = txtDatV.Text;
            v.Dirv = txtDirV.Text;
            v.Dniv = txtDniV.Text;
            v.Sexv = cmbSexV.Text;
            AgregarVendedor(v);
            MessageBox.Show("Registro Guardado");
            dgvVendedores.DataSource = Visualizar();
            dgvVendedores.Columns[0].HeaderText = "CODIGO";
            dgvVendedores.Columns[1].HeaderText = "DATOS";
            dgvVendedores.Columns[2].HeaderText = "DIRECCION";
            dgvVendedores.Columns[3].HeaderText = "DNI";
            dgvVendedores.Columns[4].HeaderText = "SEXO";
            dgvVendedores.Columns[1].Width = 105;
            dgvVendedores.Columns[2].Width = 130;
            limpiar();
            txtCodV.Text = simularcodigov();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
            {
                Vendedor v = bd.Vendedor.Find(txtCodV.Text);
                txtDatV.Text = v.Datv;
                txtDirV.Text = v.Dirv;
                txtDniV.Text = v.Dniv;
                cmbSexV.SelectedText = v.Sexv;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Vendedor v = new Vendedor();
            v.Codv = txtCodV.Text;
            v.Datv = txtDatV.Text;
            v.Dirv = txtDirV.Text;
            v.Dniv = txtDniV.Text;
            v.Sexv = cmbSexV.Text;

            ModificarVendedor(v);
            MessageBox.Show("Registro modificado");
            dgvVendedores.DataSource = Visualizar();
            dgvVendedores.Columns[0].HeaderText = "CODIGO";
            dgvVendedores.Columns[1].HeaderText = "DATOS";
            dgvVendedores.Columns[2].HeaderText = "DIRECCION";
            dgvVendedores.Columns[3].HeaderText = "DNI";
            dgvVendedores.Columns[4].HeaderText = "SEXO";
            dgvVendedores.Columns[1].Width = 105;
            dgvVendedores.Columns[2].Width = 130;
            limpiar();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodV.Text.Trim().Length > 0)
            {
                DialogResult respuestaAdvertencia = DialogResult.OK;
                respuestaAdvertencia = MessageBox.Show("¿Estas seguro de eliminar el turno?", "Eliminar :(", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuestaAdvertencia == DialogResult.Yes)
                {
                    Vendedor v = new Vendedor();
                    v.Codv = txtCodV.Text;
                    EliminarVendedor(v);
                    dgvVendedores.DataSource = Visualizar();
                    dgvVendedores.Columns[0].HeaderText = "CODIGO";
                    dgvVendedores.Columns[1].HeaderText = "DATOS";
                    dgvVendedores.Columns[2].HeaderText = "DIRECCION";
                    dgvVendedores.Columns[3].HeaderText = "DNI";
                    dgvVendedores.Columns[4].HeaderText = "SEXO";
                    limpiar();
                    MessageBox.Show("Registro eliminado");
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Debe seleccionar el registro a eliminar");
                }
            }
        }

        private void dgvVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvVendedores.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvVendedores.CurrentRow.Selected = true;
                txtCodV.Text = dgvVendedores.Rows[e.RowIndex].Cells["cod"].FormattedValue.ToString();
                txtDatV.Text = dgvVendedores.Rows[e.RowIndex].Cells["dat"].FormattedValue.ToString();
                txtDirV.Text = dgvVendedores.Rows[e.RowIndex].Cells["dir"].FormattedValue.ToString();
                txtDniV.Text = dgvVendedores.Rows[e.RowIndex].Cells["dni"].FormattedValue.ToString();
                cmbSexV.SelectedText = dgvVendedores.Rows[e.RowIndex].Cells["sex"].FormattedValue.ToString();
            }
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }
    }
}
