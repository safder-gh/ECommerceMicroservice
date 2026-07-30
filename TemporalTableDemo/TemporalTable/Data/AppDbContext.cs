using Microsoft.EntityFrameworkCore;

namespace TemporalTable.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().ToTable("Employee", b => b.IsTemporal(t =>
        { 
            t.HasPeriodStart("SysStartTime");
            t.HasPeriodEnd("SysEndTime");
            t.UseHistoryTable("EmployeeHistoryEm");
        }));
    } 

}
