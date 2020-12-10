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
    public class FlujoInstancia
    {
        public FlujoInstancia(FLUJO_INSTANCIA flujo)
        {
            Id = decimal.ToInt32(flujo.ID);
            Inicio = flujo.INICIO;
            Fin = flujo.FIN;
            Plantilla = new Flujo(flujo.FLUJO);
            Estado = new EstadoFlujo(flujo.ESTADO_FLUJO);
            Responsable = new User(flujo.USUARIO_CLIENTE);
            Tareas = new List<TareaInstancia>();
            foreach(TAREA_INSTANCIA tarea in flujo.TAREA_INSTANCIA)
            {
                Tareas.Add(new TareaInstancia(tarea));
            }
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Inicio { get; set; }
        [DataMember]
        public DateTime Fin { get; set; }
        [DataMember]
        public Flujo Plantilla { get; set; }
        [DataMember]
        public EstadoFlujo Estado { get; set; }
        [DataMember]
        public User Responsable { get; set; }
        [DataMember]
        public List<TareaInstancia> Tareas { get; set; }

        public List<FlujoInstancia> GetAllFlujosDeCargo(int cargoId)
        {
            Entities ent = new Entities();
            List<FLUJO_INSTANCIA> flujos = ent.FLUJO_INSTANCIA.Where(f => f.FLUJO.CARGOS.IDDEP == cargoId).ToList();
            List<FlujoInstancia> listaFinal = new List<FlujoInstancia>();
            foreach(FLUJO_INSTANCIA flujo in flujos)
            {
                listaFinal.Add(new FlujoInstancia(flujo));
            }
            return listaFinal;
        }

        public static void CambiarEstado(int idFlujo, int idEstado)
        {
            Entities ent = new Entities();
            ent.FLUJO_INSTANCIA.Where(f => f.ID == idFlujo).FirstOrDefault().ESTADO_ID = idEstado;
            ent.SaveChanges();
        }

        public static List<FlujoInstancia> GetAllFromUser(int v)
        {
            Entities ent = new Entities();
            List<FLUJO_INSTANCIA> flujos = ent.FLUJO_INSTANCIA.Where(f => f.RESPONSABLE == v).ToList();
            List<FlujoInstancia> listaFinal = new List<FlujoInstancia>();
            foreach (FLUJO_INSTANCIA flujo in flujos)
            {
                listaFinal.Add(new FlujoInstancia(flujo));
            }
            return listaFinal;
        }

        public static List<FlujoInstancia> GetAll()
        {
            Entities ent = new Entities();
            List<FLUJO_INSTANCIA> flujos = ent.FLUJO_INSTANCIA.ToList();
            List<FlujoInstancia> listaFinal = new List<FlujoInstancia>();
            foreach (FLUJO_INSTANCIA flujo in flujos)
            {
                listaFinal.Add(new FlujoInstancia(flujo));
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
            Cargo = new Cargo(flujo.CARGOS);
            Tareas = new List<Tarea>();
            Instancias = flujo.FLUJO_INSTANCIA.Count();
            foreach(TAREA tar in flujo.TAREA)
            {
                Tareas.Add(new Tarea(tar));
            }
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<Tarea> Tareas { get; set; }
        [DataMember]
        public Cargo Cargo { get; set; }
        [DataMember]
        public int Instancias { get; set; }

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

            Entities ent = new Entities();
            FLUJO flujo = ToFLUJO();
            FLUJO flujoExistente = ent.FLUJO.Where(f => f.IDFLU == flujo.IDFLU).FirstOrDefault();

            if (flujoExistente != null)
            {
                flujo.NOMBRE = flujoExistente.NOMBRE;
                flujo.CARGOS = flujoExistente.CARGOS;
                foreach (TAREA tar in flujo.TAREA.Where(t => Tareas.All(ta => ta.Id != t.IDTAR)))
                {
                    flujo.TAREA.Remove(tar);
                }
                foreach (Tarea tar in Tareas)
                {
                    TAREA tarea = flujo.TAREA.Where(t => t.IDTAR == tar.Id).FirstOrDefault();
                    if (tarea != null)
                    {
                        tarea.DESCRIPCION = tar.Descripcion;
                        continue;
                    }
                    flujo.TAREA.Add(tar.GetTAREA(ent));
                }
                ent.SaveChanges();
                return;
            }

            foreach (Tarea tar in Tareas)
            {
                flujo.TAREA.Add(tar.GetTAREA(ent));
            }
            ent.FLUJO.Add(flujo);
            ent.SaveChanges();
        }

        private FLUJO ToFLUJO()
        {
            FLUJO flujo = new FLUJO()
            {
                DESCRIPCION = Descripcion,
                NOMBRE = Nombre,
                CARGOS_IDDEP = Cargo.Id
            };
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

        public static List<Flujo> GetAllFlujosDeCargo(int v)
        {
            Entities ent = new Entities();
            List<Flujo> flujos = new List<Flujo>();
            foreach(FLUJO flujo in ent.FLUJO.Where(f => f.CARGOS_IDDEP == v))
            {
                flujos.Add(new Flujo(flujo));
            }
            return flujos;
        }

        public static void Ejecutar(int idFlujo, int idResponsable, List<EjecutarTareaRequestData> tareaData)
        {
            Entities ent = new Entities();
            FLUJO flujo = ent.FLUJO.Where(f => f.IDFLU == idFlujo).FirstOrDefault();
            FLUJO_INSTANCIA flujoInstancia = new FLUJO_INSTANCIA();
            DateTime ultimaFecha = DateTime.Now;
            foreach(TAREA tarea in flujo.TAREA)
            {
                TAREA_INSTANCIA instancia = new TAREA_INSTANCIA();
                EjecutarTareaRequestData request = tareaData.Where(t => t.Id == tarea.IDTAR).FirstOrDefault();
                instancia.INICIO = DateTime.Parse(request.Inicio);
                instancia.FIN = DateTime.Parse(request.Fin);
                instancia.ESTADO = 1;
                instancia.RESPONSABLE = request.IdResponsable;
                instancia.PLANTILLA = tarea.IDTAR;
                flujoInstancia.TAREA_INSTANCIA.Add(instancia);

                if(instancia.FIN > ultimaFecha)
                {
                    ultimaFecha = instancia.FIN;
                }
            }
            flujoInstancia.PLANTILLA_ID = flujo.IDFLU;
            flujoInstancia.RESPONSABLE = idResponsable;
            flujoInstancia.ESTADO_ID = 1;
            flujoInstancia.INICIO = DateTime.Today;
            flujoInstancia.FIN = ultimaFecha;
            ent.FLUJO_INSTANCIA.Add(flujoInstancia);
            ent.SaveChanges();
        }
    }
}
