namespace HerreraSierraVargas_ChatBot.Models
{
    public class OpenAIRequest
    {
        public string model { get; set; }
        public string input { get; set; }
    }

    public class OpenAIResponseChoice
    {
        public string message { get; set; }
    }

    public class OpenAIResponse
    {
        public List<OpenAIResponseChoice> choices { get; set; }
    }
}
