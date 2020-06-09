using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido GetItemPedido(int itemPedidoId);
        void RemoveItemPedido(int itemPedidoId);
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext context) : base(context)
        {
        }

        public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return _dbSet.Where(ip => ip.Id == itemPedidoId)
                         .SingleOrDefault();
        }

        public void RemoveItemPedido(int itemPedidoId)
        {
            _dbSet.Remove(GetItemPedido(itemPedidoId));
        }
    }
}
