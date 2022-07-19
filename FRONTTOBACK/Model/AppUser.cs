using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FRONTTOBACK.Model
{
    public class AppUser:IdentityUser
    {
        public string FullName{ get; set; }

        public List<Sale> Sales { get; set; }

        public List<SalesProduct> Salesproducts { get; set; }
    }
}
