using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
			string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads";

            if (item != null)
			{
				if(item.File != null)
				{
                    var PathToSave = str + Constants.GetSavePath(item.Priority);
					if (!Directory.Exists(PathToSave))
						Directory.CreateDirectory(PathToSave);
					item.Name = Path.GetFileNameWithoutExtension(item.File.FileName);
					string NewFileName = Constants.GenericStorageName() + Path.GetExtension(item.File.FileName);
                    using var stream = new FileStream(PathToSave + NewFileName, FileMode.Create);
                    item.File.CopyTo(stream);
					return View("Index");
                }
			}
            return View();
		}
	}
}
