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
            Id = (int)emp.ID;
            Nombre = emp.NOMBRE;
            Correo = emp.CORREO;
            Direccion = emp.DIRECCION;
            Rubro = emp.RUBRO;

            Empleados = new List<User>();
            foreach(USUARIO user in emp.USUARIO)
            {
                Empleados.Add(new User(user));
            }
        }

        public Empresa()
        {

        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Rubro { get; set; }
        public List<User> Empleados { get; set; }

        #region database operations
        public static bool Eliminar(int id)
        {
            Entities ent = new Entities();
            EMPRESA emp = ent.EMPRESA.Where(x => x.ID == id).FirstOrDefault();

            if (emp == null)
                return false;

            ent.EMPRESA.Remove(emp);
            return true;
        }

        public bool Modificar()
        {
            Entities ent = new Entities();
            EMPRESA emp = ent.EMPRESA.Where(x => x.ID == Id).FirstOrDefault();

            if (emp == null)
                return false;

            emp.NOMBRE = Nombre;
            ent.SaveChanges();
            return true;
        }

        public bool Guardar()
        {
            Entities ent = new Entities();
            EMPRESA emp = new EMPRESA
            {
                ID = Id,
                NOMBRE = Nombre,
                CORREO = Correo,
                DIRECCION = Direccion,
                RUBRO = Rubro
            };

            ent.EMPRESA.Add(emp);
            ent.SaveChanges();
            return true;
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

        #endregion
    }
}
