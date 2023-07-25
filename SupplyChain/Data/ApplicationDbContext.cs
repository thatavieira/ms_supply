using Microsoft.EntityFrameworkCore;
using SupplyChain.Models;
using SupplyChain.ViewModels;

namespace SupplyChain.Data;

public class ApplicationDbContext : DbContext   
{
    public DbSet<Product> Products { get; set;  }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    
    public DbSet<InventoryManagementChart> InventoryManagementChart { get; set; }

   public DbSet<InventoryManagement> InventoryManagements { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

}