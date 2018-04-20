using Microsoft.EntityFrameworkCore;
 
namespace C_Sharp_Belt.Models
{
    public class BeltExamContext : DbContext
    {
        public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        // Update to references to Database tables
        // public DbSet<WeddingPlan> weddingplan { get; set; }
        // public DbSet<WeddingInfo> weddinginfo { get; set; }
    }
}