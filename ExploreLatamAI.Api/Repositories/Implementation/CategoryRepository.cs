using ExploreLatamAI.Api.Data;
using ExploreLatamAI.Api.Models.Domain;
using ExploreLatamAI.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExploreLatamAI.Api.Repositories.Implementation
{
    //Esta clase implementa  los metodos de la interfaz, ademas tratamos con los modelos dominios y no con los dtos
    //ademas en program agrgamos el servivio ara poder usarla en nuestro controlador
    public class CategoryRepository : ICategoryRepository
    {
        // Contexto de EF Core para interactuar con la BD
        private readonly ApplicationDbContext _context;

        // Inyección de dependencias del DbContext
        public CategoryRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Crea una nueva categoría en la base de datos
        public async Task<Category> CreateAsync(Category category)
        {

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }



        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
