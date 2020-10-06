using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ProcessSAServicio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ProcessSAServicio" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ProcessSAServicio.svc o ProcessSAServicio.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserServicio : IUserServicio
    {
        private readonly Entities ent = new Entities();
        public List<User> GetAllUsers()
        {
            return User.GetAllUsers(UserType.USUARIO_CLIENTE);
        }

        public User LoginSistema(RequestData data)
        {
            try
            {
                return User.Login(data.Username, data.Password, UserType.USUARIO_SISTEMA);
            }
            catch (Exception)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }
        }

        public User LoginCliente(RequestData data)
        {
            try
            {
                return User.Login(data.Username, data.Password, UserType.USUARIO_CLIENTE);
            }
            catch (Exception)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
