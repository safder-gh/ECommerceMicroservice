using OrderApi.Application.DTOs;
using ProductApi.Domain.Entities;

namespace OrderApi.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDetailDTO> GetOrderDetailAsync(Guid OrderId);
        Task<IEnumerable<OrderDTO>> GetOrdersByClientAsync(Guid ClientId);
    }
}