using ExploreLatamAI.Api.Models.Domain;
// Importa Entity Framework Core (ORM que conecta C# con la base de datos)
using Microsoft.EntityFrameworkCore;


namespace ExploreLatamAI.Api.Data
{
    // Esta clase es el CONTEXTO de la base de datos
    // Hereda de DbContext, EF Core la usa para mapear las clases a tabla
    public class ApplicationDbContext: DbContext
    {
        //Constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet, es la representacion de la entida a tabla en la base de datos
        // Representa las tablas en la base de datos por ejemplo, BlogPosts y Categories
        // EF Core usará esta propiedad para hacer CRUD automáti
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }    
    }
}


//NOTA:
// DbContext es la conexion a la DB
// DbSet<T> representa las tablas
// Ef Core traduce codigo C# a SQL