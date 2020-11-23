using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Empresa
    {
        public Empresa(EMPRESA emp)
        {
            Id = (int)emp.IDEMP;
            Nombre = emp.NOMBRE;
            Correo = emp.CORREO;
            Rubro = emp.RUBRO;
            Rut = emp.RUT;

            Cargos = new List<Rol>();
        }

        public Empresa()
        {

        }

        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Rubro { get; set; }
        public int Telefono { get; set; }
        public byte[] Contrato { get; set; }
        public List<Rol> Cargos { get; set; }

        #region database operations
        public static bool Eliminar(int id)
        {
            Entities ent = new Entities();
            EMPRESA emp = ent.EMPRESA.Where(x => x.IDEMP == id).FirstOrDefault();

            if (emp == null)
                return false;

            ent.EMPRESA.Remove(emp);
            ent.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            Entities ent = new Entities();
            EMPRESA emp = ent.EMPRESA.Where(x => x.IDEMP == Id).FirstOrDefault();

            if (emp == null)
                return false;

            emp.NOMBRE = Nombre;
            ent.SaveChanges();
            return true;
        }

        public bool Guardar()
        {
            Entities ent = new Entities();

            EMPRESA existente = ent.EMPRESA.Where(e => e.IDEMP == this.Id).FirstOrDefault();

            if (existente != null)
            {
                existente.NOMBRE = this.Nombre;
                existente.CORREO = this.Correo;
                existente.RUBRO = this.Rubro;
                existente.TELEFONO = Telefono;
                existente.RUT = Rut;
                existente.CONTRATO = Contrato;
                ent.SaveChanges();
            } else
            {
                EMPRESA emp = new EMPRESA
                {
                    NOMBRE = Nombre,
                    CORREO = Correo,
                    RUBRO = Rubro,
                    TELEFONO = Telefono,
                    RUT = Rut,
                    CONTRATO = Contrato
                };

                ent.EMPRESA.Add(emp);
                ent.SaveChanges();
            }
            return true;
        }

        public static Empresa GetEmpresa(int v)
        {
            Entities ent = new Entities();

            EMPRESA emp = ent.EMPRESA.Where(e => e.IDEMP == v).FirstOrDefault();
            return new Empresa(emp);
        }

        public static List<Empresa> GetAllEmpresas()
        {
            List<Empresa> empresas = new List<Empresa>();
            Entities ent = new Entities();
            foreach (EMPRESA emp in ent.EMPRESA)
            {
                empresas.Add(new Empresa(emp));
            }

            return empresas;
        }

        public static Empresa GetEmpresaDeUsuario(int v)
        {
            Entities ent = new Entities();
            return new Empresa(ent.EMPRESA
                .Where(e => e.IDEMP == ent.USUARIO_CLIENTE.Where(u => u.IDUSU == v).FirstOrDefault().CARGOS.EMPRESA_IDEMP)
                .FirstOrDefault()
                );
        }

        #endregion
    }
}
