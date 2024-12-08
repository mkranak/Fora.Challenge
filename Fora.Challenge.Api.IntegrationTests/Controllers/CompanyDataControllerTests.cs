using Fora.Challenge.Application.Features.FinancialData.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Fora.Challenge.Api.IntegrationTests.Controllers
{
    public class CompanyDataControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CompanyDataControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SaveCompanyData_WhenValidInput_ExpectNoContent()
        {
            // Arrange
            var command = new SaveCompanyDataCommand
            {
                Ciks = new List<int> { 18926 }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/CompanyData/import", command);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
