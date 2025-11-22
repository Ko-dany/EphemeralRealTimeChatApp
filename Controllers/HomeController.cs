using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EphemeralRealTimeChatApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("auth/login")]
        public IActionResult GoogleLogin(string returnUrl = "/chat")
        {
            HttpContext.Session.Clear();
            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl
            };
            props.Items["prompt"] = "select_account";
            return Challenge(props, "Google");
        }

        [HttpGet("auth/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            var props = new AuthenticationProperties
            {
                RedirectUri = "/"
            };
            return SignOut(props, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("auth/denied")]
        public IActionResult AccessDenied()
        {
            return Content("Access Denied");
        }
    }
}
