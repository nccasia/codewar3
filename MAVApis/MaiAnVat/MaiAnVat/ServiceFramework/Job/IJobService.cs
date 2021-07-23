using MaiAnVat.Models;
using System;
using System.Threading.Tasks;

namespace MaiAnVat.ServiceFramework.JobAndJobType
{
    public interface IJobService : IService<Job, Guid>
    {
        Task<Job> SubmitAsync(Job entity);
    }
}
