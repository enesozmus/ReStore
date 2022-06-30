using Microsoft.AspNetCore.Http;
using ReStore.Application.RequestHelpers;
using System.Text.Json;

namespace ReStore.Application.Extensions;

public static class HttpExtensions
{
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData, options));
                response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
}
