using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fora.Challenge.Persistence.Repositories
{
    public class CompanyDataRepository : ICompanyDataRepository
    {
        private readonly CompanyDataDbContext _dbContext;

        public CompanyDataRepository(CompanyDataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Company>> GetEdgarCompanyInfoAsync(string startsWith)
        {
            return await _dbContext.Companies
                .Where(c => startsWith == null || c.EntityName.StartsWith(startsWith))
                .Include(c => c.NetIncomeLossData)
                .ToListAsync();
        }

        public async Task SaveEdgarCompanyInfoAsync(IEnumerable<Company> companies)
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
