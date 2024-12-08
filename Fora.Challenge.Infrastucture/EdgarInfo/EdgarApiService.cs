using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Fora.Challenge.Infrastucture.EdgarInfo
{
    public class EdgarApiService : IEdgarApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EdgarApiService> _logger;

        /// <summary>Initializes a new instance of the <see cref="EdgarApiService"/> class.</summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="logger">The logger.</param>
        public EdgarApiService(HttpClient httpClient, ILogger<EdgarApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>Fetches the edgar company information asynchronous.</summary>
        /// <param name="cik">The cik.</param>
        /// <returns>Company info.</returns>
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
                _logger.LogError($"Unable to fetch edgar company info for cik={cik}");
            }

            return null;
        }
    }
}
