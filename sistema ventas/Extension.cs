using sistema_ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscritorioEC04
{
    public class Extension
    {
        public void registrar_factura(Factura fac)
        {
            try
            {
                using(BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    bd.Factura.Add(fac);
                    bd.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void registrarDetalle(string numf, string codp, string cant, string precio_un, string importe)
        {
            try
            {
                using(BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    bd.SP_InsertDetalle(numf, codp, int.Parse(cant), float.Parse(precio_un), float.Parse(importe));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string simularcodigo()
        {
            try
            {
                using(BD_PROYECTO_FINALEntities bd = new BD_PROYECTO_FINALEntities())
                {
                    var sql = (from p in bd.Factura select p.NumFac).Max();
                    string primerasletras = sql.Substring(0, 4);//f000
                    string ultima = sql.Substring(sql.Length - 1);//3
                    int numero = int.Parse(ultima) + 1;
                    string conver = numero.ToString();
                    string unir = primerasletras + conver;//f0004
                    return unir;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void validarvacios_factura(string numf, DateTime fech,string totalf,string igv, string totalcancel, string codc,string codv)
        {
            if (numf=="" || totalf =="")
            {
                MessageBox.Show("Complete todos los campos","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                Factura factu = new Factura();
                factu.NumFac = numf;
                factu.FechaFac= fech;
                factu.TotalFac=float.Parse(totalf);
                factu.IgvFac=float.Parse(igv);
                factu.TotalPago = float.Parse(totalcancel);
                factu.Codc = codc;
                factu.Codv = codv;
                registrar_factura(factu);

                MessageBox.Show("FACTURA REGISTRADA CON EXITO", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        
    }

    public class combocliente
    {
        public string idcli { get; set; }
        public string combcli { get; set; }
    }
    public class combovendedor
    {
        public string idven { get; set; }
        public string vend { get; set; }
    }
    public class comboproducto
    {
        public string id { get; set; }
        public string prod { get; set; }
    }
    public class precioprod
    {
        public string descrip { get; set; }
        public string precio { get; set; }
    }


    class clasedetalle
    {
        private string cod_d;
        private string descrip_d;
        private float uni_p_d;
        private int cant_d;
        private float imp_d;


        public clasedetalle(string cod_d, string descrip_d, float uni_p_d, int cant_d, float imp_d)
        {
            this.Cod_d = cod_d;
            this.Descrip_d = descrip_d;
            this.Uni_p_d = uni_p_d;
            this.Cant_d = cant_d;
            this.Imp_d = imp_d;
        }

        public string Cod_d { get => cod_d; set => cod_d = value; }
        public string Descrip_d { get => descrip_d; set => descrip_d = value; }
        public float Uni_p_d { get => uni_p_d; set => uni_p_d = value; }
        public int Cant_d { get => cant_d; set => cant_d = value; }
        public float Imp_d { get => imp_d; set => imp_d = value; }
    }

    public class datosdelDetalle
    {
        public string codigo { get; set; }
        public string Descripcion { get; set; }
        public string Preci_Unitario { get; set; }
        public string Cantidad { get; set; }
        public string Importe { get; set; }
    }

    

}
