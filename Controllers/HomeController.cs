using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
	public class HomeController : Controller
	{
		private readonly Priority[] priorities = new[]
		{
			new Priority(){ID = 0, Name = "Low", FilePath = "/Low/"},
			new Priority(){ID = 1, Name = "High", FilePath = "/High/"},
			new Priority(){ID = 2, Name = "Critical", FilePath = "/Critical/"},
		};
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public ActionResult<IEnumerable<Priority>> Getpriorities()
		{
			return Ok(priorities.AsEnumerable());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
