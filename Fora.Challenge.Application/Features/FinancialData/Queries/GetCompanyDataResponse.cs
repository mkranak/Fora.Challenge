namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StandardFundableAmount { get; set; }
        public decimal SpecialFundableAmount { get; set; }
    }
}
