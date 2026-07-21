using System.ComponentModel.DataAnnotations;

namespace OrderApi.Application.DTOs;

public record OrderDTO
{
    public Guid? Id { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}