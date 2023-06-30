using System.ComponentModel.DataAnnotations;

namespace PersonalProfileUI.Models
{
    public class AddEducationViewModel
    {
        public string University { get; set; }

        public string Course { get; set; }

        public string Grade { get; set; }

        public string? Description { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? EndDate { get; set; }
	}
}
