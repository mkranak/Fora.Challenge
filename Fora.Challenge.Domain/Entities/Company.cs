namespace Fora.Challenge.Domain.Entities
{
    public class Company
    {
        /// <summary>Gets or sets the identifier.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the cik.</summary>
        public int Cik { get; set; }

        /// <summary>Gets or sets the name of the entity.</summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>Gets or sets the net income loss data.</summary>
        public ICollection<NetIncomeLossData> NetIncomeLossData { get; set; } = new List<NetIncomeLossData>();
    }
}
