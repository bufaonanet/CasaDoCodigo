using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Produto> GetProdutos()
        {
            return _dbSet.OrderBy(p => p.Codigo).ToList();
        }

        public void SaveLivros(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                bool produtoJaSalvo = _dbSet.Where(p => p.Codigo == livro.Codigo).Any();

                if (!produtoJaSalvo)
                {
                    _dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }
            }
            _context.SaveChanges();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
