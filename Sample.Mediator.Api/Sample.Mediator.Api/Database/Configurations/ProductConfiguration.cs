using Microsoft.EntityFrameworkCore;
using Sample.Mediator.Api.Database.Entities;

namespace Sample.Mediator.Api.Database.Configurations
{
    public static class ProductConfiguration
    {
        public static void Config(ModelBuilder builder)
        {
            builder.Entity<ProductEntity>(cfg =>
            {
                cfg.HasKey(e => e.Id);
                cfg.HasAlternateKey(e => e.ExternalId);

                cfg.HasIndex(e => e.Code).IsUnique();

                cfg.Property(e => e.ExternalId)
                    .IsRequired();
                cfg.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(8);
                cfg.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
                cfg.Property(e => e.Price)
                    .IsRequired();
            });
        }
    }
}