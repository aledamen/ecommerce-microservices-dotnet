using Microsoft.EntityFrameworkCore;
using MSCustomers.Domain.Entities;

namespace MSCustomers.Infraestructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(o => o.Address);

            base.OnModelCreating(modelBuilder);
        }
    }
}
