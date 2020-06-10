using System.ComponentModel.DataAnnotations;

namespace CasaDoCodigo.Models
{
    public class Cadastro : BaseModel
    {
        public virtual Pedido Pedido { get; set; }

        [Required(ErrorMessage = "Nome obritatório")]
        [MinLength(5, ErrorMessage = "Nome deve ter o mínimo de 5 caracteres")]
        [MaxLength(50, ErrorMessage = "Nome deve ter o máximo de 50 caracteres")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Email obritatório")]
        [EmailAddress(ErrorMessage = "Entre com um email válido")]      
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Telefone obritatório")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = "";

        [Required(ErrorMessage = "Endereço obritatório")]
        public string Endereco { get; set; } = "";

        public string Complemento { get; set; } = "";

        [Required(ErrorMessage = "Bairro obritatório")]
        public string Bairro { get; set; } = "";

        [Required(ErrorMessage = "Município obritatório")]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; } = "";

        [Required(ErrorMessage = "UF obritatório")]
        public string UF { get; set; } = "";

        [Required(ErrorMessage = "CEP obritatório")]
        public string CEP { get; set; } = "";

        public Cadastro()
        {
        }
    }
}
