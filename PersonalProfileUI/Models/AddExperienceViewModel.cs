using System.ComponentModel.DataAnnotations;

namespace PersonalProfileUI.Models
{
	public class AddExperienceViewModel
	{
		[Required]
		[MaxLength(50, ErrorMessage = "50 Characters Max")]
		public string Company { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "50 Characters Max")]
		public string Role { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "50 Characters Max")]
		public string Description { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? EndDate { get; set; }
	}
}
