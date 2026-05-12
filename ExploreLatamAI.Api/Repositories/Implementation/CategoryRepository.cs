using ExploreLatamAI.Api.Data;
using ExploreLatamAI.Api.Models.Domain;
using ExploreLatamAI.Api.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
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
            _context = context;
        }

        // Crea una nueva categoría en la base de datos
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory =  await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory is null)
            {
                return null;
            } 

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;

        }

        //Mostrar todas las categorias
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        //Buscar categoria por id
        public async Task<Category?> GetByIdAsync(Guid id)  
        {
           return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            //Buscar la entidad existente en la BD
            var existingCategory = await _context.Categories.
                FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory == null) return null;

            //Actualizar SOLO los campos necesarios
            existingCategory.Name = category.Name;
            existingCategory.UrlHandle = category.UrlHandle;

            await _context.SaveChangesAsync();
            return existingCategory;




        }
    }
}
