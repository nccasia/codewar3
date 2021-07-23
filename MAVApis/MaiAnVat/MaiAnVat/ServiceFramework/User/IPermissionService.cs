using MaiAnVat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.ServiceFramework
{
    public interface IPermissionService : IService<Permission, Guid>
    {
    }
}
