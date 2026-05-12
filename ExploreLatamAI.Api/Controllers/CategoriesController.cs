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
            _categoryRepository = categoryRepository;
        }



        //POST:  https://localhost:7263/api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
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


        //GET:  https://localhost:7263/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            // Llama al repositorio para obtener todas las categorías desde la base de datos
            var categories =  await _categoryRepository.GetAllCategoriesAsync();

            // Se crea una lista vacia de DTOs (Data Transfer Object)
            // Aqui se almacenara la respuesta que se enviara al cliente
            var response = new List<CategoryDto>();

            // Se recorre cada categoria obtenida del dominio (modelo interno)
            foreach (var category in categories)
            {
                // Se transforma (mapea) cada entidad Category a un CategoryDto
                // Esto evita exponer directamente el modelo de dominio
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);

            // Mapear entidades a DTO usando LINQ (proyeccion)
            //var response = categories.Select(category => new CategoryDto
            //{
            //    Id = category.Id,
            //    Name= category.Name,
            //    UrlHandle= category.UrlHandle,
            //}).ToList();
            //return Ok(response);

        }


        //GET:  https://localhost:7263/api/Categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async  Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };

            return Ok(response);
        }


        //PUT: https://localhost:7263/api/Categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory(
            [FromRoute] Guid id, 
            UpdateCategoryRequestDto request)
        {

            //Convertir DTO a modelo dominio
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            // Llamar repositorio
            category = await _categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            // Convertir dominio a DTO de respuesta
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }

        //DELETE: https://localhost:7263/api/Categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
           var category = await _categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            //Convertir modelo Dominio a Dto
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

    }
}