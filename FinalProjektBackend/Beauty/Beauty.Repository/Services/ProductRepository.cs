using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products
                .SingleOrDefaultAsync(x => x.Id.Equals(productId));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLastFourProductsAsync()
        {
            return await _context.Products.Take(4).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
