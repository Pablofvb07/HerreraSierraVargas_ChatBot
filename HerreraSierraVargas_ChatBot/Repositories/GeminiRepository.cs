using HerreraSierraVargas_ChatBot.Interfaces;
using HerreraSierraVargas_ChatBot.Models;
using Newtonsoft.Json;
using HerreraSierraVargas_ChatBot.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SierraHerreraVargasChatBot.Repositories
{
    public class GeminiRepository : IChatBotService
    {
        private HttpClient _httpClient;
        private string GeminiApiKey = "AIzaSyBp0QFHAWHqzais6-MtlNZ6tCp4wQz1Z8k";

        public GeminiRepository()
        {
            _httpClient = new HttpClient();
            _context = context;
        }

        public async Task<string> GetChatbotResponse(string prompt)
        {
            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + GeminiApiKey;

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
            var jsonResponse = await response.Content.ReadAsStringAsync();

            try
            {
                dynamic data = JsonConvert.DeserializeObject(jsonResponse);
                string text = data.candidates[0].content.parts[0].text;
                return text;
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
