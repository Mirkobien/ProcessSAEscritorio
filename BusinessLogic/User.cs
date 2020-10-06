using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public enum UserType
    {
        USUARIO_CLIENTE,
        USUARIO_SISTEMA
    }
    [DataContract]
    public class User
    {
        public User()
        {
            
        }
        public User(USUARIO_SISTEMA usuario)
        {
            Id = decimal.ToInt32(usuario.ID);
            Nombre = usuario.NOMBRE;
            Rol = new Rol(usuario.ROL_SISTEMA);
            TipoUsuario = UserType.USUARIO_SISTEMA;
        }
        public User(USUARIO usuario)
        {
            Id = int.Parse(usuario.ID_USER);
            Nombre = usuario.NOMBRE;
            Apellido = usuario.PATERNO;
            Sexo = new Sexo(usuario.SEXO);
            Rol = new Rol(usuario.ROL_CLIENTE);
            TipoUsuario = UserType.USUARIO_CLIENTE;
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
                List<USUARIO> lista = ent.USUARIO.ToList();
                foreach (USUARIO user in lista)
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

            AUTH_USUARIO auth = ent.AUTH_USUARIO.Where(p => p.USERNAME == username &&
            p.PASSWORD == password).FirstOrDefault();

            if (auth == null)
                return null;

            if (tipo == UserType.USUARIO_CLIENTE)
            {
                USUARIO usuario = auth.USUARIO;
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

            AUTH_USUARIO auth = ent.AUTH_USUARIO.Where(p => p.USERNAME == username &&
            p.PASSWORD == password).FirstOrDefault();

            if (auth == null)
                return null;

            if (auth.USUARIO != null)
            {
                USUARIO usuario = auth.USUARIO;
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
                USUARIO user = ent.USUARIO.Where(p => p.ID_USER == id.ToString()).FirstOrDefault();
                ent.USUARIO.Remove(user);
                ent.SaveChanges();
                return true;
            } else if (type == UserType.USUARIO_SISTEMA)
            {
                USUARIO_SISTEMA user = ent.USUARIO_SISTEMA.Where(p => decimal.ToInt32(p.ID) == id).FirstOrDefault();
                ent.USUARIO_SISTEMA.Remove(user);
                ent.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool Add(User user, UserType type)
        {
            return true;
        }
        #endregion

        #region data members
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public Sexo Sexo { get; set; }
        [DataMember]
        public Rol Rol { get; set; }
        [DataMember]
        public UserType TipoUsuario { get; set; }
        #endregion
    }
}
