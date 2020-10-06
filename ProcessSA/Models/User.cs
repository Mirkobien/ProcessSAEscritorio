using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.ViewModels
{
    enum UserType
    {
        USUARIO_CLIENTE,
        USUARIO_SISTEMA
    }
    class User : ObservableObject
    {
        private string nombre;
        private string rut;
        private Rol rol;
        private string unidad;
        private string jerarquia;

        public string Rut { get => rut; set { rut = value; OnPropertyChanged("Rut"); } }
        public string Nombre { get => nombre; set { nombre = value; OnPropertyChanged("Nombre"); } }
        public Rol Rol { get => rol; set { rol = value; OnPropertyChanged("Rol"); } }
        public string Unidad { get => unidad; set { unidad = value; OnPropertyChanged("Unidad"); } }
        public string Jerarquia { get => jerarquia; set { jerarquia = value; OnPropertyChanged("Jerarquia"); } }
        public UserType TipoUsuario { get; set; }

        public User()
        {
        }

        public bool EliminarUsuario()
        {
            return true;
        }
    }
}
