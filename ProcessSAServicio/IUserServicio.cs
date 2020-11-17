using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
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
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Sexos/")]
        List<Sexo> GetAllSexos();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/EstadoUsuario/")]
        List<EstadoUsuario> GetAllEstadoUsuarios();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UsersEmpresa/{id}")]
        List<User> GetAllUsersEmpresa(string id);

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

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/AddUser/")]
        void AddUser(User Usuario, string Username, string Password);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/EliminarUser/{id}")]

        void DeleteUser(string id);

        #endregion
        #region Empresas

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Empresas/")]
        List<Empresa> GetAllEmpresas();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Empresas/{id}")]
        Empresa GetEmpresa(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/AddEmpresa/")]
        void AddEmpresa(Empresa Empresa, string Contrato);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/EliminarEmpresa/{id}")]
        void EliminarEmpresa(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Empresa/Usuario/{id}")]
        Empresa GetEmpresaDeUsuario(string id);

        #endregion
        #region Departamentos
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Departamentos/Empresa/{id}")]
        List<Departamento> GetDepartamentoDeEmpresa(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DepartamentosList/Empresa/{id}")]
        List<Departamento> GetDepartamentoDeEmpresaAsList(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Departamentos/{id}")]
        Departamento GetDepartamento(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/AddDepartamento/")]
        void AddDepartamento(Departamento Departamento, int Padre, int Empresa);
        #endregion
        #region Tareas
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AddTarea/")]
        void AddTarea(Tarea tarea);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Tareas/")]
        List<Tarea> GetAllTareas();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/TareasDeEmpresa/{id}")]
        List<Tarea> GetAllTareasEmpresa(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/TareasDeRol/{id}")]
        List<Tarea> GetAllTareasRol(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CambiarEstadoTarea/")]
        void CambiarEstadoTarea(int idTarea, int idEstado);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CambiarEstadoTareas/")]
        void CambiarEstadoTareas(List<int> idTareas, int idEstado);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EstadoTarea/")]
        List<EstadoTarea> GetAllEstadoTareas();

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EliminarTarea/{id}")]
        void EliminarTarea(string id);
        #endregion
        #region Roles

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AddRol/")]
        void AddRol(Rol rol);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Roles/{id}")]
        List<Rol> GetAllRoles(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/EliminarRol/{id}")]
        void EliminarRol(string id);

        #endregion
        #region Flujos
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/AddFlujo/")]
        void AddFlujo(Flujo flujo);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Flujos/Empresa/{id}")]
        List<Flujo> GetAllFlujos(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/EstadoFlujos/")]
        List<EstadoFlujo> GetAllEstadoFlujos();

        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/EliminarFlujo/{id}")]
        void EliminarFlujo(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/AddFlujoToTarea/")]
        void AddTareasToFlujo(List<Tarea> tareas, int id);
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
