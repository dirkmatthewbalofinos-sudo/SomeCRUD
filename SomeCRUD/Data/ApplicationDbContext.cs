using Microsoft.EntityFrameworkCore;
using SomeCRUD.Models;

namespace SomeCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}