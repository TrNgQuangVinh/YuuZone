using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Constant
{
    public static class ConstantEnum
    {
        public static class Roles
        {
            public const string ADMIN = "Admin";
            public const string CUSTOMER = "Customer";
        }
        
        public static class Statuses
        {
            public const string ACTIVE = "Active";
            public const string PENDING = "Pending";
            public const string INACTIVE = "Inactive";
        }

        public enum RoleID
        {
            ADMIN = 1,
            CUSTOMER = 2
        }
        public enum StatusID
        {
            ACTIVE = 1,
            PENDING = 2,
            INACTIVE = 3
        }
    }
}
