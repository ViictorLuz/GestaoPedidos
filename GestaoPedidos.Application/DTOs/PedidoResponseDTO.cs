namespace GestaoPedidos.Application.DTOs;

public record PedidoResponseDTO(
    Guid Id,
    string Status,
    decimal ValorTotal,
    decimal ValorDesconto,
    decimal ValorFinal,
    int QuantidadeItens
);