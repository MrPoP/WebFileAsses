using System.ComponentModel.DataAnnotations;

namespace WebFileAsses.Models
{
	public class User
	{
        [Key]
        public Guid Guid { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Address { get; set; }
	}
}
