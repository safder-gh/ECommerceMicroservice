using System.ComponentModel.DataAnnotations;

namespace TemporalTable.Data;

public class Employee
{
    [Key]
    public Guid Id { get; set; }
    public required string FirstName { get; set;}
    public required string LastNameName { get; set; }
    public string Position { get; set; } = string.Empty;
}

