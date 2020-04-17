using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.ServiceModels;
using Quizz.Constants;

namespace Quizz.Services
{
    public class RegistrationService
    {
        public async Task<bool> Registration(RegistrationModel registration)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Constants.Constants.RegistrationUri);
            var stringContent = new StringContent(JsonConvert.SerializeObject(registration), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Constants.Constants.RegistrationUri, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
