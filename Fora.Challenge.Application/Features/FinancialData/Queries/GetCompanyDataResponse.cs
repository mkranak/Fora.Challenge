namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataResponse
    {
        /// <summary>Gets or sets the identifier.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the standard fundable amount.</summary>
        public decimal StandardFundableAmount { get; set; }

        /// <summary>Gets or sets the special fundable amount.</summary>
        public decimal SpecialFundableAmount { get; set; }
    }
}
