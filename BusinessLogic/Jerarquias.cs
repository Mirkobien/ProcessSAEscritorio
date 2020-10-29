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
    public class JerarquiaUsuario
    {
        public JerarquiaUsuario(JERARQUIA_USR jer)
        {
            Id = decimal.ToInt32(jer.IDJER);
            Nivel = decimal.ToInt32(jer.NIVEL);
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Nivel { get; set; }
    }

    [DataContract]
    public class JerarquiaDepartamento
    {
        public JerarquiaDepartamento(JERARQUIA_DEP jer)
        {
            Id = decimal.ToInt32(jer.IDJER);
            Nivel = decimal.ToInt32(jer.NIVEL);
            Departamentos = new List<Departamento>();
            /*
             * DEBUG
             */

            List<DEPARTAMENTO> deps = jer.DEPARTAMENTO.ToList();

            int i = 0;


            foreach(DEPARTAMENTO dep in jer.DEPARTAMENTO)
            {
                Departamentos.Add(new Departamento(dep));
            }
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public List<Departamento> Departamentos { get; set; }
    }
}
