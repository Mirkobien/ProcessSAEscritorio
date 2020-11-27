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
    public class Tarea
    {
        public Tarea(TAREA tarea)
        {
            Id = decimal.ToInt32(tarea.IDTAR);
            Descripcion = tarea.DESCRIPCION;
            Comienzo = tarea.INICIO;
            Termino = tarea.FIN;
            Duracion = decimal.ToInt32(tarea.DURACION);
            Responsables = new List<User>();
            Cargo = new Cargo(tarea.CARGOS);
            foreach(USUARIO_CLIENTE usr in tarea.USUARIO_CLIENTE)
            {
                Responsables.Add(new User(usr));
            }

            Estado = new EstadoTarea(tarea.ESTADO_TAREA);
        }

        public Tarea()
        {

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Duracion { get; set; }
        [DataMember]
        public DateTime Comienzo { get; set; }
        [DataMember]
        public DateTime Termino { get; set; }
        [DataMember]
        public List<User> Responsables { get; set; }
        [DataMember]
        public EstadoTarea Estado { get; set; }
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

        public void CambiarEstado(EstadoTarea est)
        {
            Entities ent = new Entities();

            ent.TAREA.Where(p => p.IDTAR == Id).FirstOrDefault().ESTADO_TAREA_IDEST = est.Id;
            ent.SaveChanges();
        }

        public void CambiarEstado(int est)
        {
            Entities ent = new Entities();

            ent.TAREA.Where(p => p.IDTAR == Id).FirstOrDefault().ESTADO_TAREA_IDEST = est;
            ent.SaveChanges();
        }

        internal TAREA GetTAREA(Entities ent)
        {
            if (this.Comienzo.Year < 1990)
                this.Comienzo = DateTime.Today;
            if (this.Termino.Year < 1990)
                this.Termino = DateTime.Today;

            TAREA tareaExistente = ent.TAREA.Where(t => this.Id == t.IDTAR).FirstOrDefault();
            if (tareaExistente != null)
            {
                return tareaExistente;
            }
            TAREA tar = ToTAREA();
            tar.USUARIO_CLIENTE = (from res in Responsables join usu in ent.USUARIO_CLIENTE on res.Id equals usu.IDUSU select usu).ToList();
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

            if (this.Comienzo.Year < 1990)
                this.Comienzo = DateTime.Today;
            if (this.Termino.Year < 1990)
                this.Termino = DateTime.Today;

            Entities ent = new Entities();
            TAREA tar = ToTAREA();
            tar.USUARIO_CLIENTE = (from res in Responsables join usu in ent.USUARIO_CLIENTE on res.Id equals usu.IDUSU select usu).ToList();

            ent.TAREA.Add(ToTAREA());
            ent.SaveChanges();
        }
        public void Guardar(FLUJO flujo)
        {

            if (this.Comienzo.Year < 1990)
                this.Comienzo = DateTime.Today;
            if (this.Termino.Year < 1990)
                this.Termino = DateTime.Today;

            Entities ent = new Entities();
            TAREA tar = ToTAREA();
            tar.USUARIO_CLIENTE = (from res in Responsables join usu in ent.USUARIO_CLIENTE on res.Id equals usu.IDUSU select usu).ToList();

            flujo.TAREA.Add(ToTAREA());
        }

        private TAREA ToTAREA()
        {
            TAREA tarea = new TAREA
            {
                IDTAR = Id,
                INICIO = Comienzo,
                FIN = Termino,
                DESCRIPCION = Descripcion,
                ESTADO_TAREA_IDEST = Estado.Id,
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
