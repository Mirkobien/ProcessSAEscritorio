using Newtonsoft.Json;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class RESTClient
    {
        public static string USER_URL = ConfigurationManager.AppSettings.Get("users-url");
        public static string BASE_URL = ConfigurationManager.AppSettings.Get("base-url");

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
    }
}
