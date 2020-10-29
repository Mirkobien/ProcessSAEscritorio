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
        }

        public string Nombre { get; set; }
        public int Id { get; set; }

        public static List<Departamento> GetDepartamentos(int idEmpresa)
        {
            Entities ent = new Entities();
            List<Departamento> departamentos = new List<Departamento>();
            foreach (JERARQUIA_DEP jer in ent.EMPRESA.Where(e => e.IDEMP == idEmpresa).FirstOrDefault().JERARQUIA_DEP)
            {
                foreach (DEPARTAMENTO dep in jer.DEPARTAMENTO)
                {
                    departamentos.Add(new Departamento(dep));
                }
            }
            return departamentos;
        }

        public static Departamento GetDepartamento(int id)
        {
            Entities ent = new Entities();
            DEPARTAMENTO dep = ent.DEPARTAMENTO.Where(d => d.IDDEP == id).FirstOrDefault();
            return new Departamento(dep);
        }
    }
}
