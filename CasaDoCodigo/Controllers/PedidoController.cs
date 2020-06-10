using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewmModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Cadastro()
        {
            Pedido pedido = _pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction(nameof(Carrossel));
            }
            return View(pedido.Cadastro);
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                _pedidoRepository.AddItem(codigo);
            }

            var items = _pedidoRepository.GetPedido().Itens;
            var carrinhoView = new CarrinhoViewModel(items);

            return View(carrinhoView);
        }

        public IActionResult Carrossel()
        {
            return View(_produtoRepository.GetProdutos());
        }

        [HttpPost]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                Pedido pedido = _pedidoRepository.UpdateCadastro(cadastro);
                View(pedido);
            }


            return RedirectToAction("Cadastro");
        }

        [HttpPost]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody] ItemPedido itemPedido)
        {
            return _pedidoRepository.UpdateQuantidade(itemPedido);


        }
    }
}
