using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Constant
{
    public static class ConstantEnum
    {
        public static class RepoStatus
        {
            public const string SUCCESS = "Success";
            public const string FAILURE = "Failure";
        }

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

        public enum InternalStatusID
        {
            SUCCESS = 0,
            FAILURE = -1
        }

        public enum StatusID
        {
            ACTIVE = 1,
            INACTIVE = 2,
            PENDING = 3
        }
    }
}
