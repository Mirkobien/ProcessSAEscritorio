using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Departamento
    {
        public Departamento()
        {

        }

        public Departamento(DEPARTAMENTO dep)
        {
            Nombre = dep.NOMBRE;
            Id = decimal.ToInt32(dep.IDDEP);
            Descripcion = dep.DESCRIPCION;
        }

        public Departamento(DEPARTAMENTOS_JERARQUIA dep, Entities ent)
        {
            Id = decimal.ToInt32(dep.ID);
            Nombre = dep.NOMBRE;
            Departamentos = new List<Departamento>();
            foreach(DEPARTAMENTOS_JERARQUIA depas in ent.DEPARTAMENTOS_JERARQUIA.Where(tab => tab.PADRE == Id)){
                Departamentos.Add(new Departamento(depas, ent));
            }
        }

        public string Nombre { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<User> Usuarios { get; set; }
        public List<Departamento> Departamentos { get; set; }
        public static List<Departamento> GetDepartamentos(int idEmpresa)
        {
            Entities ent = new Entities();
            List<Departamento> departamentos = new List<Departamento>();
            /*
            foreach (DEPARTAMENTO dep in ent.EMPRESA.Where(e => e.IDEMP == idEmpresa).FirstOrDefault().DEPARTAMENTO)
            {
                departamentos.Add(new Departamento(dep));
            }*/

            foreach(DEPARTAMENTOS_JERARQUIA dep in ent.DEPARTAMENTOS_JERARQUIA.Where(d => d.PADRE == null))
            {
                departamentos.Add(new Departamento(dep, ent));
            }
            return departamentos;
        }

        public static Departamento GetDepartamento(int id)
        {
            Entities ent = new Entities();
            DEPARTAMENTO dep = ent.DEPARTAMENTO.Where(d => d.IDDEP == id).FirstOrDefault();
            return new Departamento(dep);
        }

        public void Guardar(int padre, int empresa)
        {
            DEPARTAMENTO dep = new DEPARTAMENTO();
            dep.DESCRIPCION = this.Descripcion;
            dep.NOMBRE = this.Nombre;
            dep.DEPARTAMENTO_IDDEP = padre;
            dep.EMPRESA_IDEMP = empresa;

            Entities ent = new Entities();

            ent.DEPARTAMENTO.Add(dep);
            ent.SaveChanges();
        }

        public static List<Departamento> GetDepartamentosAsList(int v)
        {
            List<Departamento> lista = new List<Departamento>();
            Entities ent = new Entities();

            foreach(DEPARTAMENTO depa in ent.DEPARTAMENTO.Where(d => d.EMPRESA_IDEMP == v))
            {
                lista.Add(new Departamento(depa));
            }
            return lista;
        }
    }
}
