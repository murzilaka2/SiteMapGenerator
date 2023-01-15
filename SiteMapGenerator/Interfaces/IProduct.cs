using SiteMapGenerator.Models;

namespace SiteMapGenerator.Interfaces
{
    public interface IProduct
    {
        Product GetProduct(string productId);
        IEnumerable<string> GetProductsId();
        void AddProduct(Product product); 
    }
}
