using Newtonsoft.Json;
using HerreraSierraVargas_ChatBot.Interfaces;
using HerreraSierraVargas_ChatBot.Models;
using HerreraSierraVargas_ChatBot.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace HerreraSierraVargas_ChatBot.Repositories
{
    public class GroqRepository : IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly ChatDbContext _context;
        private readonly string GroqApiKey = "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";

        public GroqRepository(ChatDbContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
        }

        public async Task<string> GetChatbotResponse(string prompt)
        {
            try
            {
                var url = "https://api.groq.com/openai/v1/chat/completions";

                var requestBody = new
                {
                    model = "llama3-70b-8192", // corregido el nombre del modelo si aplica
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
                        return $"Error HTTP: {response.StatusCode}";
                    }
                }

                dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                string text = data.choices[0].message.content;
                return text.Trim();
            }
            catch (Exception ex)
            {
                return $"Error al procesar la respuesta: {ex.Message}";
            }
        }

        public async Task<bool> SaveResponseInDatabase(string prompt, string response)
        {
            try
            {
                var historial = new ChatHistorial
                {
                    Prompt = prompt,
                    Response = response,
                    Timestamp = DateTime.Now,
                    BotName = "Groq"
                };

                _context.ChatHistorial.Add(historial);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
