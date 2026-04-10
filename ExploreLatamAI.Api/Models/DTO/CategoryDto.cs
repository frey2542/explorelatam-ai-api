namespace ExploreLatamAI.Api.Models.DTO
{
    public class CategoryDto
    {
        // DTO de salida (Response)
        // Se usa para enviar datos al cliente de forma controlada
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
