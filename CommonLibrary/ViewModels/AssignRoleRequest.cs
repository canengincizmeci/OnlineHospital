using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class AssignRoleRequest
    {
        public string UserId { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
