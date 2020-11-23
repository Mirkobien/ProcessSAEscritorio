using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class Rol : ObservableObject
    {
        private int id;
        private string nombre;
        private Cargo departamento;

        public int Id { get => id; set { OnPropertyChanged("Id"); id = value; } }
        public string Nombre { get => nombre; set { OnPropertyChanged("Nombre"); nombre = value; } }
        public Cargo Departamento { get => departamento; set { OnPropertyChanged("Departamento"); departamento = value; } }
    }
}
