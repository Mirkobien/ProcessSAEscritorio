using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace ProcessSAServicio
{
    [ServiceContract]
    public interface IUserServicio
    {
        #region Usuarios

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Users/")]
        List<User> GetAllUsers();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LoginSistema/")]
        User LoginSistema(LoginRequestData data);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LoginCliente/")]
        User LoginCliente(LoginRequestData data);

        #endregion
        #region Empresas

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Empresas/")]
        List<Empresa> GetAllEmpresas();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GuardarEmpresa/")]
        void GuardarEmpresa(Empresa emp);
        #endregion

        #region Tareas
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Tareas/")]
        List<Tarea> GetAllTareas();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CambiarEstado/")]
        void CambiarEstadoTarea(int id, EstadoTarea estado);
        #endregion
    }

    [DataContract]
    public class LoginRequestData
    {
        public LoginRequestData(string username, string password)
        {
            Username = username;
            Password = password;
        }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
