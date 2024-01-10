namespace WebFileAsses.Models
{
	public class FileItem
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public DateOnly Date { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public Priority? Priority { get; set; }
		public DateTime? Due_date { get; set; }
		public Guid UserID { get; set; }
	}
}
