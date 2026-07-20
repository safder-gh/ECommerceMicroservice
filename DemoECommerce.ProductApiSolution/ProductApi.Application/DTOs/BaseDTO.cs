using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs;

public abstract record BaseDTO
{
    public required Guid Id { get; init; } 
    public Guid? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public Guid UpdatedBy { get; init; }
    public DateTime? UpdatedOn { get; init; }
    public bool IsDeleted { get; init; }
}
