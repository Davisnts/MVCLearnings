using System.Text.Json;
using fullstack.API.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace fullstack.API.Extensions
{
    public static class Pagination
    {
        public static void AddPagination(this HttpResponse response,int currentPage,int itemsPerPage,int totalItems,int totalPages)
        {
            var pagination = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            var options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            //Sintax Nova
            response.Headers["Pagination"] = JsonSerializer.Serialize(pagination,options
            );
            //Sintax Antiga
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}