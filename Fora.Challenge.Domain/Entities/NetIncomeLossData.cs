namespace Fora.Challenge.Domain.Entities
{
    public class NetIncomeLossData
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <remarks>Primary key</remarks>
        public int Id { get; set; }

        /// <summary>Gets or sets the form.</summary>
        /// <remarks>10-K</remarks>
        public string Form { get; set; } = string.Empty;

        /// <summary>Gets or sets the frame.</summary>
        /// <value>CYYYY</value>
        public string Frame { get; set; } = string.Empty;

        /// <summary>Gets or sets the income loss amount.</summary>
        public decimal Val { get; set; }

        /// <summary>Gets or sets the company identifier.</summary>
        /// <value>Foreign key</value>
        public int CompanyId { get; set; }

        /// <summary>Gets or sets the company.</summary>
        /// <value>Navigational property</value>
        public Company Company { get; set; }
    }
}
