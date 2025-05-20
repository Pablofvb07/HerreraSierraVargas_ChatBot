namespace HerreraSierraVargas_ChatBot.Models
{
    public class GeminiPart
    {
        public string text { get; set; }
    }

    public class GeminiContent
    {
        public List<GeminiPart> parts { get; set; }
    }

    public class GeminiRequest
    {
        public List<GeminiContent> contents { get; set; }
    }
}
