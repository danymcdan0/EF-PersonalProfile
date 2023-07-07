using System.ComponentModel.DataAnnotations;

namespace PersonalProfileAPI.Models.DTOs
{
    public class AddEducationDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "50 Characters Max")]
        public string University { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "50 Characters Max")]
        public string Course { get; set; }
        
        [Required]
        [MaxLength(10, ErrorMessage = "10 Characters Max")]
        public string Grade { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
