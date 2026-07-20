using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs.Conversion;

public static  class ProductConversion
{
    public static Product ToEntity(ProductDTO productDTO) => new Product { 
        Id = productDTO.Id, 
        Name = productDTO.Name, 
        Price = productDTO.Price, 
        Quantity = productDTO.Quantity };

    public static (ProductDTO?,IEnumerable<ProductDTO>?) FromEntity(Product? product, IEnumerable<Product>? products)
    {
        if(product is not null || products is null)
        {
            var singleProduct = new ProductDTO { Id = product!.Id, Name = product.Name, Price = product.Price, Quantity = product.Quantity };
            return (singleProduct, null);
        }else if (products is not null || product is null)
        {
            var _products = products!.Select(p =>
                new ProductDTO { Id = p.Id,Name=p.Name,Price = p.Price, Quantity=p.Quantity }
            ).ToList();
            return (null, _products);
        }
        return (null, null);
    }

}
