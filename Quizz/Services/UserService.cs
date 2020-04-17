using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.ServiceModels;
using Quizz.Constants;

namespace Quizz.Services
{
    public class UserService
    {
       
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<bool> ChangeUserInformation(PersonalInformationModel personalInformationModel)
        {
            var data = new
            {
                username = personalInformationModel.Username,
                password = personalInformationModel.Password,
                email = personalInformationModel.Email,
                token = personalInformationModel.Token
            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Constants.Constants.UpdateInformationUri, stringContent);

            if (result.IsSuccessStatusCode)
            {
                var body = await result.Content.ReadAsStringAsync();
                var token = await Task.Run(() => JsonConvert.DeserializeObject<LoginModel>(body));


                App.user = new Models.User
                {
                    Username = personalInformationModel.Username,
                    Token = token.AuthToken,
                    Email = token.Email
                };

                App.settings.Email = token.Email;
                App.settings.Username = personalInformationModel.Username;
                App.settings.Password = personalInformationModel.Password;
                App.SettingsDatabase.SaveSettingsAsync(App.settings);
                App.Token = token.AuthToken;

                return true;
            }
            return false;
        }

    }

}