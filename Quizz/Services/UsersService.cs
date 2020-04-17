using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.ServiceModels;
using Quizz.Constants;

namespace Quiz.Services
{
    public class UsersService
    {
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<IEnumerable<UsersModel>> GetUsers()
        {
            var client = GetClient();
            client.BaseAddress = new Uri(Constants.RatingUri);
            string result = await client.GetStringAsync(Constants.RatingUri);
            return JsonConvert.DeserializeObject<IEnumerable<UsersModel>>(result);

        }
    }
}
