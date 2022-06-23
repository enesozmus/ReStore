using AutoMapper;
using MediatR;
using ReStore.Application.Extensions;
using ReStore.Application.IRepositories;
using ReStore.Application.RequestHelpers;
using ReStore.Domain.Entities;

namespace ReStore.Application.CQRS;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, PagedList<GetProductsQueryResponse>>
{
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductReadRepository productReadRepository, IMapper mapper)
        {
                _productReadRepository = productReadRepository;
                _mapper = mapper;
        }
        public async Task<PagedList<GetProductsQueryResponse>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
                var productList =  _productReadRepository.GetAllProductsForIndex();

                var query = productList
                            .Sort(request.OrderBy)
                            .Search(request.SearchTerm)
                            .Filter(request.Brands, request.Colors)
                            .AsQueryable();

                //var queryProduct = _mapper.Map<IQueryable<GetProductsQueryResponse>>(query);

                var products = await PagedList<Product>.ToPagedList(query, request.PageNumber, request.PageSize);

                var mappedProduct = _mapper.Map<PagedList<GetProductsQueryResponse>>(products);

                return mappedProduct;
        }
}
