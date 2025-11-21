using Microsoft.AspNetCore.Mvc;

namespace EphemeralRealTimeChatApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
