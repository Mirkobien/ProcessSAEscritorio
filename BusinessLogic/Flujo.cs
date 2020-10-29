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
    public class EstadoFlujo
    {
        public EstadoFlujo(ESTADO_FLUJO estado)
        {
            Id = decimal.ToInt32(estado.IDESF);
            Descripcion = estado.DESCRIPCION;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }

    [DataContract]
    public class Flujo
    {
        public Flujo(FLUJO flujo)
        {
            Id = decimal.ToInt32(flujo.IDFLU);
            Nombre = flujo.NOMBRE;
            Descripcion = flujo.DESCRIPCION;
            Ejecucion = flujo.INICIO;
            Estado = new EstadoFlujo(flujo.ESTADO_FLUJO);
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public DateTime Ejecucion { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public EstadoFlujo Estado { get; set; }

        public static List<Flujo> GetAllFlujos()
        {
            Entities ent = new Entities();
            List<Flujo> listaFinal = new List<Flujo>();
            foreach(FLUJO flujo in ent.FLUJO)
            {
                listaFinal.Add(new Flujo(flujo));
            }
            return listaFinal;
        }

        public static void Eliminar(int id)
        {
            Entities ent = new Entities();
            ent.FLUJO.Remove(ent.FLUJO.Where(p => p.IDFLU == id).FirstOrDefault());
            ent.SaveChanges();
        }

        public void Guardar()
        {
            Entities ent = new Entities();
            ent.FLUJO.Add(ToFLUJO(ent));
            ent.SaveChanges();
        }

        private FLUJO ToFLUJO(Entities ent)
        {
            FLUJO flujo = new FLUJO()
            {
                DESCRIPCION = Descripcion,
                INICIO = Ejecucion,
                NOMBRE = Nombre
            };
            flujo.ESTADO_FLUJO = ent.ESTADO_FLUJO.Where(p => p.IDESF == Estado.Id).FirstOrDefault();

            return flujo;
        }

        public static void AddTareasToFlujo(List<Tarea> tareas, int id)
        {
            Entities ent = new Entities();
            List<TAREA> lista = (from tar in tareas
                                       join dbtar in ent.TAREA
                                       on tar.Id equals dbtar.IDTAR select dbtar).ToList();

            FLUJO flujo = ent.FLUJO.Where(f => f.IDFLU == id).FirstOrDefault();

            foreach (TAREA tar in lista)
            {
                if (!flujo.TAREA.Contains(tar))
                    flujo.TAREA.Add(tar);
            }

            ent.SaveChanges();
        }
    }
}
