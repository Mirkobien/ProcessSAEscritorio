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
    
    public partial class TAREA_INSTANCIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAREA_INSTANCIA()
        {
            this.ERROR_TAREA = new HashSet<ERROR_TAREA>();
        }
    
        public decimal ID { get; set; }
        public System.DateTime INICIO { get; set; }
        public System.DateTime FIN { get; set; }
        public decimal RESPONSABLE { get; set; }
        public decimal PLANTILLA { get; set; }
        public decimal ESTADO { get; set; }
        public decimal PROGRESO { get; set; }
        public decimal FLUJO { get; set; }
    
        public virtual ESTADO_TAREA ESTADO_TAREA { get; set; }
        public virtual FLUJO_INSTANCIA FLUJO_INSTANCIA { get; set; }
        public virtual TAREA TAREA { get; set; }
        public virtual USUARIO_CLIENTE USUARIO_CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERROR_TAREA> ERROR_TAREA { get; set; }
    }
}
