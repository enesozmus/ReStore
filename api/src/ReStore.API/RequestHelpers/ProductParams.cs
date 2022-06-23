namespace ReStore.API.RequestHelpers;

public class ProductParams : PaginationParams
{
        public string? OrderBy { get; set; }
        public string? SearchTerm { get; set; }
        public string? Colors { get; set; }
        public string? Brands { get; set; }
}
