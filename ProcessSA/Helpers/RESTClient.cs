using Newtonsoft.Json;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class RESTClient
    {
        public static HttpClient client = new HttpClient();

        public static string USER_URL = ConfigurationManager.AppSettings.Get("users-url");
        public static string BASE_URL = ConfigurationManager.AppSettings.Get("base-url");

        public async static Task<List<EstadoTarea>> GetAllEstadoTareas()
        {

            List<EstadoTarea> etareas = new List<EstadoTarea>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "EstadoTarea");
            if (responseMessage.IsSuccessStatusCode)
            {
                etareas = await responseMessage.Content.ReadAsAsync<List<EstadoTarea>>();
            }
            return etareas;
        }

        public async static Task<List<Flujo>> GetAllFlujos(int id)
        {


            List<Flujo> etareas = new List<Flujo>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Flujos/Empresa/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                etareas = await responseMessage.Content.ReadAsAsync<List<Flujo>>();
            }
            return etareas;
        }

        public async static Task<List<Reporte1>> GetReportes()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Reportes/1");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<List<Reporte1>>();
            }
            return null;
        }

        public async static Task<Empresa> GetEmpresa(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Empresas/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
               return await responseMessage.Content.ReadAsAsync<Empresa>();
            }
            return null;
        }

        public async static Task<List<EstadoFlujo>> GetAllEstadoFlujos()
        {
            List<EstadoFlujo> etareas = new List<EstadoFlujo>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "EstadoFlujos");
            if (responseMessage.IsSuccessStatusCode)
            {
                etareas = await responseMessage.Content.ReadAsAsync<List<EstadoFlujo>>();
            }
            return etareas;

        }

        public async static Task<bool> GuardarFlujo(Flujo flujo)
        {
            string jsonData = JsonConvert.SerializeObject(flujo, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddFlujo", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task<bool> EliminarCargo(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(USER_URL + "EliminarCargo/" + id.ToString());
            return response.IsSuccessStatusCode;
        }

        public async static Task<List<Sexo>> GetAllSexos()
        {
            List<Sexo> sexos = new List<Sexo>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Sexos");
            if (responseMessage.IsSuccessStatusCode)
            {
                sexos = await responseMessage.Content.ReadAsAsync<List<Sexo>>();
            }
            return sexos;
        }

        public async static Task<bool> EliminarFlujo(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(USER_URL + "EliminarFlujo/" + id.ToString());
            return responseMessage.IsSuccessStatusCode;
        }

        public static void InitClient()
        {
            
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async static Task<List<User>> GetUsersAsync()
        {
            List<User> users = new List<User>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Users");
            if (responseMessage.IsSuccessStatusCode)
            {
                users = await responseMessage.Content.ReadAsAsync<List<User>>();
            }
            return users;
        }

        public async static Task<User> LoginSistema(string user, string pass)
        {
            string jsonData = JsonConvert.SerializeObject(
                new { Username = user, Password = pass }
                );
            StringContent content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "LoginSistema", content);
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadAsAsync<User>();
            }
            return null;
        }

        public async static Task<bool> GuardarUser(User user, string username, string pass)
        {
            string jsonData = JsonConvert.SerializeObject(
                new { Usuario = user, Username = username, Password = pass }
                );
            StringContent content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddUser", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task<List<User>> GetUsersDeEmpresa(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "/UsersEmpresa/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<List<User>>();
            }
            return null;
        }

        public async static Task<bool> GuardarCargo(Cargo departamento, int idjer, int empresaid)
        {
            string jsonData = JsonConvert.SerializeObject(new { Cargo = departamento, Padre = idjer, Empresa = empresaid });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddDepartamento", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task<List<Cargo>> GetAllDepartamentosJerarquia(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "/Departamentos/Empresa/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<List<Cargo>>();
            }
            return null;
        }
        public async static Task<List<Cargo>> GetAllDepartamentosList(int id)
        {

            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "/Departamentos/Empresa/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                List<Cargo> cargos = await responseMessage.Content.ReadAsAsync<List<Cargo>>();
                return Cargo.Traverse(cargos.FirstOrDefault()).ToList();
            }
            throw new NullReferenceException("No existe.");
        }

        internal static Task ActualizarEmpresa(Empresa emp)
        {
            throw new NotImplementedException();
        }

        public async static Task<bool> GuardarEmpresa(Empresa emp, byte[] archivo)
        {
            string jsonData = JsonConvert.SerializeObject(new { Empresa = emp, Contrato = Convert.ToBase64String(archivo) });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddEmpresa", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task GuardarJerarquia(JerarquiaDepartamento jer, int id)
        {
            string jsonData = JsonConvert.SerializeObject(new { Jerarquia = jer, EmpresaId = id });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddJerarquia/Departamento", content);
            if (message.IsSuccessStatusCode)
            {
                return;
            }
        }

        public async static Task<List<Empresa>> GetAllEmpresas()
        {
            List<Empresa> empresas = new List<Empresa>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Empresas");
            if (responseMessage.IsSuccessStatusCode)
            {
                empresas = await responseMessage.Content.ReadAsAsync<List<Empresa>>();
            }
            return empresas;
        }

        public async static Task<bool> EliminarEmpresa(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(USER_URL + "EliminarEmpresa/" + id.ToString());
            return responseMessage.IsSuccessStatusCode;
        }

        public async static Task<bool> EliminarUser(int id)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(USER_URL + "EliminarUser/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) return true;
            return false;
        }

        public async static Task<bool> GuardarTarea(Tarea tarea)
        {
            string jsonData = JsonConvert.SerializeObject(tarea, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddTarea", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task EliminarTarea(Tarea tarea)
        {
            HttpResponseMessage responseMessage = await client.DeleteAsync(USER_URL + "EliminarTarea/" + tarea.Id.ToString());
        }

        public async static Task<List<Tarea>> GetAllTareas()
        {
            List<Tarea> tareas = new List<Tarea>();
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Tareas");
            if (responseMessage.IsSuccessStatusCode)
            {
                tareas = await responseMessage.Content.ReadAsAsync<List<Tarea>>();
            }
            return tareas;
        }

        public async static Task<List<Rol>> GetAllRoles(int id)
        {
            List<Rol> roles = new List<Rol>();
            HttpResponseMessage response = await client.GetAsync(USER_URL + "Roles/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                roles = await response.Content.ReadAsAsync<List<Rol>>();
            }
            return roles;
        }

        public async static Task<bool> GuardarRol(Rol rol)
        {
            string jsonData = JsonConvert.SerializeObject(rol, new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddRol", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async static Task EliminarRol(int id)
        {
            HttpResponseMessage message = await client.DeleteAsync(USER_URL + "EliminarRol/" + id.ToString());
            if (message.IsSuccessStatusCode)
            {
                return;
            }
            return;
        }
    }
}
