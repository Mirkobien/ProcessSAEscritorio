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
    
    public partial class CARGOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARGOS()
        {
            this.CARGOS1 = new HashSet<CARGOS>();
            this.FLUJO = new HashSet<FLUJO>();
            this.TAREA = new HashSet<TAREA>();
            this.USUARIO_CLIENTE = new HashSet<USUARIO_CLIENTE>();
        }
    
        public decimal IDDEP { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<decimal> EMPRESA_IDEMP { get; set; }
        public Nullable<decimal> CARGOS_IDDEP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARGOS> CARGOS1 { get; set; }
        public virtual CARGOS CARGOS2 { get; set; }
        public virtual EMPRESA EMPRESA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FLUJO> FLUJO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAREA> TAREA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_CLIENTE> USUARIO_CLIENTE { get; set; }
    }
}
