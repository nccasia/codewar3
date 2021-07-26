using Abp.Authorization;
using AutoGenerateTestcase.Authorization.Roles;
using AutoGenerateTestcase.Authorization.Users;

namespace AutoGenerateTestcase.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
