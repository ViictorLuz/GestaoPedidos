using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces;
using GestaoPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ApplicationDbContext _context;

    public PedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Pedido pedido, CancellationToken cancellationToken = default)
    {
        await _context.Pedidos.AddAsync(pedido, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Atualizar(Pedido pedido, CancellationToken cancellationToken = default)
    {
        foreach (var item in pedido.Itens)
        {
            if (_context.Entry(item).State == EntityState.Detached || _context.Entry(item).State == EntityState.Modified)
            {
                _context.Entry(item).State = EntityState.Added;
            }
        }

        _context.Entry(pedido).State = EntityState.Modified;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Pedido?> ObterPorId(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}