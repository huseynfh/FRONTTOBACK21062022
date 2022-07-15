using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FRONTTOBACK.ViewModel
{
    public class RoleVM
    {
        public string FullName { get; set; }

        public List<IdentityRole> roles { get; set; }

        public IList<string> userRoles { get; set; }

        public string UserId { get; set; }
    }
}
