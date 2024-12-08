using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Domain.Entities;
using Moq;

namespace Fora.Challenge.Application.UnitTests.Mock
{
    public static class CompanyDataRepositoryMockExtensions
    {
        public static void SetupGetCompanyDataAsyncMock(
            this Mock<ICompanyDataRepository> repositoryMock, string filter, List<Company> companies)
        {
            repositoryMock.Setup(exp => exp.GetCompanyDataAsync(filter))
                .ReturnsAsync(companies)
                .Verifiable();
        }

        public static void SetupSaveCompanyDataAsyncMock(
            this Mock<ICompanyDataRepository> repositoryMock, IEnumerable<Company> companies)
        {
            repositoryMock.Setup(exp => exp.SaveCompanyDataAsync(
                It.Is<IEnumerable<Company>>(c => 
                    c.Count() == companies.Count() &&
                    c.All(ac => companies.Any(ec => AreCompaniesEqual(ac, ec)))
                )))
                .Returns(Task.CompletedTask)
                .Verifiable();
        }

        private static bool AreCompaniesEqual(Company actual, Company expected)
        {
            return actual.EntityName == expected.EntityName &&
                actual.Id == expected.Id;
        }
    }
}
