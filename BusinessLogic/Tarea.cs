using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    [DataContract]
    public class TareaError
    {
        public TareaError(ERROR_TAREA error)
        {
            Id = decimal.ToInt32(error.IDERR);
            Justificacion = error.JUSTIFICACION;
            Fecha = error.FECHA.ToString("yyyy-MM-dd");
            Tarea = new TareaInstancia(error.TAREA_INSTANCIA);
        }

        public static List<TareaError> GetTareaErrors(int idJefeFlujo)
        {
            Entities ent = new Entities();
            List<ERROR_TAREA> errores = ent.ERROR_TAREA.Where(e => e.TAREA_INSTANCIA.FLUJO_INSTANCIA.USUARIO_CLIENTE.IDUSU == idJefeFlujo).ToList();
            List<TareaError> listaFinal = new List<TareaError>();
            foreach(ERROR_TAREA error in errores)
            {
                listaFinal.Add(new TareaError(error));
            }
            return listaFinal;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Justificacion { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public TareaInstancia Tarea { get; set; }
    }
    public enum EstadoTareaEnum
    {
        POR_CONFIRMAR = 1,
        CONFIRMADO = 2,
        COMPLETADO = 3,
        ATRASADO = 4,
        RECHAZADA = 5
    }
    public class EjecutarTareaRequestData
    {
        public EjecutarTareaRequestData()
        {

        }
        public int Id { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }
        public int IdResponsable { get; set; }
    }
    [DataContract]
    public class EstadoTarea
    {
        public EstadoTarea(ESTADO_TAREA est)
        {
            Id = decimal.ToInt32(est.IDEST);
            Descripcion = est.DESCRIPCION;
        }

        public EstadoTarea()
        {

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        public static List<EstadoTarea> GetAllEstadoTareas()
        {
            Entities ent = new Entities();

            List<EstadoTarea> lista = new List<EstadoTarea>();

            foreach(ESTADO_TAREA est in ent.ESTADO_TAREA)
            {
                lista.Add(new EstadoTarea(est));
            }

            return lista;
        }
    }

    [DataContract]
    public class TareaInstancia
    {
        public TareaInstancia(TAREA_INSTANCIA tarea)
        {
            Id = decimal.ToInt32(tarea.ID);
            Inicio = tarea.INICIO;
            Fin = tarea.FIN;
            Plantilla = new Tarea(tarea.TAREA);
            Responsable = new User(tarea.USUARIO_CLIENTE);
            Estado = new EstadoTarea(tarea.ESTADO_TAREA);
            Progreso = decimal.ToInt32(tarea.PROGRESO);
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Inicio { get; set; }
        [DataMember]
        public DateTime Fin { get; set; }
        [DataMember]
        public int Progreso { get; set; }
        [DataMember]
        public Tarea Plantilla { get; set; }
        [DataMember]
        public User Responsable { get; set; }
        [DataMember]
        public EstadoTarea Estado { get; set; }

        public static List<TareaInstancia> GetAllTareasDeFlujo(int id)
        {
            Entities ent = new Entities();
            List<TAREA_INSTANCIA> tareas = ent.TAREA_INSTANCIA.Where(t => t.FLUJO == id).ToList();
            List<TareaInstancia> listaFinal = new List<TareaInstancia>();
            foreach(TAREA_INSTANCIA tarea in tareas)
            {
                listaFinal.Add(new TareaInstancia(tarea));
            }
            return listaFinal;
        }

        public void SetProgresoYGuardar(int progreso)
        {
            Entities ent = new Entities();
            ent.TAREA_INSTANCIA.Where(t => t.ID == Id).FirstOrDefault().PROGRESO = progreso;
            ent.SaveChanges();
        }
        public static void SetProgresoYGuardar(int id, int progreso)
        {
            Entities ent = new Entities();
            ent.TAREA_INSTANCIA.Where(t => t.ID == id).FirstOrDefault().PROGRESO = progreso;
            ent.SaveChanges();
        }

        public static void CambiarEstado(int id, int idEstado)
        {
            Entities ent = new Entities();
            ent.TAREA_INSTANCIA.Where(t => t.ID == id).FirstOrDefault().ESTADO = idEstado;
            ent.SaveChanges();
        }

        public static void RechazarTarea(int id, string Justificacion)
        {
            Entities ent = new Entities();
            ent.TAREA_INSTANCIA.Where(t => t.ID == id).FirstOrDefault().ESTADO = (int)EstadoTareaEnum.RECHAZADA;
            ERROR_TAREA error = new ERROR_TAREA();
            error.FECHA = DateTime.Now;
            error.JUSTIFICACION = Justificacion;
            error.TAREA_IDTAR = id;
            ent.ERROR_TAREA.Add(error);
            ent.SaveChanges();
        }

        public static void ReasignarTarea(int id, int responsable)
        {
            Entities ent = new Entities();
            TAREA_INSTANCIA tarea = ent.TAREA_INSTANCIA.Where(t => t.ID == id).FirstOrDefault();
            if (tarea.ESTADO != (int)EstadoTareaEnum.RECHAZADA)
                return;
            tarea.ESTADO = (int)EstadoTareaEnum.POR_CONFIRMAR;
            tarea.RESPONSABLE = responsable;
            ERROR_TAREA error = ent.ERROR_TAREA.Where(e => e.TAREA_IDTAR == id).FirstOrDefault();
            ent.ERROR_TAREA.Remove(error);
            ent.SaveChanges();
        }

        public void CambiarEstado(int idEstado)
        {
            Entities ent = new Entities();
            ent.TAREA_INSTANCIA.Where(t => t.ID == Id).FirstOrDefault().ESTADO = idEstado;
            ent.SaveChanges();
        }

        public static List<TareaInstancia> GetAllTareasDeUser(int v)
        {
            Entities ent = new Entities();
            List<TareaInstancia> listaFinal = new List<TareaInstancia>();
            foreach(TAREA_INSTANCIA tarea in ent.TAREA_INSTANCIA.Where(t => t.RESPONSABLE == v).ToList())
            {
                listaFinal.Add(new TareaInstancia(tarea));
            }
            return listaFinal;
        }

        public static List<TareaInstancia> GetAllTareasRechazadasDeUser(int v)
        {
            Entities ent = new Entities();
            List<TareaInstancia> listaFinal = new List<TareaInstancia>();
            foreach (TAREA_INSTANCIA tarea in ent.TAREA_INSTANCIA.Where(t => t.FLUJO_INSTANCIA.RESPONSABLE == v && t.ESTADO == (int)EstadoTareaEnum.RECHAZADA).ToList())
            {
                listaFinal.Add(new TareaInstancia(tarea));
            }
            return listaFinal;
        }

        public static void CambiarProgresoTarea(int idTarea, int progreso)
        {
            Entities ent = new Entities();
            ent.CAMBIAR_PROGRESO_TAREA(idTarea, progreso);
            ent.SaveChanges();
        }
    }
    [DataContract]
    public class Tarea
    {
        public Tarea(TAREA tarea)
        {
            Id = decimal.ToInt32(tarea.IDTAR);
            Descripcion = tarea.DESCRIPCION;
            Cargo = new Cargo(tarea.CARGOS);
        }

        public Tarea()
        {

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Cargo Cargo { get; set; }

        public static List<Tarea> GetAllTarea()
        {
            List<Tarea> listaFinal = new List<Tarea>();

            Entities ent = new Entities();
            List<TAREA> lista = ent.TAREA.ToList();
            foreach (TAREA tar in lista)
            {
                listaFinal.Add(new Tarea(tar));
            }

            return listaFinal;
        }

        public static List<Tarea> GetAllTarea(int idEmpresa)
        {
            List<Tarea> listaFinal = new List<Tarea>();

            return listaFinal;
        }

        internal TAREA GetTAREA(Entities ent)
        {

            TAREA tareaExistente = ent.TAREA.Where(t => this.Id == t.IDTAR).FirstOrDefault();
            if (tareaExistente != null)
            {
                return tareaExistente;
            }
            TAREA tar = ToTAREA();
            return tar;
        }

        public static Tarea GetTarea(int id)
        {
            Entities ent = new Entities();

            TAREA tar = ent.TAREA.Where(t => t.IDTAR == id).FirstOrDefault();
            return new Tarea(tar);
        }

        public void Guardar()
        {
            Entities ent = new Entities();
            TAREA tar = ToTAREA();

            ent.TAREA.Add(ToTAREA());
            ent.SaveChanges();
        }

        private TAREA ToTAREA()
        {
            TAREA tarea = new TAREA
            {
                IDTAR = Id,
                DESCRIPCION = Descripcion,
                CARGOS_IDDEP = Cargo.Id
            };
            return tarea;
        }

        public static List<Tarea> GetAllTareaCargo(int v)
        {
            Entities ent = new Entities();
            List<Tarea> tareas = new List<Tarea>();
            foreach(TAREA tar in ent.TAREA.Where(t => t.CARGOS_IDDEP == v))
            {
                tareas.Add(new Tarea(tar));
            }
            return tareas;
        }

        public void Eliminar()
        {
            Entities ent = new Entities();

            TAREA tar = ent.TAREA.Where(p => p.IDTAR == Id).FirstOrDefault();
            ent.TAREA.Remove(tar);
            ent.SaveChanges();
        }
    }
}
