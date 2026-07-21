using System.ComponentModel.DataAnnotations;

namespace AuthenticationApi.Application.DTOs;

public record AppUserDTO(
    Guid Id,
    [Required] string Name,
    [Required] string CellNumber,
    [Required] string Address,
    [Required, EmailAddress] string Email,
    [Required] string UserName,
    [Required] string Role);