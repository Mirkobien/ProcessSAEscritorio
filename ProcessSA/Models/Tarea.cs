using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class EstadoTarea : ObservableObject
    {
        public EstadoTarea()
        {
            
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    class Tarea
    {
        public Tarea()
        {
            Responsables = new ObservableCollection<User>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Comienzo { get; set; }
        public DateTime Termino { get; set; }
        public EstadoTarea Estado { get; set; }
        public ObservableCollection<User> Responsables { get; set; }
    }
}
