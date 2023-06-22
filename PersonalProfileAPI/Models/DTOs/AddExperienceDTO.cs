using System.ComponentModel.DataAnnotations;

namespace PersonalProfileAPI.Models.DTOs
{
    public class AddExperienceDTO
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

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
