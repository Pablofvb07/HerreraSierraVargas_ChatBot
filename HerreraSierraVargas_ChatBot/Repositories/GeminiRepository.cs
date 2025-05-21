using HerreraSierraVargas_ChatBot.Data;
using HerreraSierraVargas_ChatBot.Interfaces;
using HerreraSierraVargas_ChatBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SierraHerreraVargasChatBot.Repositories
{
    public class GeminiRepository : IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly ChatDbContext _context;
        private readonly string GeminiApiKey = "AIzaSyBp0QFHAWHqzais6-MtlNZ6tCp4wQz1Z8k";

        public GeminiRepository(ChatDbContext context)
        {
            _httpClient = new HttpClient();
            _context = context;
        }

        public async Task<string> GetChatbotResponse(string prompt)
        {
            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={GeminiApiKey}";

                GeminiRequest request = new GeminiRequest
                {
                    contents = new List<GeminiContent>
                    {
                        new GeminiContent
                        {
                            parts = new List<GeminiPart>
                            {
                                new GeminiPart { text = prompt }
                            }
                        }
                    }
                };

                string jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error {response.StatusCode}: {await response.Content.ReadAsStringAsync()}";
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                string text = data.candidates[0].content.parts[0].text;

                return text;
            }
            catch (Exception ex)
            {
                return $"Ocurrió un error: {ex.Message}";
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
                    BotName = "Gemini"
                };

                _context.ChatHistorial.Add(historial);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Aquí podrías loguear el error si deseas
                return false;
            }
        }
    }
}
