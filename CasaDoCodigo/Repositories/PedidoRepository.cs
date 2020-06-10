using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewmModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {
        void AddItem(string codigo);
        Pedido GetPedido();
        UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido);
        Pedido UpdateCadastro(Cadastro cadastro);
    }

    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly ICadastroRepository _cadastroRepository;
        public PedidoRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor,
            IItemPedidoRepository itemPedidoRepository,
            ICadastroRepository cadastroRepository) : base(context)
        {
            _contextAccessor = contextAccessor;
            _itemPedidoRepository = itemPedidoRepository;
            _cadastroRepository = cadastroRepository;
        }

        public void AddItem(string codigo)
        {
            var produto = _context.Set<Produto>()
                .Where(p => p.Codigo == codigo)
                .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado!");
            }

            var pedido = GetPedido();
            var itemPedido = _context.Set<ItemPedido>()
                            .Where(ip => ip.Produto.Codigo == codigo
                                      && ip.Pedido.Id == pedido.Id)
                            .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _context.Set<ItemPedido>().Add(itemPedido);
                _context.SaveChanges();
            }

        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();

            var pedido = _dbSet
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                _dbSet.Add(pedido);
                _context.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }
        /// <summary>
        /// Retorna o id do pedido que está salvo na sessão da conexão
        /// </summary>
        /// <returns>int pedidoId</returns>
        private int? GetPedidoId()
        {
            return _contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            _contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDb = _itemPedidoRepository.GetItemPedido(itemPedido.Id);

            if (itemPedidoDb != null)
            {
                itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);

                if (itemPedidoDb.Quantidade == 0)
                {
                    _itemPedidoRepository.RemoveItemPedido(itemPedido.Id);
                }

                _context.SaveChanges();

                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new UpdateQuantidadeResponse(itemPedidoDb, carrinhoViewModel);
            }

            throw new ArgumentException("Item do pedido não encontrado");
        }

        public Pedido UpdateCadastro(Cadastro cadastro)
        {
            var pedido = GetPedido();
            _cadastroRepository.Update(pedido.Cadastro.Id, cadastro);
            return pedido;
        }
    }
}
