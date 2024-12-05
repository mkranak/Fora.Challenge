using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataHandler : IRequestHandler<GetCompanyDataQuery, List<GetCompanyDataResponse>>
    {
        private readonly ICompanyDataRepository _companyDataRepository;
        private readonly IMapper _mapper;

        public GetCompanyDataHandler(ICompanyDataRepository companyDataRepository, IMapper mapper)
        {
            _companyDataRepository = companyDataRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCompanyDataResponse>> Handle(GetCompanyDataQuery request, CancellationToken cancellationToken)
        {
            var companyData = await _companyDataRepository.GetEdgarCompanyInfoAsync(request.StartsWith);

            var result = companyData.Select(c =>
            {
                return _mapper.Map<List<GetCompanyDataResponse>>(companyData);
            });



            return _mapper.Map<List<GetCompanyDataResponse>>(companyData);
        }
    }
}
