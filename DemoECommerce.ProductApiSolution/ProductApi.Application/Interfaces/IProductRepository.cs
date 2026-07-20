using ecommerce.SharedLibrary.Interfaces;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Interfaces;

public interface IProductRepository: IGenericRepository<Product, Guid> { }