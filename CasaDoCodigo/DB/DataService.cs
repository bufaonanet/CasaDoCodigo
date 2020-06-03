using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo.DB
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            _context = context;
        }

        public void InicializaDb()
        {
            //Garante que o banco de dados foi criado usando migrations
            //Caso não queira usar migrations, trocar para "EnsureCreated()"
            _context.Database.Migrate();

            var json = File.ReadAllText("livros.json");
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
        }
    }

    class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
