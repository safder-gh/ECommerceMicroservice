using ecommerce.SharedLibrary.Interfaces;
using ProductApi.Domain.Entities;
using System.Linq.Expressions;

namespace ProductApi.Application.Interfaces;

public interface IOrderRepository: IGenericRepository<Order, Guid> { 
    Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order,bool>> predicate);
}