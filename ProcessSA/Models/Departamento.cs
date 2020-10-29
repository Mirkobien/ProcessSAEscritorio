using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class Departamento
    {
        public Departamento()
        {
            Usuarios = new ObservableCollection<User>();
        }
        public string Nombre { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ObservableCollection<User> Usuarios { get; set; }
    }
}
