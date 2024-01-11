using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFileAsses.Models;

namespace WebFileAsses.Controllers
{
	public class FileController : Controller
	{
        private readonly DatabaseContext _myDbContext = new DatabaseContext();
        public List<FileItem> fileItems { get; set; } = new List<FileItem>();
		public IActionResult Index()
		{
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
			string userstr = HttpContext.Session.GetString("UserID");
			Guid User = Guid.Parse(userstr);
            var items = _myDbContext.Files.Where(p => p.UserID == User).ToList();
            foreach(var item in items)
            {
                FileItem itemf = item;
                fileItems.Add(itemf);
            }
			return View(fileItems);
		}
        public IActionResult Edit(int ID, FileItem item)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
            string userstr = HttpContext.Session.GetString("UserID");
            Guid User = Guid.Parse(userstr);
            var items = _myDbContext.Files.Where(p => p.UserID == User).ToList();
            var sitem = items.Where(p => p.ID == ID).FirstOrDefault();
            if (item.Due_date == null)
            {
                FileItem fitem = sitem;
                return View(fitem);
            }
            else
            {
                string ItemName = sitem.Name;
                _myDbContext.Files.Remove(sitem);
                sitem = item;
                sitem.Name = ItemName;
                _myDbContext.Files.Add(sitem);
                _myDbContext.SaveChanges();
                return RedirectToAction("Index", "File");
            }
        }
        public IActionResult View(int ID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
            string userstr = HttpContext.Session.GetString("UserID");
            Guid User = Guid.Parse(userstr);
            var items = _myDbContext.Files.Where(p => p.UserID == User).ToList();
            var item = items.Where(p => p.ID == ID).FirstOrDefault();
            if (item != null)
            {
                FileItem fitem = item;
                return View(fitem);
            }
            return RedirectToAction("Index", "File");
        }
        public IActionResult Delete(int ID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
            string userstr = HttpContext.Session.GetString("UserID");
            Guid User = Guid.Parse(userstr);
            var items = _myDbContext.Files.Where(p => p.UserID == User).ToList();
            var item = items.Where(p => p.ID == ID).FirstOrDefault();
            if (item != null)
            {
                string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads" + Constants.GetSavePath(item.Priority) + item.Name;
                System.IO.File.Delete(str);
                _myDbContext.Files.Remove(item);
                _myDbContext.SaveChanges();
            }
            return RedirectToAction("Index", "File");
        }
        public IActionResult Download(int ID)
        {
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
            string userstr = HttpContext.Session.GetString("UserID");
            Guid User = Guid.Parse(userstr);
            var items = _myDbContext.Files.Where(p => p.UserID == User).ToList();
            var item = items.Where(p => p.ID == ID).FirstOrDefault();
            if(item != null)
            {
                string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads" + Constants.GetSavePath(item.Priority) + item.Name;
                return PhysicalFile(str, "application/octet-stream", item.Name);
            }
            return RedirectToAction("Index", "File");
        }

        public IActionResult Create(FileItem item)
		{
            if (HttpContext.Session.GetString("UserID") == null)
            {
                return RedirectToAction("Index", "User");
            }
            string userstr = HttpContext.Session.GetString("UserID");
            Guid User = Guid.Parse(userstr);
            string str = Directory.GetCurrentDirectory() + @"\wwwroot\Uploads";

            if (item != null)
			{
				if(item.File != null)
				{
                    var PathToSave = str + Constants.GetSavePath(item.Priority);
					if (!Directory.Exists(PathToSave))
						Directory.CreateDirectory(PathToSave);
                    item.Name = item.File.FileName; // Path.GetFileNameWithoutExtension(item.File.FileName);
					//string NewFileName = Constants.GenericStorageName() + Path.GetExtension(item.File.FileName);
                    using var stream = new FileStream(PathToSave + item.File.FileName, FileMode.Create);
                    item.File.CopyTo(stream);
                    item.UserID = User;
                    FileSet dbItem = item;
                    _myDbContext.Files.Add(dbItem);
                    _myDbContext.SaveChanges();
                    return RedirectToAction("Index", "File");
                }
			}
            return View();
        }
	}
}
