using Microsoft.AspNetCore.Mvc;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserID") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }
        public IActionResult Register(User user)
        {
            if (HttpContext.Session.GetString("UserID") != null)
                return RedirectToAction("Index", "Home");
            if (user == null)
                return View("Register");
            return View("Register");
        }
        public IActionResult Login(User user)
        {
            if(user == null)
            {
                return View("Index");
            }
            HttpContext.Session.SetString("UserID", null);
            return RedirectToAction("Index", "File");
        }
    }
}
