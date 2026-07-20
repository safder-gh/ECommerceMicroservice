using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs;

public record ProductDTO(Guid Id,
    [Required]
    string name,
    [Required,Range(1,int.MaxValue)]
    int Quantity,
    [Required,DataType(DataType.Currency)]
    decimal Price
    );
