using SiteMapGenerator.Data;
using SiteMapGenerator.Interfaces;
using SiteMapGenerator.Models;

namespace SiteMapGenerator.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product GetProduct(string productId)
        {
            return _context.Products.FirstOrDefault(e => e.Id.ToString() == productId);
        }

        public IEnumerable<string> GetProductsId()
        {
            return _context.Products.Select(e=>e.Id.ToString());
        }
    }
}
