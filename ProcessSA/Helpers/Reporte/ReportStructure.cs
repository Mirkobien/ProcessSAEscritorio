using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Helpers.Reporte
{
    class ReportStructure
    {
        public string NombreUsuario { get; set; }
        public int FlujosEjecutados { get; set; }
        public string NombreEmpresa { get; set; }
        public List<ReportStructure> GetReportStructures()
        {
            RESTClient.GetAllFlujoInstancias();
            return new List<ReportStructure>();
        }
    }
}
