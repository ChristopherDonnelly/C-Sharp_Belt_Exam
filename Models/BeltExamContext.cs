using Microsoft.EntityFrameworkCore;
 
namespace C_Sharp_Belt.Models
{
    public class BeltExamContext : DbContext
    {
        public BeltExamContext(DbContextOptions<BeltExamContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Activities> activites { get; set; }
        public DbSet<UserActivity> user_activity { get; set; }
    }
}