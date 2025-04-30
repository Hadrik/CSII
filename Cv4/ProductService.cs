using Cv4.Models;

namespace Cv4;

public class ProductService
{
    public List<Product> List()
    {
        return Product.GetProducts();
    }

    public Product? GetProduct(int id)
    {
        return Product.GetProducts().FirstOrDefault(p => p.Id == id);
    }
}