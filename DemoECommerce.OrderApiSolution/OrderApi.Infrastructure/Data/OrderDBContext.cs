using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace OrderApi.Infrastructure.Data;

public class OrderDBContext(DbContextOptions<OrderDBContext> options):DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}
