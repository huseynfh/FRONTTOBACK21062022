using Microsoft.AspNetCore.Identity;

namespace FRONTTOBACK.Model
{
    public class AppUser:IdentityUser
    {
        public string FullName{ get; set; } 
    }
}
