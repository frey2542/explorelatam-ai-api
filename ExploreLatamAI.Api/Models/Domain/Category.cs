namespace ExploreLatamAI.Api.Models.Domain
{
    public class Category
    {
        // Entidad de dominio
        // Representa la tabla "Categories" en la base de datos (EF Core)
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
