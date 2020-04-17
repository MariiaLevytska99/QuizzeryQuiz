using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Quizz.DTO;
using Quizz.Constants;

namespace Quizz.Services
{
    public class CategoriesService
    {

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "applictaion/json");
            return client;
        }

        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var client = GetClient();
            client.BaseAddress = new Uri(Constants.Constants.CategoriesUri);
            string result = await client.GetStringAsync(Constants.Constants.CategoriesUri);
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(result);
        }
    }
}

