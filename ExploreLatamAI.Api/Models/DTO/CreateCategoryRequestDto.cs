namespace ExploreLatamAI.Api.Models.DTO
{

    // DTO de entrada (Request)
    // Se usa para recibir datos desde el cliente (frontend/Postman)
    // No contiene lógica ni acceso a base de datos
    public class CreateCategoryRequestDto
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
