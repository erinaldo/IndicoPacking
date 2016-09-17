using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.Common
{
    class LoginInfo
    {
        public static int UserID = 1;
        public static UserType Role;
    }

    public enum UserType
    {
        AppAdmin = 1,
        IndimanAdmin = 2,
        JkAdmin = 3,
        FillingCordinator = 4
    }
}
