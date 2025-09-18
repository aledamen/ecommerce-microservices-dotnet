using Microsoft.EntityFrameworkCore;
using MSOrders.Domain.Entities;

namespace MSOrders.Infraestructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Order>().OwnsOne(o => o.ShippingAddress);

            base.OnModelCreating(modelBuilder);
        }
    }
}
