using AutoMapper;
using Fora.Challenge.Application.Contracts.Infrastructure;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Exceptions;
using Fora.Challenge.Domain.Entities;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Commands
{
    public class SaveCompanyDataHandler : IRequestHandler<SaveCompanyDataCommand>
    {
        private readonly IEdgarApiService _edgarApiService;
        private readonly ICompanyDataRepository _companyDataRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="SaveCompanyDataHandler"/> class.</summary>
        /// <param name="edgarApiService">The edgar API service.</param>
        /// <param name="companyDataRepository">The company data repository.</param>
        /// <param name="mapper">The mapper.</param>
        public SaveCompanyDataHandler(IEdgarApiService edgarApiService, 
            ICompanyDataRepository companyDataRepository, IMapper mapper)
        {
            _edgarApiService = edgarApiService;
            _companyDataRepository = companyDataRepository;
            _mapper = mapper;
        }

        /// <summary>Handles the request to save company data.</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <exception cref="BadRequestException" />
        public async Task Handle(SaveCompanyDataCommand request, CancellationToken cancellationToken)
        {
            var companies = new List<Company>();

            if (request.Ciks.Count == 0)
                throw new BadRequestException("At least one cik is required.");

            foreach(var cik in request.Ciks)
            {
                var companyData = await _edgarApiService.FetchEdgarCompanyInfoAsync(cik);

                if (companyData == null) // todo we can make a log entry here
                    continue;

                var company = _mapper.Map<Company>(companyData);
                companies.Add(company);
            }

            await _companyDataRepository.SaveCompanyDataAsync(companies);
        }
    }
}
