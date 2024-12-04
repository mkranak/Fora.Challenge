using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Domain.Entities;
using MediatR;

namespace Fora.Challenge.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await _eventRepository.GetAllAsync()).OrderBy(e => e.Date);
            return _mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}
