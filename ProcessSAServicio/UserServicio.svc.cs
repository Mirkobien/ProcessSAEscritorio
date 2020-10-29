using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        #region Users
        public List<User> GetAllUsers()
        {
            return User.GetAllUsers(UserType.USUARIO_CLIENTE);
        }

        public List<User> GetAllUsersEmpresa(string id)
        {
            return User.GetAllUsersEmpresa(int.Parse(id));
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

        public void AddUser(User user)
        {
            user.Guardar();
        }

        #endregion
        #region Empresas
        public List<Empresa> GetAllEmpresas()
        {
            return Empresa.GetAllEmpresas();
        }

        public Empresa GetEmpresa(string id)
        {
            return Empresa.GetEmpresa(int.Parse(id));
        }

        public void AddEmpresa(Empresa emp)
        {
            emp.Guardar();
        }

        #endregion
        #region Tareas
        public List<Tarea> GetAllTareas()
        {
            return Tarea.GetAllTarea();
        }
        public void AddTarea(Tarea tarea)
        {
            tarea.Guardar();
        }

        public List<Tarea> GetAllTareasEmpresa(string id)
        {
            return Tarea.GetAllTarea(int.Parse(id));
        }

        public void CambiarEstadoTarea(int idTarea, int idEstado)
        {
            Tarea.GetTarea(idTarea).CambiarEstado(idEstado);
        }

        public List<EstadoTarea> GetAllEstadoTareas()
        {
            return EstadoTarea.GetAllEstadoTareas();
        }

        public void EliminarTarea(string id)
        {
            Tarea tar = new Tarea();
            try
            {
                tar.Id = int.Parse(id);
                tar.Eliminar();
            } catch (FormatException)
            {
                return;
            }
        }
        #endregion
        #region Roles

        public void AddRol(Rol rol)
        {
            rol.Guardar();
        }

        public List<Rol> GetAllRoles()
        {
            return Rol.GetAllRol();
        }

        public void EliminarRol(string id)
        {
            Rol.Eliminar(int.Parse(id));
        }

        #endregion
        #region Flujos

        public void AddFlujo(Flujo flujo)
        {
            flujo.Guardar();
        }

        public List<Flujo> GetAllFlujos()
        {
            return Flujo.GetAllFlujos();
        }

        public void EliminarFlujo(string id)
        {
            Flujo.Eliminar(int.Parse(id));
        }

        public void AddTareasToFlujo(List<Tarea> tareas, int id)
        {
            Flujo.AddTareasToFlujo(tareas, id);
        }

        public List<Departamento> GetDepartamentoDeEmpresa(string id)
        {
            return Departamento.GetDepartamentos(int.Parse(id));
        }

        public Departamento GetDepartamento(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
