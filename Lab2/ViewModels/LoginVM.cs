using System.ComponentModel.DataAnnotations;

namespace Lab2.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        public bool RememeberMe { get; set; } = false;
    }
}
