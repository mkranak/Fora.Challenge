using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
    }
}
