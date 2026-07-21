namespace ProductApi.Domain.Entities;

public class Order : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
}
