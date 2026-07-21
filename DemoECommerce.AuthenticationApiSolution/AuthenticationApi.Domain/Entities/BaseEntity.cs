namespace AuthenticationApi.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7(); 
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; } 
    public bool IsDeleted { get; set; }
}
