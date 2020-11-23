using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class JerarquiaUsuario : ObservableObject
    {
        public JerarquiaUsuario()
        {

        }

        

        public int Id { get; set; }
        public int Nivel { get; set; }
    }

    class JerarquiaDepartamento : ObservableObject
    {
        public JerarquiaDepartamento()
        {

        }

        public int Id { get; set; }
        public int Nivel { get; set; }
        public List<Cargo> Departamentos { get; set; }
    }
}
