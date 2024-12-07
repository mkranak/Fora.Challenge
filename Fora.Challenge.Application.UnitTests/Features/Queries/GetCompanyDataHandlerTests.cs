using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Features.Profiles;
using Fora.Challenge.Application.UnitTests.Mock;
using Fora.Challenge.Domain.Entities;
using Moq;
using System.Xml.Serialization;

namespace Fora.Challenge.Application.UnitTests.Features.Queries
{
    public class GetCompanyDataHandlerTests
    {
        private readonly GetCompanyDataHandler _handler;
        private readonly Mock<ICompanyDataRepository> _companyDataRepositoryMock;

        public GetCompanyDataHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            _companyDataRepositoryMock = new Mock<ICompanyDataRepository>();

            _handler = new GetCompanyDataHandler(_companyDataRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task Handle_WhenFilterPassedIn_ExpectSuccess()
        {
            // Arrange
            var request = new GetCompanyDataQuery { StartsWith = "T" };
            var responseCompanies = new List<Company>()
            {
                new Company
                {
                    Id = 1,
                    Cik = 1,
                    EntityName = "First_Name"
                },
                new Company
                {
                    Id = 2,
                    Cik = 2,
                    EntityName = "Second_Name"
                }
            };

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.StartsWith, responseCompanies);

            // Act
            var actual = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(responseCompanies.Count, actual.Count);
            Assert.Equal(responseCompanies.ElementAt(0).Id, actual.ElementAt(0).Id);
            Assert.Equal(responseCompanies.ElementAt(0).EntityName, actual.ElementAt(0).Name);
            Assert.Equal(responseCompanies.ElementAt(1).Id, actual.ElementAt(1).Id);
            Assert.Equal(responseCompanies.ElementAt(1).EntityName, actual.ElementAt(1).Name);
            _companyDataRepositoryMock.Verify();
        }

        [Fact]
        public async Task Handle_WhenFilterNotPassedIn_ExpectSuccess()
        {
            // Arrange
            var request = new GetCompanyDataQuery ();
            var responseCompanies = new List<Company>()
            {
                new Company
                {
                    Id = 1,
                    Cik = 1,
                    EntityName = "First_Name"
                },
                new Company
                {
                    Id = 2,
                    Cik = 2,
                    EntityName = "Second_Name"
                }
            };

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.StartsWith, responseCompanies);

            // Act
            var actual = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(responseCompanies.Count, actual.Count);
            Assert.Equal(responseCompanies.ElementAt(0).Id, actual.ElementAt(0).Id);
            Assert.Equal(responseCompanies.ElementAt(0).EntityName, actual.ElementAt(0).Name);
            Assert.Equal(responseCompanies.ElementAt(1).Id, actual.ElementAt(1).Id);
            Assert.Equal(responseCompanies.ElementAt(1).EntityName, actual.ElementAt(1).Name);
            _companyDataRepositoryMock.Verify();
        }

        [Fact]
        public async Task Handle_WhenNoCompanyData_ExpectEmptyList()
        {
            // Arrange
            var request = new GetCompanyDataQuery();
            var responseCompanies = new List<Company>(); // empty list

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.StartsWith, responseCompanies);

            // Act
            var actual = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(actual);
            Assert.Empty(actual);
            _companyDataRepositoryMock.Verify();
        }
    }
}
