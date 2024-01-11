using System.ComponentModel.DataAnnotations;

namespace WebFileAsses.Models
{
    public class FileSet
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public byte Priority { get; set; }
        public DateTime? Due_date { get; set; }
        public Guid UserID { get; set; }
        public static implicit operator FileSet(FileItem file)
        {
            return new FileSet()
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
