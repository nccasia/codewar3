using MaiAnVat.Models;
using MaiAnVat.ServiceFramework.JobAndJobType;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services.JobAndJobType
{
    public class JobTypeService : IJobTypeService
    {
        private readonly MaiAnVatContext db;
        public JobTypeService()
        {
            db = new MaiAnVatContext();
        }
        public JobType Create(JobType model)
        {
            model.JobTypeK = Guid.NewGuid();
            var jobWFS = db.JobWorkFlowStatus.FirstOrDefault(x => x.JobTypeWorkFlowFk == model.JobTypeK);
            if(jobWFS != default) 
                model.DefaultWorkFlowFk = jobWFS.JobTypeWorkFlowFk;
            db.JobType.Add(model);
            db.SaveChanges();
            return model;
        }

        public JobType Create()
        {
            throw new NotImplementedException();
        }

        public async Task<JobType> CreateAsync(JobType model)
        {
            model.JobTypeK = Guid.NewGuid();
            db.JobType.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var jobtype = Read(id);
            if (jobtype != null)
            {
                jobtype.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var jobtype = await ReadAsync(id);
            if (jobtype != null)
            {
                jobtype.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<JobType> Find(Expression<Func<JobType, bool>> whereExpression = null)
        {
            IQueryable<JobType> qJobtypes = db.JobType.Include(x => x.Job).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qJobtypes = qJobtypes.Where(whereExpression);
            }
            return qJobtypes;
        }

        public IQueryable<JobType> Find(string searchTerm, Expression<Func<JobType, bool>> whereExpression = null)
        {
            IQueryable<JobType> qJobtypes = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qJobtypes = qJobtypes.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qJobtypes;
        }

        public Task<IQueryable<JobType>> FindAsync(Expression<Func<JobType, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<JobType>> FindAsync(string searchTerm, Expression<Func<JobType, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public JobType Read(Guid id)
        {
            return db.JobType.FirstOrDefault(x => x.JobTypeK == id);
        }

        public async Task<JobType> ReadAsync(Guid id)
        {
            return await db.JobType.FindAsync(id);
        }

        public void Update(Guid id, JobType entity)
        {
            var jobtype = Read(id);
            if (jobtype != null)
            {
                jobtype.IsException = entity.IsException;
                jobtype.DefaultTimeInHours = entity.DefaultTimeInHours;
                jobtype.Name = entity.Name;
                jobtype.Description = entity.Description;
                jobtype.ColorCode = entity.ColorCode;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, JobType entity)
        {
            var jobtype = await ReadAsync(id);
            if (jobtype != null)
            {
                jobtype.IsException = entity.IsException;
                jobtype.DefaultTimeInHours = entity.DefaultTimeInHours;
                jobtype.Name = entity.Name;
                jobtype.Description = entity.Description;
                jobtype.ColorCode = entity.ColorCode;
                await db.SaveChangesAsync();
            }
        }
    }
}
