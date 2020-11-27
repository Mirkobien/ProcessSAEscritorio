using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    [DataContract]
    public class Cargo
    {
        public Cargo(CARGOS cargo)
        {
            Id = decimal.ToInt32(cargo.IDDEP);
            Nombre = cargo.NOMBRE;
            Padre = cargo.CARGOS_IDDEP == null ? 0 : decimal.ToInt32((decimal)cargo.CARGOS_IDDEP);
        }

        public Cargo(CARGOS_JERARQUIA cargos)
        {
            Id = decimal.ToInt32(cargos.ID);
            Nombre = cargos.NOMBRE;
            Nivel = decimal.ToInt32(cargos.NIVEL == null ? 0 : (decimal)cargos.NIVEL);
            Padre = decimal.ToInt32(cargos.PADRE_ID == null ? 0 : (decimal)cargos.PADRE_ID);
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Padre { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public List<Cargo> Cargos { get; set; }

        public static List<Cargo> GetCargosDeEmpresa(int v)
        {
            Entities ent = new Entities();
            List<Cargo> listaFinal = new List<Cargo>();
            var a = ent.CARGOS_JERARQUIA.ToList();
            List<CARGOS_JERARQUIA> todosLosCargos = ent.CARGOS_JERARQUIA.Where(c => c.EMPRESA_ID == v).ToList();

            List<CARGOS_JERARQUIA> cargosRoot = todosLosCargos.Where(c => c.PADRE_ID == null).ToList();
            foreach(CARGOS_JERARQUIA cargoRoot in cargosRoot)
            {
                Cargo cargo = new Cargo(cargoRoot);
                cargo.Cargos = GetCargosDeCargo(todosLosCargos, cargo.Id);
                listaFinal.Add(cargo);
            }

            return listaFinal;
        }

        private static List<Cargo> GetCargosDeCargo(ICollection<CARGOS_JERARQUIA> todosLosCargos, int id)
        {
            List<Cargo> listaFinal = new List<Cargo>();
            foreach(CARGOS_JERARQUIA hijo in todosLosCargos.Where(t => t.PADRE_ID == id))
            {
                Cargo cargo = new Cargo(hijo);
                cargo.Cargos = GetCargosDeCargo(todosLosCargos, cargo.Id);
                listaFinal.Add(cargo);
            }
            return listaFinal;
        }

        public static List<Cargo> GetCargosDeFuncionario(int empresaId)
        {
            Entities ent = new Entities();
            IQueryable<CARGOS> carg = ent.CARGOS.Where(c => c.EMPRESA_IDEMP == empresaId);
            List<CARGOS> cargos = carg.Where(c => c.USUARIO_CLIENTE.Where(u => u.ROL_CLIENTE_IDROLC != 3).Count() < 1).ToList();
            List<Cargo> listaFinal = new List<Cargo>();
            foreach(CARGOS cargo in cargos)
            {
                listaFinal.Add(new Cargo(cargo));
            }
            return listaFinal;

        }

        public static Cargo GetCargo(int v)
        {
            return new Cargo(new Entities().CARGOS.Where(c => c.IDDEP == v).FirstOrDefault());
        }

        public void Guardar(int padre, int empresa)
        {
            Entities ent = new Entities();
            CARGOS c = new CARGOS();
            c.IDDEP = this.Id;
            c.NOMBRE = this.Nombre;
            c.EMPRESA_IDEMP = empresa;
            if (padre != 0)
            {
                c.CARGOS_IDDEP = padre;
            }
            ent.CARGOS.Add(c);
            ent.SaveChanges();
        }

        public static bool Eliminar(int v)
        {
            Entities ent = new Entities();
            CARGOS cargo = ent.CARGOS.Where(c => c.IDDEP == v).FirstOrDefault();
            if (cargo == null)
                return false;

            ent.CARGOS.Remove(cargo);
            ent.SaveChanges();
            return true;
        }

        public static List<Cargo> GetCargosDeEmpresaFlat(int v)
        {
            Entities ent = new Entities();
            List<Cargo> listaFinal = new List<Cargo>();
            List<CARGOS_JERARQUIA> cargos = ent.CARGOS_JERARQUIA.Where(c => c.EMPRESA_ID == v).ToList();
            foreach(CARGOS_JERARQUIA cargo in cargos)
            {
                listaFinal.Add(new Cargo(cargo));
            }
            return listaFinal;
        }
    }
    [DataContract]
    public class Rol
    {
        public Rol(ROL_CLIENTE rol)
        {
            Id = decimal.ToInt32(rol.IDROLC);
            Nombre = rol.NOMBRE;
        }
        public Rol(ROL_SISTEMA rol)
        {
            Id = decimal.ToInt32(rol.IDROS);
            Nombre = rol.DESCRIPCION;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        public static List<Rol> GetAllRol(int v)
        {
            Entities ent = new Entities();

            List<Rol> listaFinal = new List<Rol>();
            foreach(ROL_CLIENTE rol in ent.ROL_CLIENTE)
            {
                listaFinal.Add(new Rol(rol));
            }

            return listaFinal;
        }

        public void Guardar()
        {
            Entities ent = new Entities();
            ent.ROL_CLIENTE.Add(ToROL_CLIENTE());
            ent.SaveChanges();
        }

        private ROL_CLIENTE ToROL_CLIENTE()
        {

            ROL_CLIENTE rol = new ROL_CLIENTE
            {
                NOMBRE = Nombre
            };
            if (Id != 0)
                rol.IDROLC = Id;

            return rol;            
        }
        public static void Eliminar(int id)
        {
            Entities ent = new Entities();
            ROL_CLIENTE rol = ent.ROL_CLIENTE.Where(p => p.IDROLC == id).FirstOrDefault();

            if (rol == null)
                return;

            ent.ROL_CLIENTE.Remove(rol);
            ent.SaveChanges();
        }
    }
}
