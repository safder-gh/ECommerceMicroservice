using ecommerce.SharedLibrary.Logs;
using ecommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;
using Serilog.Core;
using System.Linq.Expressions;

namespace ProductApi.Infrastructure.Repositories;

public class ProductRepository(ProductDBContext context) : IProductRepository
{
    public async Task<Response> CreateAsync(Product entity)
    {
        try
        {
            var existing = await GetByAsync(_ => _.Name.Equals(entity.Name));
            if (existing is not  null) {
                return new Response(false, $"{entity.Name} already exist.");
            }
            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
            return new Response(true, "Product added.");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error occured during adding new product.");
        }
    }

    public async Task<Response> DeleteAsync(Guid id)
    {
        try
        {
            var existing = await FindByIdAsync(id);
            if (existing is null)
            {
                return new Response(false, "Product not found.");
            }

            context.Products.Remove(existing);
            await context.SaveChangesAsync();
            return new Response(true, "Product removed.");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error occured during removing product.");
        }
    }

    public async Task<Product?> FindByIdAsync(Guid id)
    {
        try
        {
            var existing = await context.Products.FindAsync(id);
            return existing;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return null;
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetByAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            var existing = await context.Products.Where(predicate).FirstOrDefaultAsync();
            if (existing is null)
            {
                return null;
            }
            return existing;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return null;
        }
    }

    public async Task<Response> UpdateAsync(Product entity)
    {
        try
        {
            var existing = await FindByIdAsync(entity.Id);
            if (existing is  null)
            {
                return new Response(false, $"{entity.Name} doesn't exist.");
            }
            context.Entry(entity).State = EntityState.Detached;
            context.Products.Update(entity);
            await context.SaveChangesAsync();
            return new Response(true, "Product updated.");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error occured during updating new product.");
        }
    }
}
