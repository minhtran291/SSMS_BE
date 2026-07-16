using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Constant
{
    public static class UserRole
    {
        public const string CUSTOMER = "CUSTOMER";
        public const string SALES = "SALES";
        public const string MANAGER = "MANAGER";
        public const string ADMIN = "ADMIN";

        public static readonly string[] ALL =
        [
            CUSTOMER,
            SALES,
            MANAGER,
            ADMIN
        ];
    }
}
