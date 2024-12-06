using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Resolvers;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.UnitTests.Resolvers
{
    public class SpecialFundableAmountResolverTests
    {
        private readonly SpecialFundableAmountResolver _resolver;

        public SpecialFundableAmountResolverTests()
        {
            _resolver = new SpecialFundableAmountResolver();
        }

        [Fact]
        public void OnResolve_WhenStandardFundableAmountIsZero_ExpectZero()
        {
            // Arrange
            var company = new Company
            {
                EntityName = "Apple"
            };
            var standardAmount = 0; // zero
            var destination = new GetCompanyDataResponse
            {
                StandardFundableAmount = standardAmount
            };

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(standardAmount, result); // standard amount is the special amount
            Assert.Equal(standardAmount, destination.StandardFundableAmount);
        }

        [Fact]
        public void OnResolve_WhenNoConditionsMetAndPrecisionExceedsTwo_ExpectCorrectPrecision()
        {
            // Arrange
            var company = new Company
            {
                EntityName = "Windows",
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2021", Val = 1 },
                    new() { Frame = "CY2022", Val = 2 }
                }
            };
            var standardAmount = 100.12567m; // zero
            var destination = new GetCompanyDataResponse
            {
                StandardFundableAmount = standardAmount
            };
            var expectedAmount = decimal.Round(standardAmount, 2, MidpointRounding.AwayFromZero);

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(expectedAmount, result); // standard amount is the special amount
            Assert.Equal(expectedAmount, destination.StandardFundableAmount);
        }

        [Fact]
        public void OnResolve_WhenCompanyNameStartsWithVowel_ExpectCorrectFundableAmounts()
        {
            // Arrange
            var company = new Company
            {
                EntityName = "Apple"
            };
            var standardAmount = 1000000; // 1 million
            var destination = new GetCompanyDataResponse
            {
                StandardFundableAmount = standardAmount
            };
            var expectedStandardAmount = standardAmount + (standardAmount * 0.15m);

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(standardAmount, result); // standard amount is the special amount
            Assert.Equal(expectedStandardAmount, destination.StandardFundableAmount);
        }

        [Fact]
        public void OnResolve_WhenCompanyNameHasLessIncomeIn2022_ExpectCorrectFundableAmounts()
        {
            // Arrange
            var company = new Company
            {
                EntityName = "Windows",
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2021", Val = 2 },
                    new() { Frame = "CY2022", Val = 1 }
                }
            };
            var standardAmount = 1000000; // 1 million
            var destination = new GetCompanyDataResponse
            {
                StandardFundableAmount = standardAmount
            };
            var expectedStandardAmount = standardAmount - (standardAmount * 0.25m);

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(standardAmount, result); // standard amount is the special amount
            Assert.Equal(expectedStandardAmount, destination.StandardFundableAmount);
        }

        [Fact]
        public void OnResolve_WhenCompanyNameStartsWithVowelAndHasLessIncomeIn2022_ExpectCorrectFundableAmounts()
        {
            // Arrange
            var company = new Company
            {
                EntityName = "Apple",
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2021", Val = 2 },
                    new() { Frame = "CY2022", Val = 1 }
                }
            };
            var standardAmount = 1000000; // 1 million
            var destination = new GetCompanyDataResponse
            {
                StandardFundableAmount = standardAmount
            };
            var expectedStandardAmount = standardAmount + (standardAmount * 0.15m);
            expectedStandardAmount = expectedStandardAmount - (expectedStandardAmount * 0.25m);

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(standardAmount, result); // standard amount is the special amount
            Assert.Equal(expectedStandardAmount, destination.StandardFundableAmount);
        }
    }
}
