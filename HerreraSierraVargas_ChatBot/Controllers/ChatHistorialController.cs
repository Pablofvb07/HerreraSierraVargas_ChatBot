using Microsoft.AspNetCore.Mvc;
using HerreraSierraVargas_ChatBot.Data;
using HerreraSierraVargas_ChatBot.Models;
using System.Linq;

namespace HerreraSierraVargas_ChatBot.Controllers
{
    public class ChatHistorialController : Controller
    {
        private readonly ChatDbContext _context;

        public ChatHistorialController(ChatDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var historial = _context.ChatHistorial.OrderByDescending(x => x.Timestamp).ToList();
            return View(historial);
        }
    }
}
