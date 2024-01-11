using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _myDbContext = new DatabaseContext();
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
                user.Guid = Guid.NewGuid();
                _myDbContext.Users.Add(user);
                _myDbContext.SaveChanges();
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
            if (user.Email != null && user.Password != null)
            {
                var fuser = _myDbContext.Users.Where(p => p.Email == user.Email && p.Password == user.Password).FirstOrDefault();
                if (fuser != null)
                {
                    HttpContext.Session.SetString("UserID", fuser.Guid.ToString());
                    return RedirectToAction("Index", "File");
                }
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
