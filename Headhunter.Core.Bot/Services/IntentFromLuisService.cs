using Headhunter.Core.Bot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Headhunter.Core.Bot.Services
{
    public class IntentFromLuisService : IIntentFromLuisService
    {
        public async Task<LUISDataModel> Get(string query)
        {
            query = Uri.EscapeDataString(query);
            var sourceToReturn = new LUISDataModel();

            using (var client = new HttpClient())
            {
                string RequestURI = "URL" + "&q=" + query;
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    sourceToReturn = JsonConvert.DeserializeObject<LUISDataModel>(JsonDataResponse);
                }
            }
            return sourceToReturn;
        }
    }
}
