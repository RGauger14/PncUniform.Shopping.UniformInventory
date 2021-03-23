using System.Data.Entity;
using System.Data.Entity.SqlServer;
using Microsoft.EntityFrameworkCore;
using PncUniform.Shopping.UniformInventory.Application.Domain.Entities;

namespace PncUniform.Shopping.UniformInventory.Application.Db
{
    [DbConfigurationType(typeof(DbContextConfiguration))]
    public class UniformManagementContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UniformManagementContext(DbContextOptions<UniformManagementContext> options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Customer> Customers { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Uniform> Uniforms { get; set; }
    }

    public class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            var now = SqlProviderServices.Instance;
            SqlProviderServices.TruncateDecimalsToScale = false;
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}