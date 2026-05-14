using GestaoPedidos.Application.DTOs;
using GestaoPedidos.Domain.Interfaces;

namespace GestaoPedidos.Application.UseCases;

public class FecharPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public FecharPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<PedidoResponseDTO> Executar(Guid pedidoId, CancellationToken cancellationToken = default)
    {
        var pedido = await _pedidoRepository.ObterPorId(pedidoId, cancellationToken);

        if (pedido == null)
            throw new KeyNotFoundException("Pedido não encontrado.");

        pedido.FecharPedido();

        await _pedidoRepository.Atualizar(pedido, cancellationToken);

        return new PedidoResponseDTO(
            pedido.Id,
            pedido.Status.ToString(),
            pedido.ValorTotal,
            pedido.ValorDesconto,
            pedido.ValorFinal,
            pedido.Itens.Count
        );
    }
}