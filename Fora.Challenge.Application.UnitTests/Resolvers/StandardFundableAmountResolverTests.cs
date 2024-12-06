using Fora.Challenge.Application.Features.FinancialData.Queries;
using Fora.Challenge.Application.Resolvers;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.UnitTests.Resolvers
{
    public class StandardFundableAmountResolverTests
    {
        private readonly StandardFundableAmountResolver _resolver;

        public StandardFundableAmountResolverTests()
        {
            _resolver = new StandardFundableAmountResolver();
        }

        [Fact]
        public void OnResolve_WhenNotAllYearsPresent_ExpectZero()
        {
            // Arrange
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = 3000000 },
                    new() { Frame = "CY2021", Val = 4000000 }
                    // Missing 2022
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(0, result); // Expect zero when not all years are present
        }

        [Fact]
        public void OnResolve_WhenIncomeIn2021IsNotPositive_ExpectZero()
        {
            // Arrange
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = 3000000 },
                    new() { Frame = "CY2021", Val = -1000 }, // Negative income
                    new() { Frame = "CY2022", Val = 5000000 }
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(0, result); // Expect zero when income in 2021 or 2022 is non-positive
        }

        [Fact]
        public void OnResolve_WhenIncomeIn2022IsNotPositive_ExpectZero()
        {
            // Arrange
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = 3000000 },
                    new() { Frame = "CY2021", Val = 4000000 }, 
                    new() { Frame = "CY2022", Val = -1000 } // Negative income
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(0, result); // Expect zero when income in 2021 or 2022 is non-positive
        }

        [Fact]
        public void OnResolve_WhenIncomeIsBelowTenBillion_ExpectCorrectAmount()
        {
            // Arrange
            var highVal = 9000000000; // 9 billion
            var percentage = 0.2151m; // 21.51%
            var expectedAmount = highVal * percentage;
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = highVal },
                    new() { Frame = "CY2021", Val = 4000000 },
                    new() { Frame = "CY2022", Val = 5000000 }
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(expectedAmount, result);
        }

        [Fact]
        public void OnResolve_WhenIncomeIsTenBillion_ExpectCorrectAmount()
        {
            // Arrange
            var highVal = 10000000000; // 10 billion
            var percentage = 0.1233m; // 12.33%
            var expectedAmount = highVal * percentage;
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = highVal },
                    new() { Frame = "CY2021", Val = 4000000 },
                    new() { Frame = "CY2022", Val = 5000000 }
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(expectedAmount, result);
        }

        [Fact]
        public void OnResolve_WhenIncomeIsAboveTenBillion_ExpectCorrectAmount()
        {
            // Arrange
            var highVal = 20000000000; // 20 billion
            var percentage = 0.1233m; // 12.33%
            var expectedAmount = highVal * percentage;
            var company = new Company
            {
                NetIncomeLossData = new List<NetIncomeLossData>
                {
                    new() { Frame = "CY2018", Val = 1000000 },
                    new() { Frame = "CY2019", Val = 2000000 },
                    new() { Frame = "CY2020", Val = highVal },
                    new() { Frame = "CY2021", Val = 4000000 },
                    new() { Frame = "CY2022", Val = 5000000 }
                }
            };
            var destination = new GetCompanyDataResponse();

            // Act
            var result = _resolver.Resolve(company, destination, 0, null);

            // Assert
            Assert.Equal(expectedAmount, result);
        }
    }
}
