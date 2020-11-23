using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class EstadoTarea : ObservableObject
    {
        public EstadoTarea()
        {
            
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    public class Tarea : IDataErrorInfo
    {
        public Tarea()
        {
            Responsables = new ObservableCollection<User>();
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty("Descripcion"))
                            result = "No puede estar vacío.";
                        break;
                    case "Rubro":
                        if (string.IsNullOrEmpty("Termino"))
                            result = "El Rubro no puede estar vacio";
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

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Comienzo { get; set; }
        public DateTime Termino { get; set; }
        public EstadoTarea Estado { get; set; }
        public ObservableCollection<User> Responsables { get; set; }
        public Cargo Cargo { get; set; }
    }
}
