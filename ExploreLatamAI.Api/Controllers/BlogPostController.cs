using ExploreLatamAI.Api.Models.Domain;
using ExploreLatamAI.Api.Models.DTO;
using ExploreLatamAI.Api.Repositories.Interface;
using ExploreLatamAI.Api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExploreLatamAI.Api.Controllers
{

    // Endpoint base: https://localhost:xxxx/api/blogPost
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {


        private readonly GeminiService _geminiService; // IA
        private readonly IBlogPostRepository _blogPostRepository; // Base de datos


        public BlogPostController(IBlogPostRepository blogPostRepository, GeminiService geminiService)
        {
            _blogPostRepository = blogPostRepository;
            _geminiService = geminiService;
        }



        // POST: https://localhost:7263/api/BlogPost
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostRequestDto request)
        {



            //Transformas el DTO, entidad de dominio
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                UrlHandle = request.UrlHandle,
                FeaturedImageUrl = request.FeaturedImageUrl,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible
            };

            var result = await _blogPostRepository.CreateAsync(blogPost);

            //Transformas la entida dominio a DTO respuesta para el clientel
            var response = new BlogPostDto
            {
                Id = Guid.NewGuid(),
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
            };

            return Ok(response);
        }


        // POST: https://localhost:7263/api/BlogPost/generate-content
        [HttpPost("generate-content")]
        public async Task<IActionResult> GenerateContent([FromBody] GenerateContentRequestDto request)
        {
            try
            {
                // Validación basica
                if (string.IsNullOrWhiteSpace(request.Title) ||
                    string.IsNullOrWhiteSpace(request.ShortDescription))
                {
                    return BadRequest("Título y descripción son obligatorios");
                }

                // Prompt reforzado (evita markdown)
                var prompt = $@"
                Eres un sistema experto en generación de contenido para blogs de viajes en Latinoamérica.

                IMPORTANTE:
                - Responde SOLO JSON válido
                - NO uses markdown
                - NO uses ``` ni explicaciones adicionales

                Formato obligatorio:
                {{
                  ""content"": ""HTML del artículo"",
                  ""urlHandle"": ""slug-seo-amigable""
                }}

                Reglas del slug:
                - minúsculas
                - sin tildes
                - sin caracteres especiales
                - separado por guiones

                Datos:
                Título: {request.Title}
                Descripción: {request.ShortDescription}
                ";

                //  Llamada a Gemini
                var response = await _geminiService.GetChatResponse(prompt);

                if (string.IsNullOrWhiteSpace(response))
                {
                    return StatusCode(500, "IA no devolvió respuesta");
                }

                // LIMPIEZA CRÍTICA (esto evita el error 500)
                response = response.Replace("```json", "")
                                   .Replace("```", "")
                                   .Trim();

                //  Validación minima de JSON
                if (!response.TrimStart().StartsWith("{"))
                {
                    return StatusCode(500, "IA devolvió formato inválido: " + response);
                }

                // Parseo seguro
                using var jsonDoc = JsonDocument.Parse(response);

                var content = jsonDoc.RootElement.GetProperty("content").GetString();
                var urlHandle = jsonDoc.RootElement.GetProperty("urlHandle").GetString();

                // Respuesta final al frontend
                return Ok(new
                {
                    content,
                    urlHandle
                });
            }
            catch (Exception ex)  
            {
                // Evita exponer detalles internos en producción
                return StatusCode(500, "Error generando contenido con IA");
            }
        }



        // GET: https://localhost:7263/api/BlogPosts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blosgPosts = await _blogPostRepository.GetAllBlogPostsAsync();

            var response = new List<BlogPostDto>();

            foreach (var bloPost in blosgPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = bloPost.Id,
                    Title = bloPost.Title,
                    Author = bloPost.Author,
                    Content = bloPost.Content,
                    ShortDescription = bloPost.ShortDescription,
                    PublishedDate = bloPost.PublishedDate,
                    FeaturedImageUrl = bloPost.FeaturedImageUrl,
                    IsVisible = bloPost.IsVisible,
                    UrlHandle = bloPost.UrlHandle
                });
            }

            return Ok(response);
        }






    }

}
  