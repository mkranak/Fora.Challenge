namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataResponse
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the standard fundable amount.</summary>
        /// <value>The standard fundable amount.</value>
        public decimal StandardFundableAmount { get; set; }

        /// <summary>Gets or sets the special fundable amount.</summary>
        /// <value>The special fundable amount.</value>
        public decimal SpecialFundableAmount { get; set; }
    }
}
