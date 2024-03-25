using FoodTracker.Models.USDA;
using FoodTracker.Utility;
using FoodTrackerWeb.Services.Interfaces;
using System.Net.Http;

namespace FoodTrackerWeb.Services
{
    public class USDAService(HttpClient client) : IUSDAService
    {

        private readonly HttpClient _client = client;  // Does not need to be static if using factory DI
        private readonly string BasePath = SD.USDA_URL_SEARCH_GET;

        public async Task<USDABrandedQueryResult> Search(string userQuery)
        {
            try
            {
                int pageSize = 25;
                int pageNumber = 1;
                string sortBy = "dataType.keyword";
                string sortOrder = "asc";

                var query = new Dictionary<string, string>
                {
                    { "query", userQuery },
                    { "pageSize", $"{pageSize}" },
                    { "pageNumber", $"{pageNumber}" },
                    { "sortBy", sortBy },
                    { "sortOrder", sortOrder },
                    { "api_key", Env.USDA_API_KEY}
                };

                var builder = new UriBuilder(BasePath)
                {
                    Port = -1,
                    Query = QueryString.Create(query).ToString()
                };

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(builder.ToString()),
                    Method = HttpMethod.Get,
                };

                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode == false)
                    throw new ApplicationException($"Error calling API: {response.ReasonPhrase}");

                return await response.Content.ReadAsAsync<USDABrandedQueryResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new USDABrandedQueryResult() { Success = false };
            }
        }
    }
}
