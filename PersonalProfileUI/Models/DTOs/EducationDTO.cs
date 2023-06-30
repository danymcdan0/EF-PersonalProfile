using System.ComponentModel.DataAnnotations;

namespace PersonalProfileUI.Models.DTOs
{
	public class EducationDTO
	{
		public Guid Id { get; set; }

		public string University { get; set; }

		public string Course { get; set; }

		public string Grade { get; set; }

        public string? Description { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

		//TODO make NYULLABUL
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; }
	}
}
