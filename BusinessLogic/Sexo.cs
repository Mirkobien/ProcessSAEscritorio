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
            Id = decimal.ToInt32(sex.IDSEX);
            Descripcion = sex.DESCRIPCION;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        public static List<Sexo> GetAllSexo()
        {
            List<Sexo> lista = new List<Sexo>();
            Entities ent = new Entities();

            foreach(SEXO sex in ent.SEXO)
            {
                lista.Add(new Sexo(sex));
            }

            return lista;
        }
    }
}
