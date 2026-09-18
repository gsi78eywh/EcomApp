using LusiTrack.Models;

namespace LusiTrack.Services.Interfaces;

public interface ICoffeeCatalogService
{
    IEnumerable<CoffeeProduct> GetAllProducts();
    IEnumerable<CoffeeProduct> GetFeaturedProducts();
    IEnumerable<CoffeeProduct> GetProductsByCategory(string category);
    CoffeeProduct? GetProductById(int id);
    IEnumerable<string> GetCategories();
}
