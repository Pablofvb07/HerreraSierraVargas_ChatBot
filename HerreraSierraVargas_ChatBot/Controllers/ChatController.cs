using HerreraSierraVargas_ChatBot.Repositories;
using Microsoft.AspNetCore.Mvc;
using HerreraSierraVargas_ChatBot.Models;
using SierraHerreraVargasChatBot.Repositories;
using System.Threading.Tasks;
using HerreraSierraVargas_ChatBot.Data;

namespace SierraHerreraVargasChatBot.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly GeminiRepository _geminiRepository;
        private readonly GroqRepository _groqRepository;

        public ChatBotController(ChatDbContext context)
        {
            _geminiRepository = new GeminiRepository(context);
            _groqRepository = new GroqRepository(context);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ChatBotViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChatBotViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Prompt) && !string.IsNullOrWhiteSpace(model.SelectedBot))
            {
                if (model.SelectedBot == "Gemini")
                {
                    model.GeminiResponse = await _geminiRepository.GetChatbotResponse(model.Prompt);
                    await _geminiRepository.SaveResponseInDatabase(model.Prompt, model.GeminiResponse);
                }
                else if (model.SelectedBot == "Groq")
                {
                    model.GroqResponse = await _groqRepository.GetChatbotResponse(model.Prompt);
                    await _groqRepository.SaveResponseInDatabase(model.Prompt, model.GroqResponse);
                }
            }

            return View(model);
        }

    }
}
