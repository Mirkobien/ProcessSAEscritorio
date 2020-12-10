using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class MasFlujosReportStructure
    {
        public MasFlujosReportStructure()
        {
        }

        public int CantidadFlujos { get; set; }
        public string NombreUsuario { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
    }
}
