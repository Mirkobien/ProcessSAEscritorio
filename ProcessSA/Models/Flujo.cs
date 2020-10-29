using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class EstadoFlujo
    {
        public EstadoFlujo()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

    }
    public class Flujo
    {
        public Flujo()
        {
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Ejecucion { get; set; }
        public string Descripcion { get; set; }
        public EstadoFlujo Estado { get; set; }

    }
}
