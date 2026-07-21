using ecommerce.SharedLibrary.Logs;
using ecommerce.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using OrderApi.Infrastructure.Data;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System.Linq.Expressions;

namespace OrderApi.Infrastructure.Repositories;

public class OrderRepository(OrderDBContext context) : IOrderRepository
{
    public async Task<Response> CreateAsync(Order entity)
    {
        try
        {
            var existing = await GetByAsync(_ => _.Id.Equals(entity.Id));
            if (existing is not  null) {
                return new Response(false, $"Order already exist.");
            }
            await context.Orders.AddAsync(entity);
            await context.SaveChangesAsync();
            return new Response(true, "Order added.");
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
                return new Response(false, "Order not found.");
            }

            context.Orders.Remove(existing);
            await context.SaveChangesAsync();
            return new Response(true, "Order removed.");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error occured during removing order.");
        }
    }

    public async Task<Order?> FindByIdAsync(Guid id)
    {
        try
        {
            var existing = await context.Orders.FindAsync(id);
            return existing;
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return null;
        }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<Order?> GetByAsync(Expression<Func<Order, bool>> predicate)
    {
        try
        {
            var existing = await context.Orders.Where(predicate).FirstOrDefaultAsync();
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

    public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate)
    {
        return  await context.Orders.Where(predicate).ToListAsync();
    }

    public async Task<Response> UpdateAsync(Order entity)
    {
        try
        {
            var existing = await FindByIdAsync(entity.Id);
            if (existing is  null)
            {
                return new Response(false, "Order doesn't exist.");
            }
            context.Entry(entity).State = EntityState.Detached;
            context.Orders.Update(entity);
            await context.SaveChangesAsync();
            return new Response(true, "Order updated.");
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            return new Response(false, "Error occured during updating order.");
        }
    }
}
