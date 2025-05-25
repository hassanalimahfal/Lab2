using Microsoft.AspNetCore.Identity;

namespace Lab2.Models
{
    public class SysUser: IdentityUser
    {
        public string FullName { get; set;} = null!;
        public string? Address { get; set; }    
    }
}
