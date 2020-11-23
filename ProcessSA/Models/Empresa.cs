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
                    case "Rut":
                        if (string.IsNullOrEmpty(Rut))
                            result = "El Rut de la Empresa no puede estar vacio.";
                        break;
                    case "Rubro":
                        if (string.IsNullOrEmpty(Rubro))
                            result = "El Rubro de la Empresa no puede estar vacio";
                        break;
                    case "Nombre":
                        if (string.IsNullOrEmpty(Nombre))
                            result = "El Nombre de la Empresa no puede estar vacio";
                        break;
                    case "Correo":
                        if (string.IsNullOrEmpty(Correo))
                            result = "El Correo de la Empresa no puede estar vacio";
                        break;
                    case "Direccion":
                        if (string.IsNullOrEmpty(Direccion))
                            result = "La Dirección de la Empresa no puede estar vacio";
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
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Rubro { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public List<JerarquiaDepartamento> JerarquiaDepartamentos { get; set; }
    }
}
