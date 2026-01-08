using Microsoft.AspNetCore.Mvc;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Repositories;
using Xunit;

public class ProductsControllerTests
{
    [Fact]
    public void Update_ReturnsNotFound_WhenProductDoesNotExist()
    {
        var repo = new InMemoryProductRepository();
        var controller = new ProductsController(repo);

        var dto = new UpdateProductDto { Name = "Test" };

        var result = controller.Update(999, dto);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
