using HerreraSierraVargas_ChatBot.Repositories;
using Microsoft.AspNetCore.Mvc;
using HerreraSierraVargas_ChatBot.Models;
using SierraHerreraVargasChatBot.Repositories;
using System.Threading.Tasks;

namespace SierraHerreraVargasChatBot.Controllers
{
    public class ChatBotController : Controller
    {
        private readonly GeminiRepository _geminiRepository;
        private readonly GroqRepository _groqRepository;

        public ChatBotController()
        {
            _geminiRepository = new GeminiRepository();
            _groqRepository = new GroqRepository();
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
                }
                else if (model.SelectedBot == "Groq")
                {
                    model.GroqResponse = await _groqRepository.GetChatbotResponse(model.Prompt);
                }
            }

            return View(model);
        }

    }
}
