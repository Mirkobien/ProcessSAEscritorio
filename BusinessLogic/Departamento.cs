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

        public string Nombre { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<User> Usuarios { get; set; }

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

        public void Guardar(int jer)
        {
            DEPARTAMENTO dep = new DEPARTAMENTO();
            dep.DESCRIPCION = this.Descripcion;
            dep.NOMBRE = this.Nombre;
            dep.JERARQUIA_DEP_IDJER = jer;
            Entities ent = new Entities();

            ent.DEPARTAMENTO.Add(dep);
            ent.SaveChanges();
        }
    }
}
