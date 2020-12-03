using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogic
{
    public class MasFlujosReportStructure
    {
        public MasFlujosReportStructure(MAS_FLUJOS_CREADOS mas)
        {
            this.CantidadFlujos = decimal.ToInt32((decimal)mas.FLUJOS);
            this.NombreUsuario = mas.Nombre;
            this.Cargo = mas.Cargo;
            this.Empresa = mas.EMPRESA;
        }

        public int CantidadFlujos { get; set; }
        public string NombreUsuario { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }

        public static List<MasFlujosReportStructure> GetReporte()
        {
            Entities ent = new Entities();
            List<MasFlujosReportStructure> listaFinal = new List<MasFlujosReportStructure>();
            foreach (MAS_FLUJOS_CREADOS fl in ent.MAS_FLUJOS_CREADOS)
            {
                listaFinal.Add(new MasFlujosReportStructure(fl));
            }
            return listaFinal;
        }
    }
}
