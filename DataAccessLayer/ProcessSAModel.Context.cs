﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AUTH_USER> AUTH_USER { get; set; }
        public virtual DbSet<DEPARTAMENTO> DEPARTAMENTO { get; set; }
        public virtual DbSet<DOCUMENTO> DOCUMENTO { get; set; }
        public virtual DbSet<EMPRESA> EMPRESA { get; set; }
        public virtual DbSet<ERROR_TAREA> ERROR_TAREA { get; set; }
        public virtual DbSet<ESTADO_TAREA> ESTADO_TAREA { get; set; }
        public virtual DbSet<ESTADO_USUARIO> ESTADO_USUARIO { get; set; }
        public virtual DbSet<FLUJO> FLUJO { get; set; }
        public virtual DbSet<JERARQUIA_DEP> JERARQUIA_DEP { get; set; }
        public virtual DbSet<JERARQUIA_USR> JERARQUIA_USR { get; set; }
        public virtual DbSet<ROL_CLIENTE> ROL_CLIENTE { get; set; }
        public virtual DbSet<ROL_SISTEMA> ROL_SISTEMA { get; set; }
        public virtual DbSet<SEXO> SEXO { get; set; }
        public virtual DbSet<TAREA> TAREA { get; set; }
        public virtual DbSet<USUARIO_CLIENTE> USUARIO_CLIENTE { get; set; }
        public virtual DbSet<USUARIO_SISTEMA> USUARIO_SISTEMA { get; set; }
        public virtual DbSet<ESTADO_FLUJO> ESTADO_FLUJO { get; set; }
    }
}
