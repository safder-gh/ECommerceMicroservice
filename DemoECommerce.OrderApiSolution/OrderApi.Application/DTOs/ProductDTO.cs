using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs;

public record ProductDTO
{
    public Guid Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required, DataType(DataType.Currency)]
    public decimal Price { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
