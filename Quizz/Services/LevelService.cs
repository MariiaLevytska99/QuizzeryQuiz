using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.DTO;
using Quizz.Constants;

namespace Quizz.Services
{
    public class LevelService
    {

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<IEnumerable<LevelDTO>> Get(int categoryIdParam)
        {
            var data = new
            {
                category = categoryIdParam,
                token = App.Token
            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Constants.Constants.LevelsUri, stringContent);

            var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var levels = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<LevelDTO>>(responseBody));

            return levels;
        }

        public async void Post(int levelIdParam, int points)
        {
            var data = new
            {
                level = levelIdParam,
                score = points,
                token = App.UserDatabase.GetUser().Token
            };
            
            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            await client.PostAsync(Constants.Constants.LevelUri, stringContent);

        }

        public async void StartLevel(int levelIdParam)
        {
            var data = new
            {
                level = levelIdParam,
                token = App.UserDatabase.GetUser().Token            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            await client.PostAsync(Constants.Constants.UserLevelUri, stringContent);
        }
    }
}
