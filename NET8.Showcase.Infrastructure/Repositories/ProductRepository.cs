using Microsoft.EntityFrameworkCore;
using NET8.Showcase.Domain.Entities;
using NET8.Showcase.Infrastructure.Context;
using NET8.Showcase.Services.Contracts.IRepositories;

namespace NET8.Showcase.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ShowcaseContext _context;
        
        public ProductRepository(ShowcaseContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
