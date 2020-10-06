using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class Rol : ObservableObject
    {
        private string id;
        private string nombre;

        public string Id { get => id; set { OnPropertyChanged("Id"); id = value; } }
        public string Nombre { get => nombre; set { OnPropertyChanged("Nombre"); nombre = value; } }
    }
}
