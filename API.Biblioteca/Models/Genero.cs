using System.ComponentModel.DataAnnotations;

namespace API.Biblioteca.Models
{
    public class Genero
    {
        // Id da classe dever ser o nome da classe + "Id"
        public Guid GeneroId { get; set; }

        [Required] // O nome do gênero é obrigatório
        public string? Nome { get; set; }
    }
}