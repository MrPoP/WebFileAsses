using Microsoft.AspNetCore.Mvc;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
    public class UserController : Controller
    {
        private string email = "1", password = "1", guid = "1";
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
            if (user.Email != null)
            {
                email = user.Email;
                password = user.Password;
                guid = Constants.GenericStorageName();
                return RedirectToAction("Index", "Home");
            }
            return View("Register");
        }
        public IActionResult Login(User user)
        {
            if(user == null)
            {
                return View("Index");
            }
            if (user.Email == email && user.Password == password)
            {
                HttpContext.Session.SetString("UserID", guid);
                return RedirectToAction("Index", "File");
            }
            return View("Login");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserID");
            return RedirectToAction("Index", "Home");
        }
    }
}
