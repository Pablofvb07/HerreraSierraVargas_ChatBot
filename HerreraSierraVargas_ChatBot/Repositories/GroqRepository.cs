using HerreraSierraVargas_ChatBot.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SierraHerreraVargasChatBot.Repositories
{
    public class GroqRepository : IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly string GroqApiKey = "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";

        public GroqRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetChatbotResponse(string prompt)
        {
            var url = "https://api.groq.com/openai/v1/chat/completions";

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + GroqApiKey);

            var response = await _httpClient.PostAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    dynamic errorObj = JsonConvert.DeserializeObject(jsonResponse);
                    string errorMessage = errorObj.error?.message;
                    return "Error de Groq: " + errorMessage;
                }
                catch
                {
                    return $"Error: {response.StatusCode}";
                }
            }

            try
            {
                dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                string text = data.choices[0].message.content;
                return text.Trim();
            }
            catch
            {
                return jsonResponse;
            }
        }

        public Task<bool> SaveResponseInDatabase(string ChatbotPrompt, string ChatbotResponse)
        {
            throw new NotImplementedException();
        }
    }
}
