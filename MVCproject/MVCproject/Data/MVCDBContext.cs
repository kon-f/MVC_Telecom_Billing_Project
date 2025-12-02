/// <summary>
/// Application database context using Entity Framework Core.
/// Defines all entity sets and configures model behavior.
/// </summary>
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;


namespace MVCproject.Data
{
    public class MVCDBContext : DbContext
    {
        public MVCDBContext(DbContextOptions<MVCDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<BillCall> BillsCalls { get; set; }
    }
}
