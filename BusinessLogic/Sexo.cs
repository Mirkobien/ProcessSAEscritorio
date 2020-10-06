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
    public class Sexo
    {
        public Sexo(SEXO sex)
        {
            Id = decimal.ToInt32(sex.ID);
            Descripcion = sex.DESCRIPCION;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }
}
