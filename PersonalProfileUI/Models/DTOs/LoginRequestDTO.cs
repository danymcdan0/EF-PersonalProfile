using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalProfileUI.Models.DTOs
{
	public class LoginRequestDTO
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
