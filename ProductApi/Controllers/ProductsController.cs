using Microsoft.AspNetCore.Mvc;
using ProductApi.Repositories;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo; 
        }

        /// <summary> 
        /// Returns products filtered by name, category or price range. 
        /// </summary> 
        /// <param name="name">Name filter</param> 
        /// <param name="categoryId">Category ID filter</param> 
        /// <param name="minPrice">Minimum price</param> 
        /// <param name="maxPrice">Maximum price</param> 
        /// <returns>A list of products matching the filter criteria</returns>
        [HttpGet]
        public IActionResult GetFiltered([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            return Ok(_repo.GetFiltered(name, categoryId, minPrice, maxPrice));
        }

        /// <summary> 
        /// Updates an existing products name and/or price. 
        /// </summary> 
        /// <param name="id">ID of the product to update</param> 
        /// <param name="dto">The updated product values</param> 
        /// <returns>No content if product has been successfully updated or an error response</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request body");
            }

            var existingProduct = _repo.GetById(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with {id} not found");
            }

            if (dto.Price < 0)
            {
                return BadRequest("Price cannot be negative");
            }

            existingProduct.Name = dto.Name ?? existingProduct.Name;
            existingProduct.Price = dto.Price ?? existingProduct.Price;

            _repo.Update(existingProduct);

            return NoContent();
        }
    }
}
