using core.CartAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastrucrue.Database.Configurations;

public class CartProductEntityConfiguration : IEntityTypeConfiguration<CartProductEntity>
{
    public void Configure(EntityTypeBuilder<CartProductEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(x => x.CartEntity)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Product)
            .WithOne();

    }
}