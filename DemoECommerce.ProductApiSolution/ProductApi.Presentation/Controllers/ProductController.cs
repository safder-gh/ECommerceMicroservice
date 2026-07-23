using ecommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Conversion;
using ProductApi.Application.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController(IProductRepository repository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll() {
            var entities = await repository.GetAllAsync();
            if (!entities.Any())  return NotFound("No Product found.");
            var (_, list) = ProductConversion.FromEntity(null!, entities);
            return list!.Any() ? Ok(list) : NotFound("No Product found.");
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(Guid id)
        {
            var entity = await repository.FindByIdAsync(id);
            if (entity is null) return NotFound("No Product found.");
            var (dto, _) = ProductConversion.FromEntity(entity,null!);
            return  Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Response>> Add(ProductDTO dto) {
            dto.Id = Guid.CreateVersion7();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = ProductConversion.ToEntity(dto);
            var response = await repository.CreateAsync(entity);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Response>> Update(ProductDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var entity = ProductConversion.ToEntity(dto);
            var response = await repository.UpdateAsync(entity);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Response>> Delete(ProductDTO dto)
        {
           var entity = ProductConversion.ToEntity(dto);
            var response = await repository.DeleteAsync(entity.Id);
            return response.Flage ? Ok(response.Flage) : BadRequest(response);
        }
    }
}
