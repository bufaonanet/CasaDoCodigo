using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Produto : BaseModel
    {
        [Required]
        public string Codigo { get; private set; }
       
        [Required]
        public string Nome { get; private set; }
       
        [Required]
        public decimal Preco { get; private set; }
       
        [Required]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Produto()
        {

        }

        public Produto(string codigo, string nome, decimal preco, Categoria categoria )
        {
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }
    }
}
