using Newtonsoft.Json;
using ProcessSA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSA.Models
{
    class RESTClient
    {
        public static HttpClient client = new HttpClient();

        public static void InitClient()
        {
            client.BaseAddress = new Uri("http://localhost:50059/UserServicio.svc/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async static Task<List<User>> GetUsersAsync()
        {
            List<User> users = new List<User>();
            HttpResponseMessage responseMessage = await client.GetAsync("Users");
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
            HttpResponseMessage message = await client.PostAsync("LoginSistema", content);
            if (message.IsSuccessStatusCode)
            {
                return await message.Content.ReadAsAsync<User>();
            }
            return null;
        }
    }
}
