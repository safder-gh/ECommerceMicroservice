using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.DTOs;

public record OrderDetailDTO(Guid OrderId, Guid ProductId, Guid CustomerId, string email, string CellNumber, string ProductName, int Quantity, decimal UnitPrice, decimal TotalPrice, DateTime OrderDate);