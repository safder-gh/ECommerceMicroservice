using Microsoft.VisualBasic;
using OrderApi.Application.Constants;
using OrderApi.Application.DTOs;
using Polly;
using Polly.Registry;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Conversion;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Services;

public class OrderService(IOrderRepository repository, HttpClient httpClient,ResiliencePipelineProvider<string> resiliencePipeline) : IOrderService
{
    public async Task<ProductDTO> GetProduct(Guid Id)
    {
        var entity = await httpClient.GetAsync($"/api/product/{Id}");
        if (!entity.IsSuccessStatusCode) return null!;
        var dto = await entity.Content.ReadFromJsonAsync<ProductDTO>();
        return dto!;
    }

    public async Task<AppUserDTO> GetUser(Guid Id)
    {
        var entity = await httpClient.GetAsync($"/api/product/{Id}");
        if (!entity.IsSuccessStatusCode) return null!;
        var dto = await entity.Content.ReadFromJsonAsync<AppUserDTO>();
        return dto!;
    }

    public async Task<OrderDetailDTO> GetOrderDetailAsync(Guid Id)
    {
        var entity = await repository.FindByIdAsync(Id);
        if (entity is null) return null!;
        var pipeline = resiliencePipeline.GetPipeline(ApplicationConstants.PIPELINE);
        var productDto = await pipeline.ExecuteAsync(async token=> await GetProduct(entity.ProductId));
        var customer = await pipeline.ExecuteAsync(async token => await GetUser(entity.CustomerId));
        return new OrderDetailDTO(
            entity.Id,
            productDto.Id,
            customer.Id,
            customer.email,
            customer.CellNumber,
            productDto.Name,
            entity.Quantity,
            productDto.Price,
            productDto.Price* entity.Quantity,
            entity.CreatedOn
            );
    }
    public async Task<IEnumerable<OrderDTO>> GetOrdersByClientAsync(Guid Id)
    {
        var entities = await repository.GetOrdersAsync(o => o.CustomerId == Id);
        if (!entities.Any()) return null!;
        var (_, dtos) = OrderConversion.FromEntity(null!,entities);
        return dtos!;
    }
    
}
