//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class FLUJO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FLUJO()
        {
            this.TAREA = new HashSet<TAREA>();
        }
    
        public decimal IDFLU { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal DIAS { get; set; }
        public System.DateTime INICIO { get; set; }
        public System.DateTime FIN { get; set; }
        public Nullable<decimal> ESTADO_FLUJO_IDESF { get; set; }
        public Nullable<decimal> ROL_CLIENTE_IDROL { get; set; }
    
        public virtual ESTADO_FLUJO ESTADO_FLUJO { get; set; }
        public virtual ROL_CLIENTE ROL_CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAREA> TAREA { get; set; }
    }
}
