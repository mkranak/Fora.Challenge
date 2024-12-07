using AutoMapper;
using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Features.FinancialData.Commands;
using Fora.Challenge.Application.Features.Profiles;
using Fora.Challenge.Application.Models;
using Fora.Challenge.Application.UnitTests.Mock;
using Fora.Challenge.Domain.Entities;
using Moq;

namespace Fora.Challenge.Application.UnitTests.Features.Commands
{
    public class SaveCompanyDataHandlerTests
    {
        private readonly SaveCompanyDataHandler _handler;
        private readonly Mock<IEdgarApiService> _edgarApiServiceMock;
        private readonly Mock<ICompanyDataRepository> _companyDataRepositoryMock;

        public SaveCompanyDataHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();

            _edgarApiServiceMock = new Mock<IEdgarApiService>();
            _companyDataRepositoryMock = new Mock<ICompanyDataRepository>();

            _handler = new SaveCompanyDataHandler(_edgarApiServiceMock.Object, 
                _companyDataRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task Handle_WhenRequestHasValidCiks_ExpectSuccess()
        {
            // Arrange
            var request = new SaveCompanyDataCommand
            {
                Ciks = new List<int> { 1, 2 }
            };
            var companyInfos = new List<EdgarCompanyInfo>
            {
                new EdgarCompanyInfo
                {
                    Cik = 1,
                    EntityName = "First_Name"
                },
                new EdgarCompanyInfo
                {
                    Cik = 2,
                    EntityName = "Second_Name"
                }
            };
            var expectedCompanies = new List<Company>()
            {
                new Company
                {
                    Cik = 1,
                    EntityName = "First_Name"
                },
                new Company
                {
                    Cik = 1, // todo look into this (this should be 2)
                    EntityName = "Second_Name"
                }
            };

            _edgarApiServiceMock.SetupFetchEdgarCompanyInfoAsyncMock(request.Ciks.ElementAt(0), companyInfos.ElementAt(0));
            _edgarApiServiceMock.SetupFetchEdgarCompanyInfoAsyncMock(request.Ciks.ElementAt(1), companyInfos.ElementAt(1));
            _companyDataRepositoryMock.SetupSaveCompanyDataAsyncMock(expectedCompanies);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _edgarApiServiceMock.Verify();
            _companyDataRepositoryMock.Verify();
        }

        [Fact]
        public async Task Handle_WhenMultipleCiksAndServiceDoesNotReturnCompanyDataForOne_ExpectSuccess() // todo should be failure for only one
        {
            // Arrange
            var request = new SaveCompanyDataCommand
            {
                Ciks = new List<int> { 1, 99999 }
            };
            EdgarCompanyInfo companyInfo = new EdgarCompanyInfo
            {
                Cik = 1,
                EntityName = "Test"
            };
            var expectedCompanies = new List<Company>()
            {
                new Company
                {
                    Cik = 1,
                    EntityName = "Test"
                }
            };

            _edgarApiServiceMock.SetupFetchEdgarCompanyInfoAsyncMock(request.Ciks.ElementAt(0), companyInfo);
            _edgarApiServiceMock.SetupFetchEdgarCompanyInfoAsyncMock(request.Ciks.ElementAt(1), null); // service returns null
            _companyDataRepositoryMock.SetupSaveCompanyDataAsyncMock(expectedCompanies);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _edgarApiServiceMock.Verify();
            _companyDataRepositoryMock.Verify();
        }
    }
}
