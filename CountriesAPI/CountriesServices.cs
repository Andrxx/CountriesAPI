using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


namespace CountriesAPI
{
    public  static class CountriesServices
    {
        private static HttpClient client = new()
        {
            BaseAddress = new Uri("https://restcountries.com/v3.1/"),
        };

        public static async Task<IActionResult> GetCountriesListAsync()
        {
            using HttpResponseMessage response = await client.GetAsync("all?fields=name,capital");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var deserialazedResp = JsonConvert.DeserializeObject(jsonResponse);

            var sorted = (deserialazedResp as JArray).OrderBy(c => (string)c["name"]["common"]);

            return new JsonResult(JsonConvert.SerializeObject(sorted));
        }

        public static async Task<IActionResult> GetCountryAsync(string name)
        {
            using HttpResponseMessage response = await client.GetAsync("name/" + name + "?fields=name,capital,region,languages");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return new JsonResult(jsonResponse);
        }

        public static async Task<IActionResult> FindCountryAsync(string name)
        {
            using HttpResponseMessage response = await client.GetAsync("name/" + name + "?fields=name,capital,region");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return new JsonResult(jsonResponse);
        }
    }
}
