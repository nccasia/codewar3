using Abp.Authorization;
using DRIMA.Authorization.Roles;
using DRIMA.Authorization.Users;

namespace DRIMA.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
