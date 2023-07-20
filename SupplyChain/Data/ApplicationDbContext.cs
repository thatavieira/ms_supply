using Microsoft.EntityFrameworkCore;

namespace SupplyChain.Data;

public class ApplicationDbContext : DbContext   
{
 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
}