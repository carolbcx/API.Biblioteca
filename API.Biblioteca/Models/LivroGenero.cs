namespace API.Biblioteca.Models
{
    public class LivroGenero
    {
        public Guid LivroGeneroId { get; set; }

        // Chave estrangeira para Livro
        public Guid LivroId { get; set; }
        // Propriedade de navegação para Livro
        public Livro? Livro { get; set; }

        // Chave estrangeira para Genero
        public Guid GeneroId { get; set; }
        // Propriedade de navegação para Genero
        public Genero? Genero { get; set; }

        // Propriedade de navegação para a relação muitos-para-muitos com Livro
        public ICollection<Livro>? Livros { get; set; }
    }
}
