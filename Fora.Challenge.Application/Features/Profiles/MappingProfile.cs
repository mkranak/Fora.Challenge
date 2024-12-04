using AutoMapper;
using Fora.Challenge.Application.Features.Categories.Commands;
using Fora.Challenge.Application.Features.Categories.Queries.GetCategoriesList;
using Fora.Challenge.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Fora.Challenge.Application.Features.Events.Commands.CreateEvent;
using Fora.Challenge.Application.Features.Events.Commands.UpdateEvent;
using Fora.Challenge.Application.Features.Events.Queries.GetEventDetail;
using Fora.Challenge.Application.Features.Events.Queries.GetEventsList;
using Fora.Challenge.Domain.Entities;

namespace Fora.Challenge.Application.Features.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Event,EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CategoryEventListVm>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
