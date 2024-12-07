using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Models;
using Moq;

namespace Fora.Challenge.Application.UnitTests.Mock
{
    public static class EdgerApiServiceMockExtensions
    {
        public static void SetupFetchEdgarCompanyInfoAsyncMock(
            this Mock<IEdgarApiService> serviceMock, int cik, EdgarCompanyInfo companyInfo)
        {
            serviceMock.Setup(exp => exp.FetchEdgarCompanyInfoAsync(cik))
                .ReturnsAsync(companyInfo)
                .Verifiable();
        }

        public static void ValidateFetchEdgarCompanyInfoAsyncMock(
            this Mock<IEdgarApiService> serviceMock, int timesCalled)
        {
            serviceMock.Verify(exp => exp.FetchEdgarCompanyInfoAsync(It.IsAny<int>()),
                Times.Exactly(timesCalled));
        }
    }
}
