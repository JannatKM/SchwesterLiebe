using Beauty.Entity.Entities;
using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Repository.Services
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountAsync(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
        }

        public void DeleteDiscount(Discount discount)
        {
            _context.Discounts.Remove(discount);
        }

        public async Task<Discount> GetDiscountAsync(int discountId)
        {
            return await _context.Discounts
                .SingleOrDefaultAsync(x => x.Id.Equals(discountId));
        }

        public async Task<IEnumerable<Discount>> GetDiscountsAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
