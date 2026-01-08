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

        [HttpGet]
        public IActionResult GetFiltered([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            return Ok(_repo.GetFiltered(name, categoryId, minPrice, maxPrice));
        }

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
