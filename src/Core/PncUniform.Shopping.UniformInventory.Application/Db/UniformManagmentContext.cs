using Microsoft.EntityFrameworkCore;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Db
{
    public class UniformManagementContext : DbContext
    {
        public UniformManagementContext(DbContextOptions<UniformManagementContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Uniform> Uniforms { get; set; }
    }
}