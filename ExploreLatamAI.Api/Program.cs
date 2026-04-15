using ExploreLatamAI.Api.Data;
using ExploreLatamAI.Api.Repositories.Implementation;
using ExploreLatamAI.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar el DbContext en el contenedor de dependencias (DI)
// Permite acceder a la base de datos desde controllers, servicios o repositorios
// Configurar EF Core para usar SQL Server con la cadena de conexión "DefaultConnection"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registrar el repositorio en el contenedor de dependencias
// Permite inyectar ICategoryRepository en otras capas (ej Controllers)
 builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilita CORS para permitir solicitudes desde otros orígenes (cross-origin),
// como el frontend Angular en otro dominio o puerto
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin(); //permitir solo para desarrollo, en producion no
});

app.UseAuthorization();

app.MapControllers();

app.Run();
