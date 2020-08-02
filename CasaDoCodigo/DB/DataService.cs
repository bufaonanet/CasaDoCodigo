using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using CasaDoCodigo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo.DB
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _context;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(ApplicationContext context, IProdutoRepository produtoRepository)
        {
            _context = context;
            _produtoRepository = produtoRepository;
        }

        public void InicializaDb()
        {
            //Garante que o banco de dados foi criado usando migrations
            //Caso não queira usar migrations, trocar para "EnsureCreated()"
            _context.Database.Migrate();

            List<Livro> livros = GetLivros();
            _produtoRepository.SaveLivros(livros);
        }       

        private static List<Livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }
    }

    
}
