namespace HerreraSierraVargas_ChatBot.Interfaces
{
    public interface IChatBotService
    {
        Task<string> GetChatbotResponse(string prompt);
        Task<bool> SaveResponseInDatabase(string ChatbotPrompt, string ChatbotResponse);
    }
}