using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities;

public interface IPrice
{
    public decimal Price { get; set; }
}
