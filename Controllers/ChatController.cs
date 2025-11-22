using Microsoft.AspNetCore.Mvc;

namespace EphemeralRealTimeChatApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("username");
            return View(model: email);
        }
    }
}
