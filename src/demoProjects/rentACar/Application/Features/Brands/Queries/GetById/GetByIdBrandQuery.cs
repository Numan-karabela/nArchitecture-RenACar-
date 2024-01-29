using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetById
{
    public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
    {
        public int id { get; set; }


        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IBrandRepository _repository;
            private readonly BrandBusinessRules _businessRules;
            public GetByIdBrandQueryHandler(IMapper Mapper, IBrandRepository repository, BrandBusinessRules businessRules)
            {
                _mapper = Mapper;
                _repository = repository;
                _businessRules = businessRules;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                Brand brand = await _repository.GetAsync(p => p.Id == request.id);
                _businessRules.BrandShouldExistWhenRequested(brand);
                BrandGetByIdDto brandGetById = _mapper.Map<BrandGetByIdDto>(brand);
                return brandGetById;


            }
        }

    }
}
