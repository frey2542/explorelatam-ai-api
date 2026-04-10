using ExploreLatamAI.Api.Models.Domain;

namespace ExploreLatamAI.Api.Repositories.Interface
{
    public interface ICategoryRepository
    {
        // Contrato del repositorio
        // Define las operaciones disponibles para Category
        // Solo trabaja con modelos de dominio
        Task<Category> CreateAsync(Category category);
    }
}
