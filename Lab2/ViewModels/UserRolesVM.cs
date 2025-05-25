namespace Lab2.ViewModels
{
    public class UserRolesVM
    {
        public UserRolesVM()
        {
            Roles = new List<RoleVM>();
        }
        public string UserId { get; set; } = null!;
     
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;

        public List<RoleVM> Roles { get; set; }

    }
}
