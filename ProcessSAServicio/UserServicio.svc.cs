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
        public List<User> GetAllUsers()
        {
            return User.GetAllUsers(UserType.USUARIO_CLIENTE);
        }

        public User LoginSistema(LoginRequestData data)
        {
            try
            {
                return User.Login(data.Username, data.Password, UserType.USUARIO_SISTEMA);
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }
        }

        public User LoginCliente(LoginRequestData data)
        {
            try
            {
                return User.Login(data.Username, data.Password, UserType.USUARIO_CLIENTE);
            }
            catch (Exception ex)
            {
                throw new WebFaultException(System.Net.HttpStatusCode.NotFound);
            }
        }

        public List<Empresa> GetAllEmpresas()
        {
            return Empresa.GetAllEmpresas();
        }

        public void GuardarEmpresa(Empresa emp)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Created;
        }

        public List<Tarea> GetAllTareas()
        {
            return Tarea.GetAllTarea();
        }

        public void CambiarEstadoTarea(int id, EstadoTarea estado)
        {
            Tarea.GetTarea(id).CambiarEstado(estado);
        }
    }
}
