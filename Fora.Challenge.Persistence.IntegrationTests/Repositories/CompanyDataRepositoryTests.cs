using Fora.Challenge.Domain.Entities;
using Fora.Challenge.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Fora.Challenge.Persistence.IntegrationTests.Repositories
{
    public class CompanyDataRepositoryTests
    {
        private readonly CompanyDataRepository _companyDataRepository;
        private readonly CompanyDataDbContext _companyDataDbContext;

        public CompanyDataRepositoryTests() 
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CompanyDataDbContext>()
                .UseSqlite(connection)
                .Options;

            _companyDataDbContext = new CompanyDataDbContext(options);
            _companyDataDbContext.Database.EnsureCreated();

            _companyDataRepository = new CompanyDataRepository(_companyDataDbContext);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetCompanyDataAsync_WhenFilterIsNullOrEmpty_ExpectAllCompanies(string? filter)
        {
            // Arrange
            const int cikA = 123456;
            const int cikB = 654321;
            const string startsWith = null;
            var companies = new List<Company>
            {
                new Company
                {
                    Cik = cikA,
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
                    Cik = cikB,
                    EntityName = "Second Record",
                    NetIncomeLossData = new List<NetIncomeLossData>
                    {
                        new NetIncomeLossData
                        {
                            Form = "20-K",
                            Frame = "CY2022",
                            Val = 2000000
                        },
                        new NetIncomeLossData
                        {
                            Form = "20-KB",
                            Frame = "CY2022B",
                            Val = 2000001
                        }
                    }
                }
            };

            await _companyDataDbContext.Companies.AddRangeAsync(companies);
            await _companyDataDbContext.SaveChangesAsync();

            // Act
            var actual = await _companyDataRepository.GetCompanyDataAsync(startsWith);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(companies.Count, actual.Count);

            var firstRecord = actual.First(c => c.Cik == cikA);
            Assert.Equal("First Record", firstRecord.EntityName);
            Assert.Single(firstRecord.NetIncomeLossData);
            Assert.Equal("10-K", firstRecord.NetIncomeLossData.First().Form);
            Assert.Equal("CY2021", firstRecord.NetIncomeLossData.First().Frame);
            Assert.Equal(1000000, firstRecord.NetIncomeLossData.First().Val);

            var secondRecord = actual.First(c => c.Cik == cikB);
            Assert.Equal("Second Record", secondRecord.EntityName);
            Assert.Equal(2, secondRecord.NetIncomeLossData.Count);
            Assert.Equal("20-K", secondRecord.NetIncomeLossData.ElementAt(0).Form);
            Assert.Equal("CY2022", secondRecord.NetIncomeLossData.ElementAt(0).Frame);
            Assert.Equal(2000000, secondRecord.NetIncomeLossData.ElementAt(0).Val);
            Assert.Equal("20-KB", secondRecord.NetIncomeLossData.ElementAt(1).Form);
            Assert.Equal("CY2022B", secondRecord.NetIncomeLossData.ElementAt(1).Frame);
            Assert.Equal(2000001, secondRecord.NetIncomeLossData.ElementAt(1).Val);
        }

        [Fact]
        public async Task GetCompanyDataAsync_WhenFilterHasCharacter_ExpectOnlyOneCompany()
        {
            // Arrange
            const int cikA = 123456;
            const int cikB = 654321;
            const string startsWith = "F";
            var companies = new List<Company>
            {
                new Company
                {
                    Cik = cikA,
                    EntityName = "First Record"
                },
                new Company
                {
                    Cik = cikB,
                    EntityName = "Second Record"
                }
            };

            await _companyDataDbContext.Companies.AddRangeAsync(companies);
            await _companyDataDbContext.SaveChangesAsync();

            // Act
            var actual = await _companyDataRepository.GetCompanyDataAsync(startsWith);

            // Assert
            Assert.NotNull(actual);
            Assert.Single(actual);

            var record = actual.First(c => c.Cik == cikA);
            Assert.Equal("First Record", record.EntityName);
        }

        [Fact]
        public async Task SaveCompanyDataAsync_WhenCikDoesNotExist_ExpectAddedNewCompany()
        {
            // Arrange
            const int cik = 123456;
            var companies = new List<Company>
            {
                new Company
                {
                    Cik = cik,
                    EntityName = "Added Record",
                    NetIncomeLossData = new List<NetIncomeLossData> 
                    { 
                        new NetIncomeLossData 
                        { 
                            Form = "10-K", 
                            Frame = "CY2020",
                            Val = 1000000
                        } 
                    }
                }
            };
            
            // Act
            await _companyDataRepository.SaveCompanyDataAsync(companies);

            // Assert
            var savedCompany = await _companyDataDbContext.Companies
                .Include(c => c.NetIncomeLossData)
                .FirstOrDefaultAsync(c => c.Cik == cik);

            Assert.NotNull(savedCompany);
            Assert.Equal("Added Record", savedCompany.EntityName);
            Assert.Single(savedCompany.NetIncomeLossData);
            Assert.Equal("10-K", savedCompany.NetIncomeLossData.First().Form);
            Assert.Equal("CY2020", savedCompany.NetIncomeLossData.First().Frame);
            Assert.Equal(1000000, savedCompany.NetIncomeLossData.First().Val);
        }

        [Fact]
        public async Task SaveCompanyDataAsync_WhenMultipleCompanies_ExpectAllSaved()
        {
            // Arrange
            const int cikA = 123456;
            const int cikB = 654321;
            var companies = new List<Company>
            {
                new Company
                {
                    Cik = cikA,
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
                    Cik = cikB,
                    EntityName = "Second Record",
                    NetIncomeLossData = new List<NetIncomeLossData>
                    {
                        new NetIncomeLossData
                        {
                            Form = "20-K",
                            Frame = "CY2022",
                            Val = 2000000
                        },
                        new NetIncomeLossData
                        {
                            Form = "20-KB",
                            Frame = "CY2022B",
                            Val = 2000001
                        }
                    }
                }
            };

            // Act
            await _companyDataRepository.SaveCompanyDataAsync(companies);

            // Assert
            var savedCompanies = await _companyDataDbContext.Companies
                .Include(c => c.NetIncomeLossData)
                .ToListAsync();

            Assert.NotNull(savedCompanies);
            Assert.Equal(2, savedCompanies.Count);

            var firstRecord = savedCompanies.First(c => c.Cik == cikA);
            Assert.Equal("First Record", firstRecord.EntityName);
            Assert.Single(firstRecord.NetIncomeLossData);
            Assert.Equal("10-K", firstRecord.NetIncomeLossData.First().Form);
            Assert.Equal("CY2021", firstRecord.NetIncomeLossData.First().Frame);
            Assert.Equal(1000000, firstRecord.NetIncomeLossData.First().Val);

            var secondRecord = savedCompanies.First(c => c.Cik == cikB);
            Assert.Equal("Second Record", secondRecord.EntityName);
            Assert.Equal(2, secondRecord.NetIncomeLossData.Count);
            Assert.Equal("20-K", secondRecord.NetIncomeLossData.ElementAt(0).Form);
            Assert.Equal("CY2022", secondRecord.NetIncomeLossData.ElementAt(0).Frame);
            Assert.Equal(2000000, secondRecord.NetIncomeLossData.ElementAt(0).Val);
            Assert.Equal("20-KB", secondRecord.NetIncomeLossData.ElementAt(1).Form);
            Assert.Equal("CY2022B", secondRecord.NetIncomeLossData.ElementAt(1).Frame);
            Assert.Equal(2000001, secondRecord.NetIncomeLossData.ElementAt(1).Val);
        }

        [Fact]
        public async Task SaveCompanyDataAsync_WhenCikAlreadyExist_ExpectUpdatedCompany()
        {
            // Arrange
            const int cik = 123456;
            var companies = new List<Company>
            {
                new Company
                {
                    Cik = cik,
                    EntityName = "Updated Record",
                    NetIncomeLossData = new List<NetIncomeLossData>
                    {
                        new NetIncomeLossData
                        {
                            Form = "10-KU",
                            Frame = "CY2020U",
                            Val = 5000000
                        }
                    }
                }
            };

            // add the existing record
            _companyDataDbContext.Companies.Add(new Company
            {
                Cik = cik,
                EntityName = "Existing Record",
                NetIncomeLossData = new List<NetIncomeLossData>()
                {
                    new NetIncomeLossData
                        {
                            Form = "10-K",
                            Frame = "CY2020",
                            Val = 1000000
                        }
                }
            });
            await _companyDataDbContext.SaveChangesAsync();

            // Act
            await _companyDataRepository.SaveCompanyDataAsync(companies);

            // Assert
            var savedCompany = await _companyDataDbContext.Companies
                .Include(c => c.NetIncomeLossData)
                .FirstOrDefaultAsync(c => c.Cik == cik);

            Assert.NotNull(savedCompany);
            Assert.Equal("Updated Record", savedCompany.EntityName);
            Assert.Single(savedCompany.NetIncomeLossData);
            Assert.Equal("10-KU", savedCompany.NetIncomeLossData.First().Form);
            Assert.Equal("CY2020U", savedCompany.NetIncomeLossData.First().Frame);
            Assert.Equal(5000000, savedCompany.NetIncomeLossData.First().Val);
        }

        [Fact]
        public async Task SaveCompanyDataAsync_WhenEmptyListOfCompanies_ExpectNothingAdded()
        {
            // Arrange
            var companies = new List<Company>();

            // Act
            await _companyDataRepository.SaveCompanyDataAsync(companies);

            // Assert
            var savedCompany = await _companyDataDbContext.Companies
                .Include(c => c.NetIncomeLossData)
                .ToListAsync();

            Assert.Empty(savedCompany);
        }
    }
}
