using AutoMapper;
using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Domain.Entities;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Commands
{
    public class SaveCompanyDataHandler : IRequestHandler<SaveCompanyDataCommand>
    {
        private readonly IEdgarApiService _edgarApiService;
        private readonly ICompanyDataRepository _companyDataRepository;
        private readonly IMapper _mapper;

        public SaveCompanyDataHandler(IEdgarApiService edgarApiService, 
            ICompanyDataRepository companyDataRepository, IMapper mapper)
        {
            _edgarApiService = edgarApiService;
            _companyDataRepository = companyDataRepository;
            _mapper = mapper;
        }

        public async Task Handle(SaveCompanyDataCommand request, CancellationToken cancellationToken)
        {
            var companies = new List<Company>();

            foreach(var cik in request.Ciks)
            {
                var companyData = await _edgarApiService.FetchEdgarCompanyInfoAsync(cik);

                if (companyData != null)
                {
                    // Use AutoMapper to map the API response to the domain object
                    var company = _mapper.Map<Company>(companyData);
                    companies.Add(company);
                }
            }

            await _companyDataRepository.SaveEdgarCompanyInfoAsync(companies);

        }
    }
}
