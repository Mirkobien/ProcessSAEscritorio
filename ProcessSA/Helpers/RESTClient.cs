using Newtonsoft.Json;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class RESTClient
    {
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

        public async static Task<Empresa> GetEmpresa(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "Empresas/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
               return await responseMessage.Content.ReadAsAsync<Empresa>();
            }
            return null;
        }

        public static HttpClient client = new HttpClient();

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

        public async static Task<List<User>> GetUsersDeEmpresa(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(USER_URL + "/UsersEmpresa/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<List<User>>();
            }
            return null;
        }

        public async static Task<bool> GuardarDepartamento(Departamento departamento, int idjer)
        {
            string jsonData = JsonConvert.SerializeObject(new { Departamento = departamento, Idjer = idjer });
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddDepartamento", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        internal static Task ActualizarEmpresa(Empresa emp)
        {
            throw new NotImplementedException();
        }

        public async static Task<bool> GuardarEmpresa(Empresa emp)
        {
            string jsonData = JsonConvert.SerializeObject(emp);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(USER_URL + "AddEmpresa", content);
            if (message.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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

        public async static void EliminarEmpresa(Empresa emp)
        {
            //TODO: Eliminar
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

        public async static Task<List<Rol>> GetAllRoles()
        {
            List<Rol> roles = new List<Rol>();
            HttpResponseMessage response = await client.GetAsync(USER_URL + "Roles");
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
