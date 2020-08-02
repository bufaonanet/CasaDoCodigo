using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task SaveLivros(List<Livro> livros);
        IList<Produto> GetProdutos();
    }
}