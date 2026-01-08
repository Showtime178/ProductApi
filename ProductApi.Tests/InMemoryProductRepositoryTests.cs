using ProductApi.Repositories;
using Xunit;

public class InMemoryProductRepositoryTests
{
    [Fact]
    public void Filter_ByCategory_ReturnsOnlyMatchingProducts()
    {
        var repo = new InMemoryProductRepository();

        var result = repo.GetFiltered(null, 1, null, null);

        foreach (var r in result) 
        { 
            Assert.Equal(1, r.CategoryId); 
        }
    }
}
