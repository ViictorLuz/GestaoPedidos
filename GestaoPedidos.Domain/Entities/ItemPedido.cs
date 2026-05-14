namespace GestaoPedidos.Domain.Entities;

public class ItemPedido
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; } = null!;
    public decimal Valor { get; private set; }

    protected ItemPedido() { }

    public ItemPedido(string descricao, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descrição é obrigatória.");
        if (valor <= 0) throw new ArgumentException("Valor do item deve ser maior que zero.");

        Id = Guid.NewGuid();
        Descricao = descricao;
        Valor = valor;
    }
}