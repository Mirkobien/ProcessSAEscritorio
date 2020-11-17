using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class EstadoFlujo : ObservableObject
    {
        public EstadoFlujo()
        {

        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

    }
    public class Flujo
    {
        public Flujo()
        {
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Descripcion { get; set; }
        public EstadoFlujo Estado { get; set; }
        public ObservableCollection<Tarea> Tareas { get; set; }
        public Rol Rol { get; set; }

    }
}
