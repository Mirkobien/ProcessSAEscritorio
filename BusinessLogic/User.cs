using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLogic
{
    public enum UserType
    {
        USUARIO_CLIENTE,
        USUARIO_SISTEMA
    }

    [DataContract]
    public class EstadoUsuario
    {
        public EstadoUsuario(ESTADO_USUARIO estado)
        {
            Id = decimal.ToInt32(estado.IDESU);
            Nombre = estado.DESCRIPCION;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        public static List<EstadoUsuario> GetAllEstadoUsuario()
        {
            List<EstadoUsuario> estadoUsuarios = new List<EstadoUsuario>();
            Entities ent = new Entities();
            foreach(ESTADO_USUARIO est in ent.ESTADO_USUARIO)
            {
                estadoUsuarios.Add(new EstadoUsuario(est));
            }
            return estadoUsuarios;
        }
    }

    [DataContract]
    public class AuthUser
    {
        public AuthUser(AUTH_USER auth)
        {
            Id = decimal.ToInt32(auth.IDAUT);
            Usuario = auth.USERNAME;
            Contrasenia = auth.PASSWORD;
        }
        public AuthUser()
        {

        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Contrasenia { get; set; }
    }
    [DataContract]
    public class User
    {
        public User()
        {
            
        }
        public User(USUARIO_SISTEMA usuario)
        {
            Id = decimal.ToInt32(usuario.IDSIS);
            Nombre = usuario.NOMBRE;
            Apellido = usuario.APELLIDO;
            Rol = new Rol(usuario.ROL_SISTEMA);
            TipoUsuario = UserType.USUARIO_SISTEMA;
        }
        public User(USUARIO_CLIENTE usuario)
        {
            Id = decimal.ToInt32(usuario.IDUSU);
            Nombre = usuario.NOMBRE;
            Apellido = usuario.PATERNO;
            ApellidoMaterno = usuario.MATERNO;
            Rut = usuario.RUT;
            Correo = usuario.CORREO;
            Sexo = new Sexo(usuario.SEXO);
            Rol = new Rol(usuario.ROL_CLIENTE);
            //if (Rol.Id == 3)
                Cargo = new Cargo(usuario.CARGOS);
            Estado = new EstadoUsuario(usuario.ESTADO_USUARIO);
            TipoUsuario = UserType.USUARIO_CLIENTE;
        }

        public static List<User> GetAllUsersEmpresa(int v)
        {
            Entities ent = new Entities();
            List<USUARIO_CLIENTE> lista =
                ent.USUARIO_CLIENTE.Where(p => p.CARGOS.EMPRESA_IDEMP == v).ToList();

            List<User> listaFinal = new List<User>();
            
            foreach(USUARIO_CLIENTE usr in lista)
            {
                listaFinal.Add(new User(usr));
            }
            return listaFinal;
        }

        #region database operations
        /// <summary>
        /// Consigue todos los usuarios de una base de datos.
        /// </summary>
        /// <param name="type">Tipo de Usuario</param>
        /// <returns>Una lista de todos los usuarios del tipo especificado</returns>
        public static List<User> GetAllUsers(UserType type){
            List<User> listaFinal = new List<User>();
            Entities ent = new Entities();

            if (type == UserType.USUARIO_SISTEMA)
            {
                List<USUARIO_SISTEMA> lista = ent.USUARIO_SISTEMA.ToList();
                foreach(USUARIO_SISTEMA user in lista)
                {
                    listaFinal.Add(new User(user));
                }
            } else if (type == UserType.USUARIO_CLIENTE)
            {
                List<USUARIO_CLIENTE> lista = ent.USUARIO_CLIENTE.ToList();
                foreach (USUARIO_CLIENTE user in lista)
                {
                    listaFinal.Add(new User(user));
                }
            }
            return listaFinal;
        }

        /// <summary>
        /// Consigue un Usuario con el Usuario y Contraseña, y tipo usuario especificados.
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <param name="tipo">Tipo de usuario</param>
        /// <returns>El usuario conseguido. Si no existe, es nulo.</returns>
        public static User Login(string username, string password, UserType tipo)
        {
            Entities ent = new Entities();
            User user = null;

            AUTH_USER auth = ent.AUTH_USER.Where(p => p.USERNAME == username &&
            p.PASSWORD == password).FirstOrDefault();

            if (auth == null)
                return null;

            if (tipo == UserType.USUARIO_CLIENTE)
            {
                USUARIO_CLIENTE usuario = auth.USUARIO_CLIENTE;
                if (usuario == null)
                    return null;
                user = new User(usuario);
                return user;
            } else
            {
                USUARIO_SISTEMA usuario = auth.USUARIO_SISTEMA;
                if (usuario == null)
                    return null;
                user = new User(usuario);
                return user;
            }
        }


        public static User Login(string username, string password)
        {
            Entities ent = new Entities();
            User user = null;

            AUTH_USER auth = ent.AUTH_USER.Where(p => p.USERNAME == username &&
            p.PASSWORD == password).FirstOrDefault();

            if (auth == null)
                return null;

            if (auth.USUARIO_CLIENTE != null)
            {
                USUARIO_CLIENTE usuario = auth.USUARIO_CLIENTE;
                if (usuario == null)
                    return null;
                user = new User(usuario);
                return user;
            }
            else if (auth.USUARIO_SISTEMA != null)
            {
                USUARIO_SISTEMA usuario = auth.USUARIO_SISTEMA;
                if (usuario == null)
                    return null;
                user = new User(usuario);
                return user;
            }

            return null;
        }
        public static bool Delete(int id, UserType type)
        {
            Entities ent = new Entities();
            if (type == UserType.USUARIO_CLIENTE)
            {
                USUARIO_CLIENTE user = ent.USUARIO_CLIENTE.Where(p => p.IDUSU == id).FirstOrDefault();
                ent.AUTH_USER.Remove(ent.AUTH_USER.Where(a => a.USUARIO_CLIENTE_IDUSU == user.IDUSU).FirstOrDefault());
                ent.USUARIO_CLIENTE.Remove(user);
                ent.SaveChanges();
                return true;
            } else if (type == UserType.USUARIO_SISTEMA)
            {
                USUARIO_SISTEMA user = ent.USUARIO_SISTEMA.Where(p => decimal.ToInt32(p.IDSIS) == id).FirstOrDefault();
                ent.AUTH_USER.Remove(ent.AUTH_USER.Where(a => a.USUARIO_SISTEMA_IDSIS == id).FirstOrDefault());
                ent.USUARIO_SISTEMA.Remove(user);
                ent.SaveChanges();
                return true;
            }
            return false;
        }
        public void GuardarCliente(string usuario, string contrasena)
        {
            Entities ent = new Entities();

            USUARIO_CLIENTE usu = new USUARIO_CLIENTE();

            usu.NOMBRE = this.Nombre;
            usu.PATERNO = this.Apellido;
            usu.SEXO_IDSEX = this.Sexo.Id;
            usu.MATERNO = this.ApellidoMaterno;
            usu.RUT = this.Rut;
            usu.TELEFONO = this.Telefono;
            usu.ROL_CLIENTE_IDROLC = this.Rol.Id;
            usu.CARGOS_IDDEP = this.Cargo.Id;
            usu.ESTADO_USUARIO_IDESU = this.Estado.Id;
            usu.CORREO = this.Correo;
            
            ent.USUARIO_CLIENTE.Add(usu);

            AUTH_USER auth = new AUTH_USER();
            auth.USERNAME = usuario;
            auth.PASSWORD = contrasena;
            auth.USUARIO_CLIENTE_IDUSU = usu.IDUSU;
            ent.AUTH_USER.Add(auth);

            ent.SaveChanges();
        }

        public static List<User> USUARIOToUser(ICollection<USUARIO_CLIENTE> col)
        {
            List<User> listaFinal = new List<User>();
            foreach (USUARIO_CLIENTE user in col)
            {
                listaFinal.Add(new User(user));
            }
            return listaFinal;
        }

        public static List<User> USUARIO_SISTEMAToUser(ICollection<USUARIO_SISTEMA> col)
        {
            List<User> listaFinal = new List<User>();
            foreach (USUARIO_SISTEMA user in col)
            {
                listaFinal.Add(new User(user));
            }
            return listaFinal;
        }

        public static List<User> GetUsuariosFromDepID(int id)
        {
            Entities ent = new Entities();
            return new List<User>();
        }
        #endregion

        #region data members
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Rut { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public string ApellidoMaterno { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public long Telefono { get; set; }
        [DataMember]
        public Sexo Sexo { get; set; }
        [DataMember]
        public Rol Rol { get; set; }
        [DataMember]
        public Cargo Cargo { get; set; }
        [DataMember]
        public UserType TipoUsuario { get; set; }
        [DataMember]
        public EstadoUsuario Estado { get; set; }
        #endregion
    }
}
