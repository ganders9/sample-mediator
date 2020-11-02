using Microsoft.EntityFrameworkCore;
using Sample.Mediator.Api.Database.Configurations;

namespace Sample.Mediator.Api.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ProductConfiguration.Config(builder);
        }
    }
}