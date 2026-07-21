using OrderApi.Application.DTOs;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs.Conversion;

public static  class OrderConversion
{
    public static Order ToEntity(OrderDTO dto) => new Order { 
        Id = (Guid)dto.Id!.Value, 
        CustomerId = dto.CustomerId, 
        ProductId = dto.ProductId, 
        Quantity = dto.Quantity };

    public static (OrderDTO?,IEnumerable<OrderDTO>?) FromEntity(Order? entity, IEnumerable<Order>? entities)
    {
        if(entity is not null || entities is null)
        {
            var singleEntity = new OrderDTO { Id = entity!.Id, ProductId = entity.ProductId, CustomerId = entity.CustomerId, Quantity = entity.Quantity };
            return (singleEntity, null);
        }else if (entities is not null || entity is null)
        {
            var _products = entities!.Select(p =>
                new OrderDTO { Id = p.Id, ProductId = p.ProductId, CustomerId = p.CustomerId, Quantity = p.Quantity }
            ).ToList();
            return (null, _products);
        }
        return (null, null);
    }

}
