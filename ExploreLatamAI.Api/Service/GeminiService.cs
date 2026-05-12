using System.Text;
using System.Text.Json;

namespace ExploreLatamAI.Api.Service
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"]
                ?? throw new Exception("Gemini API Key no configurada");
            Console.WriteLine("API KEY (INIT): " + _apiKey);
        }

        //Recibe un prompt que son instrucción y devuelve texto generado por IA.
        public async Task<string> GetChatResponse(string prompt)
        {
            //Este es el formato que Gemini espera
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini error: {response.StatusCode} - {responseContent}");
            }

            using var jsonDoc = JsonDocument.Parse(responseContent);

            //Parseo de respuesta
            var text = jsonDoc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "No se pudo generar contenido";
        }
    }
}

