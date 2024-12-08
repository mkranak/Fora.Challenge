using Fora.Challenge.Application.Features.FinancialData.Commands;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Features.Responses;
using Fora.Challenge.Domain.Entities;
using Fora.Challenge.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace Fora.Challenge.Api.IntegrationTests.Controllers
{
    public class CompanyControllerTests
    {    
        [Fact]
        public async Task GetCompanyData_WhenRecordsAreReturned_ExpectOk()
        {
            // Arrange
            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/companies");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<GetCompanyDataResponse>>();
            Assert.Equal(2, actual.Count());            
        }

        [Fact]
        public async Task GetCompanyData_WhenFilterContainsMoreThanOneCharacter_ExpectBadRequest()
        {
            // Arrange
            var filter = "ab";

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await _client.GetAsync($"/api/v1/companies?filter={filter}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetCompanyData_WhenFilterIsApplied_ExpectOkAndOneRecord()
        {
            // Arrange
            var filter = "f";

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/v1/companies?filter={filter}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var actual = await response.Content.ReadFromJsonAsync<IEnumerable<GetCompanyDataResponse>>();
            Assert.Equal(1, actual.Count());
            Assert.Single(actual);
        }

        [Fact]
        public async Task GetCompanyData_WhenFilterIsApplied_ExpectNotFound()
        {
            // Arrange
            var filter = "z";

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/v1/companies?filter={filter}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var actualError = response.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        [Fact]
        public async Task GetCompanyData_WhenFilterIsNotALetter_ExpectBadRequest()
        {
            // Arrange
            var filter = "!";

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/v1/companies?filter={filter}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var actualError = response.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        

        [Fact]
        public async Task SaveCompanyData_WhenValidInput_ExpectNoContent()
        {
            // Arrange
            var command = new SaveCompanyDataCommand
            {
                Ciks = new List<int> { 1510524 }
            };

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/companies/bulk", command);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SaveCompanyData_WhenNoCiksInRequest_ExpectBadRequest()
        {
            // Arrange
            var command = new SaveCompanyDataCommand();

            await using var application = new CustomWebApplicationFactory<Program>();
            CreateAndSeedDatabase(application);
            using var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/companies/bulk", command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static void CreateAndSeedDatabase(WebApplicationFactory<Program> appFactory)
        {
            using (var scope = appFactory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CompanyDataDbContext>();
                db.Database.EnsureCreated();

                var companies = new List<Company>
                {
                    new Company
                    {
                        Cik = 1,
                        EntityName = "First Record",
                        NetIncomeLossData = new List<NetIncomeLossData>
                        {
                            new NetIncomeLossData
                            {
                                Form = "10-K",
                                Frame = "CY2021",
                                Val = 1000000
                            }
                        }
                    },
                    new Company
                    {
                        Cik = 2,
                        EntityName = "Second Record",
                        NetIncomeLossData = new List<NetIncomeLossData>
                        {
                            new NetIncomeLossData
                            {
                                Form = "10-KB",
                                Frame = "CY2022",
                                Val = 1000002
                            }
                        }
                    }
                };

                db.Companies.AddRange(companies);
                db.SaveChanges();
            }
        }
    }
}
