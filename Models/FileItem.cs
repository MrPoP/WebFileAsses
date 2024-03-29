﻿using System.ComponentModel.DataAnnotations;

namespace WebFileAsses.Models
{
	public class FileItem
	{
        public int ID { get; set; }
		public string? Name { get; set; }
		public DateTime Date { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public byte Priority { get; set; }
		public DateTime? Due_date { get; set; }
		public Guid UserID { get; set; }
        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = "File")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
        public static implicit operator FileItem(FileSet file)
        {
            return new FileItem()
            {
                ID = file.ID,
                Name = file.Name,
                Date = file.Date,
                CreatedDate = file.CreatedDate,
                Due_date = file.Due_date,
                Priority = file.Priority,
                UserID = file.UserID
            };
        }
    }
}
