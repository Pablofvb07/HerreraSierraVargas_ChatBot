namespace HerreraSierraVargas_ChatBot.Interfaces
{
    public interface IChatBotService
    {
        public Task<string> GetChatbotResponse(string prompt);
        public Task<Boolean> SaveResponseInDatabase(string ChatbotPrompt, string ChatbotResponse);
    }
}
