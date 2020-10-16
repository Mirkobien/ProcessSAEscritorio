using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public enum EstadoTarea
    {
        EN_ESPERA = 1,
        ACEPTADA = 2,
        TERMINADA = 3,
    }

    public class Tarea
    {
        public Tarea(TAREA tarea)
        {
            Id = decimal.ToInt32(tarea.ID_TAREA);
            Descripcion = tarea.DESCRIPCION;
            Comienzo = tarea.COMIENZO;
            Termino = tarea.TERMINO;
            Justificacion = tarea.JUSTIFICACION;
            if (tarea.USUARIO != null)
            {
                Responsable = new User(tarea.USUARIO);
            } else if (tarea.USUARIO_SISTEMA != null)
            {
                Responsable = new User(tarea.USUARIO_SISTEMA);
            }
            Estado = (EstadoTarea)tarea.ESTADOTAREA_ID;
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Comienzo { get; set; }
        public DateTime Termino { get; set; }
        public string Justificacion { get; set; }
        public User Responsable { get; set; }
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

        public void CambiarEstado(EstadoTarea est)
        {
            Entities ent = new Entities();

            ent.TAREA.Where(p => p.ID_TAREA == Id).FirstOrDefault().ESTADOTAREA_ID = (decimal)est;
            ent.SaveChanges();
        }

        public static Tarea GetTarea(int id)
        {
            Entities ent = new Entities();

            TAREA tar = ent.TAREA.Where(p => p.ID_TAREA == id).FirstOrDefault();
            return new Tarea(tar);
        }
    }
}
