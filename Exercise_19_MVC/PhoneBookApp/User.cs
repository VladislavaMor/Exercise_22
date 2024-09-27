using Microsoft.AspNetCore.Identity;

namespace Exercise_21
{
    public class User : IdentityUser
    {
        public Role Role { get; set; }
    }
    public class Role : IdentityRole
    {
    }

}
