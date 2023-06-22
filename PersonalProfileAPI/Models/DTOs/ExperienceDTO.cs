namespace PersonalProfileAPI.Models.DTOs
{
    public class ExperienceDTO
    {
        public Guid Id { get; set; }

        public string Company { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
