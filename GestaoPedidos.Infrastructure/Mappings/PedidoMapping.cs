using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Mappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.ValorDesconto).HasPrecision(18, 2);

        builder.Ignore(p => p.ValorTotal);
        builder.Ignore(p => p.ValorFinal);

        builder.Metadata.FindNavigation(nameof(Pedido.Itens))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(p => p.Itens)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ItemPedidoMapping : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Descricao).IsRequired().HasMaxLength(200);
        builder.Property(i => i.Valor).HasPrecision(18, 2).IsRequired();
    }
}