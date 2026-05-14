using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorId(Guid id, CancellationToken cancellationToken = default);
    Task Adicionar(Pedido pedido, CancellationToken cancellationToken = default);
    Task Atualizar(Pedido pedido, CancellationToken cancellationToken = default);
}