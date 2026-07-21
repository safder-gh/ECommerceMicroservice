using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Domain.Entities;

public class AppUser:BaseEntity
{
    public required string Name { get; set; }
    public  string? CellNumber { get; set; }
    public string? Address { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public string? Role { get; set; }
}
