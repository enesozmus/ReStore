using ReStore.Domain.Entities;

namespace ReStore.Application.Extensions;

public static class ProductExtensions
{
        #region Alfabetik ve Fiyata Göre Sıralama

        public static IQueryable<Product> Sort(this IQueryable<Product> query, string? orderBy)
        {
                if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(p => p.Name);

                query = orderBy switch
                {
                        "price" => query.OrderBy(p => p.Price),
                        "priceDesc" => query.OrderByDescending(p => p.Price),
                        _ => query.OrderBy(p => p.Name)
                };

                return query;
        }

        #endregion

        #region Ürünler İçinde Arama

        public static IQueryable<Product> Search(this IQueryable<Product> query, string? searchTerm)
        {
                if (string.IsNullOrEmpty(searchTerm)) return query;

                var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

                return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        #endregion

        #region Filtreleme

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string? brands, string? colors)
        {
                var brandList = new List<string>();
                var colorList = new List<string>();


                if (!string.IsNullOrEmpty(brands))
                        brandList.AddRange(brands.ToLower().Split(",").ToList());


                if (!string.IsNullOrEmpty(colors))
                        colorList.AddRange(colors.ToLower().Split(",").ToList());


                query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
                query = query.Where(p => colorList.Count == 0 || colorList.Contains(p.Color.Name.ToLower()));

                return query;
        }

        #endregion

}
