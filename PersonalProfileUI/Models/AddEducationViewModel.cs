namespace PersonalProfileUI.Models
{
    public class AddEducationViewModel
    {
        public string University { get; set; }

        public string Course { get; set; }

        public string Grade { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
