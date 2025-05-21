using System.Diagnostics;
using HerreraSierraVargas_ChatBot.Models;
using HerreraSierraVargas_ChatBot.Repositories; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SierraHerreraVargasChatBot.Repositories;
using HerreraSierraVargas_ChatBot.Data;

namespace HerreraSierraVargas_ChatBot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeminiRepository _geminiRepository;
        private readonly GroqRepository _groqRepository;

        public HomeController(ILogger<HomeController> logger, ChatDbContext context)
        {
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
