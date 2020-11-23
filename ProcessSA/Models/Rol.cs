using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class Rol : ObservableObject, IDataErrorInfo
    {
        private int id;
        private string nombre;
        private Cargo departamento;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty("Nombre"))
                            result = "El nombre del Rol no puede estar vacio.";
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        public string Error
        {
            get
            {
                return "Errorsito";
            }
        }

        public int Id { get => id; set { OnPropertyChanged("Id"); id = value; } }
        public string Nombre { get => nombre; set { OnPropertyChanged("Nombre"); nombre = value; } }
        public Cargo Departamento { get => departamento; set { OnPropertyChanged("Departamento"); departamento = value; } }
    }
}
