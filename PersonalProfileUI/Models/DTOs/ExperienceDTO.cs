using System.ComponentModel.DataAnnotations;

namespace PersonalProfileUI.Models.DTOs
{
    public class ExperienceDTO
    {
        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }

		//TODO make NYULLABUL
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime EndDate { get; set; }
	}
}
