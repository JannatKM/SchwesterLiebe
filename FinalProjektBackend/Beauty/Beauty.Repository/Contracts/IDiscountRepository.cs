using Beauty.Entity.Entities;

namespace Beauty.Repository.Contracts
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetDiscountsAsync();

        Task<Discount> GetDiscountAsync(int discountId);

        Task CreateDiscountAsync(Discount discount);

        void DeleteDiscount(Discount discount);

        Task SaveAsync();
    }
}
