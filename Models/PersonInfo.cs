using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
	public class PersonInfo
	{
		public int Id { get; set; }
		
		[Display(Name ="Name")]
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		[EmailAddress]
		[Required]
		public string Email { get; set; }
	}
}
