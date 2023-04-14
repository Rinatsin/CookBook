using Microsoft.EntityFrameworkCore;

namespace CookBook.Core.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Recipe> Recipes { get; set; }
}