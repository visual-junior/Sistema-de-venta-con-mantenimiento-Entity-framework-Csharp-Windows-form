//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistema_ventas
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleFactura
    {
        public string NumFac { get; set; }
        public string Codp { get; set; }
        public Nullable<float> Cantidad { get; set; }
        public Nullable<float> PUnitario { get; set; }
        public Nullable<float> Importe { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
