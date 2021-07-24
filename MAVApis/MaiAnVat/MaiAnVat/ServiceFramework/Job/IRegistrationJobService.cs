using MaiAnVat.Models;
using System;
using System.Threading.Tasks;

namespace MaiAnVat.ServiceFramework.JobAndJobType
{
    public interface IRegistrationJobService : IService<RegistrationJob, Guid>
    {
        Task ApprovedRegistrationAsync(Guid id, RegistrationJob entity);
    }
}
