using Fora.Challenge.Application.Models;
using Fora.Challenge.Application.Resolvers;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.UnitTests.Resolvers
{
    public class NetIncomeLossDataResolverTests
    {
        private readonly NetIncomeLossDataResolver _resolver;

        public NetIncomeLossDataResolverTests()
        {
            _resolver = new NetIncomeLossDataResolver();
        }

        [Fact]
        public void OnResolve_WhenDataMeetsAllConditions_ExpectData()
        {
            // Arrange
            const string form = "10-K";
            const string frame = "CY2022";
            const decimal val = 1000000;
            var companyInfo = ConstructEdgarCompanyInfoStub(form, frame, val);
            var destination = new Company();

            // Act
            var actual = _resolver.Resolve(companyInfo, destination, null, null);

            // Assert
            Assert.Single(actual);
            Assert.Contains(actual, d => d.Form == "10-K");
            Assert.Contains(actual, d => d.Frame == "CY2022");
            Assert.Contains(actual, d => d.Val == 1000000);
        }

        [Fact]
        public void OnResolve_WhenStructureIsNull_ExpectNoData()
        {
            // Arrange
            var companyInfo = default(EdgarCompanyInfo); // null
            var destination = new Company();

            // Act
            var actual = _resolver.Resolve(companyInfo, destination, null, null);

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void OnResolve_WhenDataHasInvalidForm_ExpectNoData()
        {
            // Arrange
            const string form = "10-JK"; // invalid
            const string frame = "CY2022";
            const decimal val = 1000000;
            var companyInfo = ConstructEdgarCompanyInfoStub(form, frame, val);
            var destination = new Company();

            // Act
            var actual = _resolver.Resolve(companyInfo, destination, null, null);

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void OnResolve_WhenDataHasNullFrame_ExpectNoData()
        {
            // Arrange
            const string form = "10-K";
            const string frame = null; // null
            const decimal val = 1000000;
            var companyInfo = ConstructEdgarCompanyInfoStub(form, frame, val);
            var destination = new Company();

            // Act
            var actual = _resolver.Resolve(companyInfo, destination, null, null);

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void OnResolve_WhenDataHasInvalidFrame_ExpectNoData()
        {
            // Arrange
            const string form = "10-K";
            const string frame = "CY2022Q4"; // invalid
            const decimal val = 1000000;
            var companyInfo = ConstructEdgarCompanyInfoStub(form, frame, val);
            var destination = new Company();

            // Act
            var actual = _resolver.Resolve(companyInfo, destination, null, null);

            // Assert
            Assert.Empty(actual);
        }

        private EdgarCompanyInfo ConstructEdgarCompanyInfoStub(string form, string frame, decimal val)
        {
            return new EdgarCompanyInfo
            {
                Facts = new EdgarCompanyInfo.InfoFact
                {
                    UsGaap = new EdgarCompanyInfo.InfoFactUsGaap
                    {
                        NetIncomeLoss = new EdgarCompanyInfo.InfoFactUsGaapNetIncomeLoss
                        {
                            Units = new EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnits
                            {
                                Usd = new[]
                                {
                                    new EdgarCompanyInfo.InfoFactUsGaapIncomeLossUnitsUsd
                                    {
                                        Form = form, Frame = frame, Val = val
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
