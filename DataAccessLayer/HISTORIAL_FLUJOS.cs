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
    
    public partial class HISTORIAL_FLUJOS
    {
        public decimal ID { get; set; }
        public string DESCRICINO_HISTORIAL_FLUJO { get; set; }
        public System.DateTime FECHA_HISTORIAL_FLUJO { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public decimal FLUJO_ID_FRUJO { get; set; }
    
        public virtual FLUJO FLUJO { get; set; }
    }
}
