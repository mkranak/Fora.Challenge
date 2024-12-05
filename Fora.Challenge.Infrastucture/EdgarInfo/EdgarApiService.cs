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
        }

        public async Task<EdgarCompanyInfo> FetchEdgarCompanyInfoAsync(int cik)
        {
            string formattedCik = cik.ToString().PadLeft(10, '0');
            string url = $"api/xbrl/companyfacts/CIK{formattedCik}.json";

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
