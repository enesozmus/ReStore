namespace ReStore.API.RequestHelpers;

public class MetaData
{
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }                       // Bir sayfada kaç ürün listelensin ?
        public int TotalCount { get; set; }
}
