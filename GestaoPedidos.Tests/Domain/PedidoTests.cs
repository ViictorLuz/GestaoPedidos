using System;
using System.Linq;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Enums;
using Xunit;

namespace GestaoPedidos.Tests.Domain;

public class PedidoTests
{
    [Fact]
    public void PedidoDeveNascerAberto()
    {
        var pedido = new Pedido();

        Assert.Equal(StatusPedido.Aberto, pedido.Status);
        Assert.Empty(pedido.Itens);
    }

    [Fact]
    public void NaoDeveFecharPedidoSemItens()
    {
        var pedido = new Pedido();

        var exception = Assert.Throws<InvalidOperationException>(() => pedido.FecharPedido());
        Assert.Equal("Um pedido não pode ser fechado sem possuir itens.", exception.Message);
    }

    [Fact]
    public void NaoDeveAplicarDescontoAbaixoDe1000()
    {
        var pedido = new Pedido();
        pedido.AdicionarItem(new ItemPedido("Teclado Mecânico", 300m));
        pedido.AdicionarItem(new ItemPedido("Mouse sem fio", 200m)); 

        pedido.FecharPedido();

        Assert.Equal(StatusPedido.Fechado, pedido.Status);
        Assert.Equal(0m, pedido.ValorDesconto);
        Assert.Equal(500m, pedido.ValorFinal);
    }

    [Fact]
    public void DeveAplicarDescontoAoFecharPedidoAcimaDe1000()
    {
        var pedido = new Pedido();
        pedido.AdicionarItem(new ItemPedido("Monitor Ultrawide", 1200m)); 

        pedido.FecharPedido();

        Assert.Equal(StatusPedido.Fechado, pedido.Status);
        Assert.Equal(120m, pedido.ValorDesconto);
        Assert.Equal(1080m, pedido.ValorFinal);
    }
}