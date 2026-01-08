using ProductApi.Models;

namespace ProductApi.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        private readonly List<Category> _categories;

        public InMemoryProductRepository ()
        {
            _categories = new()
            {
                new Category { Id = 1, Name = "Tools", Description = "All types and kinds of tools" },
                new Category { Id = 2, Name = "Computers", Description = "Laptops and desktops, but mostly desktops" },
                new Category { Id = 3, Name = "Lights", Description = "Everything that makes places brighter" }
            };

            _products = new()
            {
                new Product { Id = 1, Name = "Pliers", Price = 20, CategoryId = 1 },
                new Product { Id = 2, Name = "Screwdriver", Price = 10, CategoryId = 1 },
                new Product { Id = 3, Name = "Alienware laptop", Price = 2500, CategoryId = 2 },
                new Product { Id = 4, Name = "Dell laptop", Price = 2000, CategoryId = 2 },
                new Product { Id = 5, Name = "HP laptop", Price = 2200, CategoryId = 2 },
                new Product { Id = 6, Name = "LED Celling mounted light", Price = 40, CategoryId = 3 }
            };
        }

        public IEnumerable<Product> GetFiltered(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return _products
                .Where(x => name == null || x.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Where(x => categoryId == null || x.CategoryId == categoryId)
                .Where(x => minPrice == null || x.Price >= minPrice)
                .Where(x => maxPrice == null || x.Price <= maxPrice);
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Product product)
        {
            var existingProduct = GetById(product.Id);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return true;
        }
    }
}
