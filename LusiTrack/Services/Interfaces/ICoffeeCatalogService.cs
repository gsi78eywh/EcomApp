using LusiTrack.Models;

namespace LusiTrack.Services.Interfaces;

public interface ICoffeeCatalogService
{
    IEnumerable<CoffeeProduct> GetAllProducts();
    IEnumerable<CoffeeProduct> GetFeaturedProducts();
    IEnumerable<CoffeeProduct> GetBestSellers(int count = 8);
    IEnumerable<CoffeeProduct> GetProductsByCategory(string category);
    CoffeeProduct? GetProductById(int id);
    IEnumerable<string> GetCategories();
    IEnumerable<CoffeeProduct> SearchAndFilter(string? query, string? category, string? roastLevel, decimal? minPrice, decimal? maxPrice, string? sortBy);
    bool UpdateStock(int productId, int quantityChange);
    bool ToggleAvailability(int productId);
    void SaveProduct(CoffeeProduct product);
}

