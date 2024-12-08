using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Exceptions;
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
            if (request.FirstLetter != null && request.FirstLetter.Length > 1)
                throw new BadRequestException("First letter can only have one character.");

            var companyData = await _companyDataRepository.GetCompanyDataAsync(request.FirstLetter);

            return !companyData.Any()
                ? throw new NotFoundException("No companies found.")
                : _mapper.Map<List<GetCompanyDataResponse>>(companyData);
        }
    }
}
