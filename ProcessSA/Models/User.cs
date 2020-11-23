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
    public class EstadoUsuario : ObservableObject
    {
        public EstadoUsuario()
        {

        }

        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class Sexo : ObservableObject
    {
        public Sexo()
        {

        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    public enum UserType
    {
        USUARIO_CLIENTE,
        USUARIO_SISTEMA
    }
    public class User : ObservableObject
    {
        private int id;
        private string nombre;
        private string rut;
        private Rol rol;
        private string apellido;
        private string apellidoMaterno;
        private string telefono;
        private string correo;
        private Sexo sexo;
        private EstadoUsuario estado;
        private Cargo cargo;

        public int Id { get => id; set { id = value; OnPropertyChanged("Id"); } }
        public string Rut { get => rut; set { rut = value; OnPropertyChanged("Rut"); } }
        public string Nombre { get => nombre; set { nombre = value; OnPropertyChanged("Nombre"); } }
        public string Apellido { get => apellido; set { apellido = value; OnPropertyChanged("Apellido"); } }
        public string ApellidoMaterno { get => apellidoMaterno; set { apellidoMaterno = value; OnPropertyChanged("ApellidoMaterno"); } }
        public Rol Rol { get => rol; set { rol = value; OnPropertyChanged("Rol"); } }
        public string Correo { get => correo; set { correo = value; OnPropertyChanged("Correo"); } }
        public string Telefono { get => telefono; set { telefono = value; OnPropertyChanged("Telefono"); } }
        public Sexo Sexo { get => sexo; set { sexo = value; OnPropertyChanged("Sexo"); } }
        public EstadoUsuario Estado { get => estado; set { estado = value; OnPropertyChanged("Estado"); } }
        public Cargo Cargo { get => cargo; set { cargo = value; OnPropertyChanged("Cargo"); } }

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
