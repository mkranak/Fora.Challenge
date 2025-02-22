﻿using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Exceptions;
using MediatR;

namespace Fora.Challenge.Application.Features.FinancialData.Queries
{
    public class GetCompanyDataHandler : IRequestHandler<GetCompanyDataQuery, List<GetCompanyDataResponse>>
    {
        private readonly ICompanyDataRepository _companyDataRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="GetCompanyDataHandler"/> class.</summary>
        /// <param name="companyDataRepository">The company data repository.</param>
        /// <param name="mapper">The mapper.</param>
        public GetCompanyDataHandler(ICompanyDataRepository companyDataRepository, IMapper mapper)
        {
            _companyDataRepository = companyDataRepository;
            _mapper = mapper;
        }

        /// <summary>Handles the request to get company data.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The requested company data.</returns>
        /// <exception cref="BadRequestException" />
        /// <exception cref="NotFoundException" />
        public async Task<List<GetCompanyDataResponse>> Handle(GetCompanyDataQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Filter) && (request.Filter.Length != 1 ||
                !char.IsLetter(request.Filter[0])))
                throw new BadRequestException("Filter can only be one letter.");

            var companyData = await _companyDataRepository.GetCompanyDataAsync(request.Filter);

            return !companyData.Any()
                ? throw new NotFoundException("No companies found.")
                : _mapper.Map<List<GetCompanyDataResponse>>(companyData);
        }
    }
}
