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
    
    public partial class JERARQUIA_DEP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JERARQUIA_DEP()
        {
            this.DEPARTAMENTO = new HashSet<DEPARTAMENTO>();
        }
    
        public decimal IDJER { get; set; }
        public decimal NIVEL { get; set; }
        public Nullable<decimal> EMPRESA_IDEMP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEPARTAMENTO> DEPARTAMENTO { get; set; }
        public virtual EMPRESA EMPRESA { get; set; }
    }
}