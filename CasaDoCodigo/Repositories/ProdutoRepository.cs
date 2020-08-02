using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public ProdutoRepository(
            ApplicationContext context,
            ICategoriaRepository categoriaRepository
        ) : base(context)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IList<Produto> GetProdutos()
        {
            return _dbSet.OrderBy(p => p.Codigo).ToList();
        }

        public async Task SaveLivros(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                bool produtoJaSalvo = _dbSet.Where(p => p.Codigo == livro.Codigo).Any();

                if (!produtoJaSalvo)
                {
                    Categoria categoria = await _categoriaRepository.SaveCategoria(livro.Categoria);
                    
                    _dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
