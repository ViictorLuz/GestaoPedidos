using GestaoPedidos.Application.UseCases;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly FecharPedidoUseCase _fecharPedidoUseCase;

    public PedidosController(IPedidoRepository pedidoRepository, FecharPedidoUseCase fecharPedidoUseCase)
    {
        _pedidoRepository = pedidoRepository;
        _fecharPedidoUseCase = fecharPedidoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido(CancellationToken cancellationToken)
    {
        var pedido = new Pedido();
        await _pedidoRepository.Adicionar(pedido, cancellationToken);
        return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido.Id);
    }

    [HttpPost("{id}/itens")]
    public async Task<IActionResult> AdicionarItem(Guid id, [FromBody] NovoItemRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var pedido = await _pedidoRepository.ObterPorId(id, cancellationToken);
            if (pedido == null) return NotFound("Pedido não encontrado.");

            var item = new ItemPedido(request.Descricao, request.Valor);

            pedido.AdicionarItem(item);

            await _pedidoRepository.Atualizar(pedido, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/fechar")]
    public async Task<IActionResult> FecharPedido(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _fecharPedidoUseCase.Executar(id, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPedido(Guid id, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorId(id, cancellationToken);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }
}

public record NovoItemRequest(string Descricao, decimal Valor);