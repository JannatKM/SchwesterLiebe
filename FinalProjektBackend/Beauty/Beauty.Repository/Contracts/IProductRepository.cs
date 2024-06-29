using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetLastFourProductsAsync();

        Task<Product> GetProductAsync(int productId);

        Task CreateProductAsync(Product product);

        void DeleteProduct(Product product);

        Task SaveAsync();
    }
}
