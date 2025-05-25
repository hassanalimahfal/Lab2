using System.ComponentModel.DataAnnotations;

namespace Lab2.ViewModels
{
    public class UserVM
    {

        public string? Id {  get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        [MinLength(4)]  
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
        public string? Address { get; set; }
    }
}
