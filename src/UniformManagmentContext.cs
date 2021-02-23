

namespace PncUniform.Shopping.UniformInventory.Domain.DbContext
{
    public class UniformManagementContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Uniform> Uniforms { get; set; }
    }
}
