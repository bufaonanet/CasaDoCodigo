using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _dbSet.OrderBy(x => x.Nome).ToListAsync();
        }        

        public async Task<Categoria> SaveCategoria(string nome)
        {
            var novaCategoria = _dbSet.Where(x => x.Nome == nome).SingleOrDefault();

            if (novaCategoria == null)
            {
                novaCategoria = new Categoria { Nome = nome };

                novaCategoria = _dbSet.Add(novaCategoria).Entity;
                await _context.SaveChangesAsync();               
            }

            return novaCategoria;
        }
    }
}
