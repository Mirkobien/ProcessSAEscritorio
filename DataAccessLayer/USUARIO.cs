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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.AUTH_USUARIO = new HashSet<AUTH_USUARIO>();
            this.FLUJO = new HashSet<FLUJO>();
            this.TAREA = new HashSet<TAREA>();
        }
    
        public string ID_USER { get; set; }
        public string RUT_USUARIO { get; set; }
        public string NOMBRE { get; set; }
        public string PATERNO { get; set; }
        public string MATERNO { get; set; }
        public decimal SEXO_ID { get; set; }
        public string CORREO { get; set; }
        public Nullable<decimal> TELEFONO { get; set; }
        public decimal AUTH_USUARIO_ID_AUTH { get; set; }
        public decimal DEPARTAMENTO_ID { get; set; }
        public decimal ROL_CLIENTE_ID { get; set; }
        public decimal ESTADO_USUARIO_ID { get; set; }
        public decimal EMPRESA_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTH_USUARIO> AUTH_USUARIO { get; set; }
        public virtual AUTH_USUARIO AUTH_USUARIO1 { get; set; }
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual EMPRESA EMPRESA { get; set; }
        public virtual ESTADO_USUARIO ESTADO_USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FLUJO> FLUJO { get; set; }
        public virtual ROL_CLIENTE ROL_CLIENTE { get; set; }
        public virtual SEXO SEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAREA> TAREA { get; set; }
    }
}