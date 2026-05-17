using ExploreLatamAI.Api.Models.Domain;

namespace ExploreLatamAI.Api.Models.DTO
{
    // DTO utilizado para recibir datos desde el cliente
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public DateTime PublishedDate { get; set; }

        // Campos opcionales
        public string? UrlHandle { get; set; }
        public string? Content { get; set; }


        // Relacion: muchos a muchos (N:N)
        // Un blog puede pertenecer a multiples categorias
        public Guid[] Categories { get; set; }

    }
}

// Esta entidad representa un DTO (Data Transfer Object)
//Sirve para recibir datos desde el cliente frontend o Swagger.