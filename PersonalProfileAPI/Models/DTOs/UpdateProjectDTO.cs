using System.ComponentModel.DataAnnotations;

namespace PersonalProfileAPI.Models.DTOs
{
    public class UpdateProjectDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "50 Character Max")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "500 Character Max")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "100 Character Max")]
        public string Aim { get; set; }

        public string? ImageUrl { get; set; }
    }
}
