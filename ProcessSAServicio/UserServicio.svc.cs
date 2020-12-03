using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
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

        public List<Sexo> GetAllSexos()
        {
            return Sexo.GetAllSexo();
        }

        public List<EstadoUsuario> GetAllEstadoUsuarios()
        {
            return EstadoUsuario.GetAllEstadoUsuario();
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

        public void AddUser(User Usuario, string Username, string Password)
        {
            Usuario.GuardarCliente(Username, Password);
        }

        public void DeleteUser(string id)
        {
            User.Delete(int.Parse(id), UserType.USUARIO_CLIENTE);
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

        public void AddEmpresa(Empresa emp, string contrato)
        {
            emp.Contrato = Convert.FromBase64String(contrato);
            emp.Guardar();
        }

        public void EliminarEmpresa(string id)
        {
            Empresa.Eliminar(int.Parse(id));
        }

        public Empresa GetEmpresaDeUsuario(string id)
        {
            return Empresa.GetEmpresaDeUsuario(int.Parse(id));
        }

        #endregion
        #region Cargos
        public List<Cargo> GetCargosDeEmpresa(string id)
        {
            return Cargo.GetCargosDeEmpresa(int.Parse(id));
        }
        public List<Cargo> GetCargosDeEmpresaFlat(string id)
        {
            return Cargo.GetCargosDeEmpresaFlat(int.Parse(id));
        }

        public Cargo GetCargo(string id)
        {
            return Cargo.GetCargo(int.Parse(id));
        }

        public void AddCargo(Cargo Cargo, int Padre, int Empresa)
        {
            Cargo.Guardar(Padre, Empresa);
        }

        public void EliminarCargo(string id)
        {
            Cargo.Eliminar(int.Parse(id));
        }

        public List<Cargo> CargosFuncionario(string id)
        {
            return Cargo.GetCargosDeFuncionario(int.Parse(id));
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

        public List<Tarea> GetAllTareasRol(string id)
        {
            return Tarea.GetAllTareaCargo(int.Parse(id));
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
        #region TareaInstancia

        public List<TareaInstancia> GetAllTareaInstancia(string id)
        {
            return TareaInstancia.GetAllTareasDeUser(int.Parse(id));
        }

        public void CambiarEstadoTarea(int idTarea, int idEstado)
        {
            TareaInstancia.CambiarEstado(idTarea, idEstado);
        }

        public void CambiarEstadoTareas(List<int> idTareas, int idEstado)
        {
            foreach (int i in idTareas)
            {
                TareaInstancia.CambiarEstado(i, idEstado);
            }
        }

        public List<EstadoTarea> GetAllEstadoTareas()
        {
            return EstadoTarea.GetAllEstadoTareas();
        }

        public List<TareaInstancia> GetAllTareasRechazadas(string id)
        {
            return TareaInstancia.GetAllTareasRechazadasDeUser(int.Parse(id));
        }

        public void CambiarProgresoTarea(int idTarea, int progreso)
        {
            TareaInstancia.CambiarProgresoTarea(idTarea, progreso);
        }

        #endregion
        #region Roles

        public void AddRol(Rol rol)
        {
            rol.Guardar();
        }

        public List<Rol> GetAllRoles(string id)
        {
            return Rol.GetAllRol(int.Parse(id));
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

        public List<Flujo> GetAllFlujos(string id)
        {
            return Flujo.GetAllFlujos(int.Parse(id));
        }

        public void EliminarFlujo(string id)
        {
            Flujo.Eliminar(int.Parse(id));
        }

        public List<EstadoFlujo> GetAllEstadoFlujos()
        {
            return EstadoFlujo.GetAllEstadoFlujos();
        }

        public void AddTareasToFlujo(List<Tarea> tareas, int id)
        {
            Flujo.AddTareasToFlujo(tareas, id);
        }

        public void CambiarEstadoFlujo(int idFlujo, int idEstado)
        {
            FlujoInstancia.CambiarEstado(idFlujo, idEstado);
        }

        public List<Flujo> GetAllFlujosDeCargo(string id)
        {
            return Flujo.GetAllFlujosDeCargo(int.Parse(id));
        }
        #endregion
        #region FlujoInstancias
        public void EjecutarFlujo(int idFlujo, int idResponsable, List<EjecutarTareaRequestData> tareaData)
        {
            Flujo.Ejecutar(idFlujo, idResponsable, tareaData);
        }

        public List<FlujoInstancia> GetAllFlujoInstanciaDeUsuario(string id)
        {
            return FlujoInstancia.GetAllFromUser(int.Parse(id));
        }

        public List<MasFlujosReportStructure> GetReporte(string id)
        {
            switch (id)
            {
                case "1":
                    return MasFlujosReportStructure.GetReporte();
                default:
                    return null;
                    break;
            }
        }
        #endregion
    }
}
