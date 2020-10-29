using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class JerarquiaUsuario
    {
        public JerarquiaUsuario()
        {

        }
        public int Id { get; set; }
        public int Nivel { get; set; }
    }

    class JerarquiaDepartamento
    {
        public JerarquiaDepartamento()
        {

        }
        public int Id { get; set; }
        public int Nivel { get; set; }
        public List<Departamento> Departamentos { get; set; }
    }
}
