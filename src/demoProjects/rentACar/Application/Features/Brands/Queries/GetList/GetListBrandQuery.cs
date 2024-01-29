using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetList
{
    public class GetListBrandQuery : IRequest<BrandListModel>
    {
        public PageRequest PagedRequest;
        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;

            public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;

            }

            public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PagedRequest.Page, size: request.PagedRequest.PageSize);
                BrandListModel brandListModel = _mapper.Map<BrandListModel>(brands);

                return brandListModel;

            }
        }
    }
}
