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
            Responsables = new List<User>();
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
        public DateTime Comienzo { get; set; }
        [DataMember]
        public DateTime Termino { get; set; }
        [DataMember]
        public List<User> Responsables { get; set; }
        [DataMember]
        public EstadoTarea Estado { get; set; }

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

        public static List<Tarea> GetAllTarea(int id)
        {
            List<Tarea> listaFinal = new List<Tarea>();

            Entities ent = new Entities();
            List<TAREA> lista = ent.TAREA.Where(p => p.USUARIO_CLIENTE
                .Any(u => u.JERARQUIA_USR.DEPARTAMENTO.JERARQUIA_DEP.EMPRESA.IDEMP == id)).ToList();

            foreach(TAREA tar in lista)
            {
                listaFinal.Add(new Tarea(tar));
            }

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

        public static Tarea GetTarea(int id)
        {
            Entities ent = new Entities();

            TAREA tar = ent.TAREA.Where(t => t.IDTAR == id).FirstOrDefault();
            return new Tarea(tar);
        }

        public void Guardar()
        {
            Entities ent = new Entities();

            ent.TAREA.Add(ToTAREA(ent));
            ent.SaveChanges();
        }

        private TAREA ToTAREA(Entities ent)
        {
            TAREA tarea = new TAREA
            {
                IDTAR = Id,
                INICIO = Comienzo,
                FIN = Termino,
                DESCRIPCION = Descripcion,
                ESTADO_TAREA_IDEST = Estado.Id,
                USUARIO_CLIENTE = (from res in Responsables join usu in ent.USUARIO_CLIENTE on res.Id equals usu.IDUSU select usu).ToList()
            };
            return tarea;
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
