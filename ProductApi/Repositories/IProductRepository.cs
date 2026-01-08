using ProductApi.Models;

namespace ProductApi.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetFiltered(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice); 
        Product? GetById(int id); 
        bool Update(Product product);
    }
}
