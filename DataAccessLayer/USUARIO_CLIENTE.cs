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
    
    public partial class USUARIO_CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO_CLIENTE()
        {
            this.AUTH_USER = new HashSet<AUTH_USER>();
            this.TAREA = new HashSet<TAREA>();
        }
    
        public decimal IDUSU { get; set; }
        public string RUT { get; set; }
        public string NOMBRE { get; set; }
        public string PATERNO { get; set; }
        public string MATERNO { get; set; }
        public string CORREO { get; set; }
        public Nullable<decimal> TELEFONO { get; set; }
        public Nullable<decimal> ESTADO_USUARIO_IDESU { get; set; }
        public Nullable<decimal> SEXO_IDSEX { get; set; }
        public Nullable<decimal> ROL_CLIENTE_IDROL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTH_USER> AUTH_USER { get; set; }
        public virtual ESTADO_USUARIO ESTADO_USUARIO { get; set; }
        public virtual ROL_CLIENTE ROL_CLIENTE { get; set; }
        public virtual SEXO SEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAREA> TAREA { get; set; }
    }
}
