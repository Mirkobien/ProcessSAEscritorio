using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class Empresa : ObservableObject
    {
        public Empresa()
        {

        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rubro { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public List<JerarquiaDepartamento> JerarquiaDepartamentos { get; set; }
    }
}
