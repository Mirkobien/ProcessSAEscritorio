using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    public class FlujoInstancia
    {
        public FlujoInstancia()
        {

        }

        public User Responsable { get; set; }
        public Flujo Flujo { get; set; }
    }
    public class EstadoFlujo : ObservableObject, IDataErrorInfo
    {
        public EstadoFlujo()
        {

        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Descripcion":
                        if (string.IsNullOrEmpty(Descripcion))
                            result = "La Descripción del Estado Flujo no puede estar vacio";
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

    }
    public class Flujo : IDataErrorInfo
    {

        public Flujo()
        {
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Nombre":
                        if (string.IsNullOrEmpty(Nombre))
                            result = "El Nombre del Flujo no puede estar vacio";
                        break;
                    case "Descripcion":
                        if (string.IsNullOrEmpty(Descripcion))
                            result = "La Descripción del Flujo no puede estar vacio";
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
        public string Nombre { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Descripcion { get; set; }
        public EstadoFlujo Estado { get; set; }
        public ObservableCollection<Tarea> Tareas { get; set; }
        public Cargo Cargo { get; set; }

    }
}
