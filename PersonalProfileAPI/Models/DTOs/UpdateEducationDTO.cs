using System.ComponentModel.DataAnnotations;

namespace PersonalProfileAPI.Models.DTOs
{
    public class UpdateEducationDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "100 Characters Max")]
        public string University { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 Characters Max")]
        public string Course { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "10 Characters Max")]
        public string Grade { get; set; }

        [MaxLength(200, ErrorMessage = "200 Characters Max")]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
