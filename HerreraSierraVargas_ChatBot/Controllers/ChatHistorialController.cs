using Microsoft.AspNetCore.Mvc;
using HerreraSierraVargas_ChatBot.Models;

namespace HerreraSierraVargas_ChatBot.Controllers
{
    public class ChatHistorialController : Controller
    {
        public IActionResult Index()
        {
            // Aquí luego pones lógica para mostrar la lista de logs
            return View();
        }
    }
}
