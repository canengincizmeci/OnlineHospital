using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class AppUser : IdentityUser
    {

        public bool HasRole { get; set; } = false;
        public bool ActivityStatus { get; set; } = true;
    }
}
