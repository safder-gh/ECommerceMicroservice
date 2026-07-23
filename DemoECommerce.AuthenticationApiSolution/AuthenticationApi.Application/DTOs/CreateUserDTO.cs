using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Application.DTOs;

public record CreateUserDTO(
    Guid Id,
    [Required] string Name,
    [Required] string CellNumber,
    [Required] string Address,
    [Required, EmailAddress] string Email,
    [Required] string DisplayName,
    [Required] string Password,
    [Required] string Role);