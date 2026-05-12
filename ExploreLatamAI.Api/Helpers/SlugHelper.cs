namespace ExploreLatamAI.Api.Helpers
{
    public static class SlugHelper
    {
        public static string Generate(string title)
        {
            return title
                .ToLower()
                .Trim()
                .Replace(" ", "-")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u");
        }
    }
}
