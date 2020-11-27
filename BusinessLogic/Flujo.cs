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

        public static List<EstadoFlujo> GetAllEstadoFlujos()
        {
            Entities ent = new Entities();
            List<EstadoFlujo> listaFinal = new List<EstadoFlujo>();
            foreach(ESTADO_FLUJO estado in ent.ESTADO_FLUJO)
            {
                listaFinal.Add(new EstadoFlujo(estado));
            }
            return listaFinal;
        }
    }

    [DataContract]
    public class Flujo
    {
        public Flujo(FLUJO flujo)
        {
            Id = decimal.ToInt32(flujo.IDFLU);
            Nombre = flujo.NOMBRE;
            Descripcion = flujo.DESCRIPCION;
            Inicio = flujo.INICIO;
            Fin = flujo.FIN;
            Dias = decimal.ToInt32(flujo.DIAS);
            Estado = new EstadoFlujo(flujo.ESTADO_FLUJO);
            Cargo = new Cargo(flujo.CARGOS);
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public DateTime Inicio { get; set; }
        [DataMember]
        public DateTime Fin { get; set; }
        [DataMember]
        public int Dias { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public EstadoFlujo Estado { get; set; }
        [DataMember]
        public List<Tarea> Tareas { get; set; }
        [DataMember]
        public Cargo Cargo { get; set; }

        public static List<Flujo> GetAllFlujos(int id)
        {
            Entities ent = new Entities();
            
            List<Flujo> listaFinal = new List<Flujo>();
            foreach(FLUJO flujo in ent.FLUJO.Where(f=> f.CARGOS.EMPRESA_IDEMP == id))
            {
                listaFinal.Add(new Flujo(flujo));
            }
            return listaFinal;
        }

        public static void Eliminar(int id)
        {
            Entities ent = new Entities();
            FLUJO flujo = ent.FLUJO.Where(p => p.IDFLU == id).FirstOrDefault();
            foreach(TAREA tarea in flujo.TAREA.ToList())
            {
                flujo.TAREA.Remove(tarea);
            }
            ent.FLUJO.Remove(flujo);
            ent.SaveChanges();
        }

        public void Guardar()
        {
            if (this.Inicio.Year < 1990)
                this.Inicio = DateTime.Today;
            if (this.Fin.Year < 1990)
                this.Fin = DateTime.Today;

            Entities ent = new Entities();
            FLUJO flujo = ToFLUJO(ent);
            FLUJO flujoExistente = ent.FLUJO.Where(f => f.IDFLU == flujo.IDFLU).FirstOrDefault();

            if (flujoExistente != null)
            {
                flujo.ESTADO_FLUJO_IDESF = flujoExistente.ESTADO_FLUJO_IDESF;
                flujo.NOMBRE = flujoExistente.NOMBRE;
                flujo.CARGOS = flujoExistente.CARGOS;
                flujo.TAREA.Clear();
                foreach(Tarea tar in Tareas)
                {
                    flujo.TAREA.Add(tar.GetTAREA(ent));
                }
                ent.SaveChanges();
                return;
            }

            foreach (Tarea tar in Tareas)
            {
                tar.Guardar(flujo);
            }
            ent.FLUJO.Add(flujo);
            ent.SaveChanges();
        }

        private FLUJO ToFLUJO(Entities ent)
        {
            FLUJO flujo = new FLUJO()
            {
                DESCRIPCION = Descripcion,
                INICIO = Inicio,
                FIN = Fin,
                NOMBRE = Nombre,
                CARGOS_IDDEP = Cargo.Id
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

        public static void CambiarEstado(int idFlujo, int idEstado)
        {
            Entities ent = new Entities();
            ent.FLUJO.Where(f => f.IDFLU == idFlujo).FirstOrDefault().ESTADO_FLUJO_IDESF = idEstado;
            ent.SaveChanges();
        }
    }
}
