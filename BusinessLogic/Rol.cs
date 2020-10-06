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
            Id = decimal.ToInt32(rol.ID);
            Nombre = rol.NOMBRE;
        }
        public Rol(ROL_SISTEMA rol)
        {
            Id = decimal.ToInt32(rol.ID);
            Nombre = rol.NOMBRE;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
}
