using GestaoPedidos.Domain.Enums;

namespace GestaoPedidos.Domain.Entities;

public class Pedido
{
    public Guid Id { get; private set; }
    public StatusPedido Status { get; private set; }
    public decimal ValorDesconto { get; private set; }

    private readonly List<ItemPedido> _itens;
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    public decimal ValorTotal => _itens.Sum(i => i.Valor);
    public decimal ValorFinal => ValorTotal - ValorDesconto;

    public Pedido()
    {
        Id = Guid.NewGuid();
        Status = StatusPedido.Aberto;
        _itens = new List<ItemPedido>();
    }

    public void AdicionarItem(ItemPedido item)
    {
        if (Status != StatusPedido.Aberto)
            throw new InvalidOperationException("Não é possível adicionar itens a um pedido que não está aberto.");

        _itens.Add(item);
    }

    public void FecharPedido()
    {
        if (!_itens.Any())
            throw new InvalidOperationException("Um pedido não pode ser fechado sem possuir itens.");

        if (Status == StatusPedido.Fechado)
            return;

        AplicarDescontoSeElegivel();
        Status = StatusPedido.Fechado;
    }

    private void AplicarDescontoSeElegivel()
    {
        if (ValorTotal > 1000m)
        {
            ValorDesconto = ValorTotal * 0.10m;
        }
    }
}