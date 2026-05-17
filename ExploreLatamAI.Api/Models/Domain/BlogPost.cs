namespace ExploreLatamAI.Api.Models.Domain
{
    public class BlogPost
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }


        // Relacion: muchos a muchos (N:N)
        // Un blog puede pertenecer a multiples categorias
        public ICollection<Category> Categories { get; set; }

    }
}
