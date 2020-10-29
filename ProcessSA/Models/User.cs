using ProcessSA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    enum UserType
    {
        USUARIO_CLIENTE,
        USUARIO_SISTEMA
    }
    class User : ObservableObject
    {
        private int id;
        private string nombre;
        private string rut;
        private Rol rol;
        private string unidad;
        private string apellido;
        private string apellidoMaterno;

        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
        public string Rut { get => rut; set { rut = value; OnPropertyChanged("Rut"); } }
        public string Nombre { get => nombre; set { nombre = value; OnPropertyChanged("Nombre"); } }
        public string Apellido { get => apellido; set { apellido = value; OnPropertyChanged("Apellido"); } }
        public string ApellidoMaterno { get => apellidoMaterno; set { apellidoMaterno = value; OnPropertyChanged("ApellidoMaterno"); } }
        public Rol Rol { get => rol; set { rol = value; OnPropertyChanged("Rol"); } }
        public string Unidad { get => unidad; set { unidad = value; OnPropertyChanged("Unidad"); } }
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
