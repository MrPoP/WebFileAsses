using Microsoft.AspNetCore.Mvc;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
	public class FileController : Controller
	{
		public List<FileItem> fileItems { get; set; } = new List<FileItem>();
		public IActionResult Index()
		{
			if(fileItems.Count == 0)
			{
				fileItems.Add(new FileItem() { ID = 0, Name = "TestFile", CreatedDate = DateTime.Now });
			}
			return View(fileItems);
		}
		public IActionResult Create(FileItem item)
		{
			string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads\";

            if (item != null)
			{
				if(item.File != null)
				{
					string PathToSave = Constants.GetSavePath(item.Priority);
					if (!Directory.Exists(PathToSave))
						Directory.CreateDirectory(PathToSave);
				}
			}
            return View();
		}
	}
}
