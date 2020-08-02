using System;
using System.Linq;
using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories.Interfaces;

namespace CasaDoCodigo.Repositories
{    

    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext context) : base(context)
        {
        }

        public Cadastro Update(int cadastroId, Cadastro novoCadastro)
        {
            var cadastroDb = _dbSet.Where(c => c.Id == cadastroId).SingleOrDefault();

            if(cadastroDb == null)
            {
                throw new ArgumentException("cadastro");
            }

            cadastroDb.Update(novoCadastro);
            _context.SaveChanges();
            return cadastroDb;
        }
    }
}
