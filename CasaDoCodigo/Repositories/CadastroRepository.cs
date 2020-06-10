using CasaDoCodigo.DB;
using CasaDoCodigo.Models;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
        Cadastro Update(int cadastroId, Cadastro novoCadastro);
    }

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
