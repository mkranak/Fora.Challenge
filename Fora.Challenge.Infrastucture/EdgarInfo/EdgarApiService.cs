using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using System.Text.Json;

namespace Fora.Challenge.Infrastucture.EdgarInfo
{
    public class EdgarApiService : IEdgarApiService
    {
        private readonly HttpClient _httpClient;

        public EdgarApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.34.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        }

        public async Task<EdgarCompanyInfo> FetchEdgarCompanyInfoAsync(string cik)
        {
            string formattedCik = cik.PadLeft(10, '0');
            string url = $"https://data.sec.gov/api/xbrl/companyfacts/CIK{formattedCik}.json";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<EdgarCompanyInfo>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                // Log exception (e.g., using ILogger)
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }

            return null;
        }
    }
}
