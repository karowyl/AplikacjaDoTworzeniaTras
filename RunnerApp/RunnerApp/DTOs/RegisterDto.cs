using System.ComponentModel.DataAnnotations;

namespace RunnerApp.DTOs
{
    public class RegisterDto
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
        

    }
}
