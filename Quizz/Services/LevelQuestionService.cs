using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.DTO;
using Quizz.Constants;

namespace Quizz.Services
{
    public class LevelQuestionsService
    {
        
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<IEnumerable<QuestionDTO>> Get(int levelIdParam)
        {
            var data = new
            {
                level = levelIdParam
            };

            var client = GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(Constants.Constants.LevelquestionUri, stringContent);

            var responseBody = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var questions = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<QuestionDTO>>(responseBody));


            return questions;
        }
    }
}
