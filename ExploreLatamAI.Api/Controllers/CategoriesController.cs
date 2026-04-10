using ExploreLatamAI.Api.Data;
using ExploreLatamAI.Api.Models.Domain;
using ExploreLatamAI.Api.Models.DTO;
using ExploreLatamAI.Api.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExploreLatamAI.Api.Controllers
    // El controller solo orquesta, no contiene lógica de negocio,  Recibe -> transforma -> guarda -> response
{
    // Endpoint base: https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }



        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            // Mapeo Manual
            // DTO → Entidad (Domain)
            // Se transforman los datos recibidos del cliente (DTO)
            // a una entidad que EF Core puede persistir en la base de datos
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            // Persistencia en BD a través del repositorio
            await _categoryRepository.CreateAsync(category);

            // Entidad → DTO (Response)
            // Se transforma la entidad en un objeto seguro para el cliente
            // para enviar una respuesta controlada al cliente
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(response);
        }
    }
}