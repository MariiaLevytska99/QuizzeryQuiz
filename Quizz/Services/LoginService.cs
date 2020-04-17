using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.ServiceModels;
using Quizz.Constants;

namespace Quizz.Services
{
    public class LoginService
    {
       
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<bool> Login(string username, string password)
        {
            var data = new
            {
                username = username,
                password = password
            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Constants.Constants.LoginUri, stringContent);

            if (result.IsSuccessStatusCode)
            {
                var body = await result.Content.ReadAsStringAsync();
                var token = await Task.Run(() => JsonConvert.DeserializeObject<LoginModel>(body));

                App.user = new Models.User
                {
                    Username = username,
                    Token = token.AuthToken,
                    Email = token.Email
                };
                App.settings.Email = token.Email;
                App.settings.Username = username;
                App.settings.Password = password;
                App.settings.Sound = true;
                App.settings.Notification = true;

                App.Token = token.AuthToken;

                return true;

            }

            return false;
        }

        public async Task<bool> ResetPassword(string username, string password)
        {
            var data = new
            {
                email = username,
                password = password
            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Constants.Constants.ResetPasswordUri, stringContent);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }

}