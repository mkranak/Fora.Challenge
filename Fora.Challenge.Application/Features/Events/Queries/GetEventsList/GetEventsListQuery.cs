using MediatR;

namespace Fora.Challenge.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery : IRequest<List<EventListVm>>
    {
    }
}
