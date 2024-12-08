using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fora.Challenge.Persistence.Repositories
{
    public class CompanyDataRepository : ICompanyDataRepository
    {
        private readonly CompanyDataDbContext _dbContext;

        /// <summary>Initializes a new instance of the <see cref="CompanyDataRepository"/> class.</summary>
        /// <param name="dbContext">The database context.</param>
        public CompanyDataRepository(CompanyDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Gets the company data asynchronous.</summary>
        /// <param name="firstLetter">First letter filter.</param>
        /// <returns>Company data.</returns>
        public async Task<List<Company>> GetCompanyDataAsync(string firstLetter)
        {
            var query = _dbContext.Companies
                .Include(c => c.NetIncomeLossData)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstLetter))
                query = query.Where(c => c.EntityName.StartsWith(firstLetter));

            return await query.ToListAsync();
        }

        /// <summary>Saves the company data asynchronous.</summary>
        /// <param name="companies">The companies.</param>
        public async Task SaveCompanyDataAsync(IEnumerable<Company> companies)
        {
            foreach (var company in companies)
            {
                // check if record already exists
                var existingCompany = await _dbContext.Companies
                    .Include(c => c.NetIncomeLossData)
                    .FirstOrDefaultAsync(c => c.Cik == company.Cik);

                if (existingCompany != null)
                {
                    // if company already exists update the data
                    existingCompany.EntityName = company.EntityName;

                    _dbContext.NetIncomeLossData.RemoveRange(existingCompany.NetIncomeLossData);
                    existingCompany.NetIncomeLossData = company.NetIncomeLossData;
                }
                else
                {
                    // else add it
                    _dbContext.Companies.Add(company);
                }

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
