using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class UsersWithoutRoleViewModel
    {
        public string userId { get; set; } = null!;
        public string userName { get; set; } = null!;
        public List<string>? Roles { get; set; }



    }
}
