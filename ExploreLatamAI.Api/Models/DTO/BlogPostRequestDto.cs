namespace ExploreLatamAI.Api.Models.DTO
{
    public class BlogPostRequestDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public DateTime PublishedDate { get; set; }

        // Opcionales 
        public string? UrlHandle { get; set; }
        public string? Content { get; set; }
    }
}

//Esta entidad o modelo es un Data Transfer Object
//Sirve para recibir datos desde el cliente frontend / Swagger.