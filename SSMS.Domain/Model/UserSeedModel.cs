using SSMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Model
{
    public class UserSeedModel
    {
        //public string UserName { get; set; } = null!;
        //public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public GenderType Gender { get; set; }
        //public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
