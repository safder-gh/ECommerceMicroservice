using ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Application.DTOs;
using OrderApi.Application.Services;
using ProductApi.Application.DTOs.Conversion;
using ProductApi.Application.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderRepository repository,IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll() {
            var entities = await repository.GetAllAsync();
            if (!entities.Any())  return NotFound("No Order found.");
            var (_, list) = OrderConversion.FromEntity(null!, entities);
            return list!.Any() ? Ok(list) : NotFound("No Order found.");
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Get(Guid id)
        {
            var entity = await repository.FindByIdAsync(id);
            if (entity is null) return NotFound("No Order found.");
            var (dto, _) = OrderConversion.FromEntity(entity,null!);
            return  Ok(dto);
        }
        [HttpGet("customer/{id:guid}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllByCustomer(Guid Id)
        {
            var entities = await repository.GetOrdersAsync(o=>o.CustomerId == Id);
            if (!entities.Any()) return NotFound("No Order found.");
            var (_, list) = OrderConversion.FromEntity(null!, entities);
            return list!.Any() ? Ok(list) : NotFound("No Order found.");
        }

        [HttpGet("details/{id:guid}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetails(Guid Id)
        {
            var entity = await orderService.GetOrderDetailAsync( Id);
            return entity is null ? NotFound() : entity;
        }
        [HttpPost]
        public async Task<ActionResult<Response>> Add(OrderDTO dto) {
            dto.Id = Guid.CreateVersion7();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = OrderConversion.ToEntity(dto);
            var response = await repository.CreateAsync(entity);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Update(OrderDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = OrderConversion.ToEntity(dto);
            var response = await repository.UpdateAsync(entity);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> Delete(OrderDTO dto)
        {
           var entity = OrderConversion.ToEntity(dto);
            var response = await repository.DeleteAsync(entity.Id);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }
    }
}
