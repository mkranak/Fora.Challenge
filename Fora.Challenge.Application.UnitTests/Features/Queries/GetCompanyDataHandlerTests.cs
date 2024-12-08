using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Exceptions;
using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Features.Profiles;
using Fora.Challenge.Application.UnitTests.Mock;
using Fora.Challenge.Domain.Entities;
using Moq;
using System.Diagnostics.Metrics;

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
            var request = new GetCompanyDataQuery { Filter = "T" };
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

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.Filter, responseCompanies);

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

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.Filter, responseCompanies);

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
        public async Task Handle_WhenFilterIsLengthGreaterThanOne_ExpectBadRequestException()
        {
            // Arrange
            var request = new GetCompanyDataQuery { Filter = "Ta" };
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

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => 
                _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Filter can only be one letter.", exception.Message);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("!")]
        public async Task Handle_WhenFilterIsNotALetter_ExpectBadRequestException(string input)
        {
            // Arrange
            var request = new GetCompanyDataQuery { Filter = input };
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

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
                _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Filter can only be one letter.", exception.Message);
        }

        [Fact]
        public async Task Handle_WhenNoCompanyData_ExpectNotFoundException()
        {
            // Arrange
            var request = new GetCompanyDataQuery();
            var responseCompanies = new List<Company>(); // empty list

            _companyDataRepositoryMock.SetupGetCompanyDataAsyncMock(request.Filter, responseCompanies);

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("No companies found.", exception.Message);
            _companyDataRepositoryMock.Verify();
        }
    }
}
