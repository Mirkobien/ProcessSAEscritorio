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
    public class Rol
    {
        public Rol(ROL_CLIENTE rol)
        {
            Id = decimal.ToInt32(rol.IDROL);
            Nombre = rol.DESCRIPCION;
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

        public static List<Rol> GetAllRol()
        {
            Entities ent = new Entities();

            List<Rol> listaFinal = new List<Rol>();

            foreach (ROL_CLIENTE rol in ent.ROL_CLIENTE)
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
                DESCRIPCION = Nombre
            };
            if (Id != 0)
                rol.IDROL = Id;

            return rol;            
        }

        public static void Eliminar(int id)
        {
            Entities ent = new Entities();
            ROL_CLIENTE rol = ent.ROL_CLIENTE.Where(p => p.IDROL == id).FirstOrDefault();

            if (rol == null)
                return;

            ent.ROL_CLIENTE.Remove(rol);
            ent.SaveChanges();
        }
    }
}
