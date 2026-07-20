using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities;

public class Product : BaseEntity, IPrice
{
    public required string Name { get; set; } 
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}
