using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class Empresa : ObservableObject, IDataErrorInfo
    {
        public Empresa()
        {

        }

        public string this[string columnName] { 
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty(Nombre))
                            result = "No puede estar vacío.";
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Rubro { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public List<JerarquiaDepartamento> JerarquiaDepartamentos { get; set; }

        public string Error
        {
            get
            {
                return "Errorsito";
            }
        }
    }
}
