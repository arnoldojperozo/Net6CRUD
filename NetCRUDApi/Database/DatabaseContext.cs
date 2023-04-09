using Microsoft.EntityFrameworkCore;
using NetCRUDApi.Models;

namespace NetCRUDApi.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
}