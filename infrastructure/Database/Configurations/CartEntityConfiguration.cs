using core.CartAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastrucrue.Database.Configurations;

public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
{
    public void Configure(EntityTypeBuilder<CartEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.CartEntity)
            .HasForeignKey(x => x.CartId);

    }
}