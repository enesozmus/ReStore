using MediatR;
using ReStore.Application.RequestHelpers;

namespace ReStore.Application.CQRS;

public class GetProductsQueryRequest : IRequest<PagedList<GetProductsQueryResponse>>
{
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? Colors { get; set; }
        public string? Brands { get; set; }

        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
                get => _pageSize;
                set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
}
