using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class WorkFlowStatusService : IWorkFlowStatusService
    {
        private readonly MaiAnVatContext db;
        public WorkFlowStatusService()
        {
            db = new MaiAnVatContext();
        }
        public WorkFlowStatus Create(WorkFlowStatus model)
        {
            model.WorkFlowStatusK = Guid.NewGuid();
            db.WorkFlowStatus.Add(model);
            db.SaveChanges();
            return model;
        }

        public WorkFlowStatus Create()
        {
            throw new NotImplementedException();
        }

        public async Task<WorkFlowStatus> CreateAsync(WorkFlowStatus model)
        {
            model.WorkFlowStatusK = Guid.NewGuid();
            db.WorkFlowStatus.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var WorkFlowStatus = Read(id);
            if (WorkFlowStatus != null)
            {
                WorkFlowStatus.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var WorkFlowStatus = await ReadAsync(id);
            if (WorkFlowStatus != null)
            {
                WorkFlowStatus.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<WorkFlowStatus> Find(Expression<Func<WorkFlowStatus, bool>> whereExpression = null)
        {
            IQueryable<WorkFlowStatus> qWorkFlowStatuss = db.WorkFlowStatus.Include(x => x.JobAssignmentListStatus).Include(x => x.Job).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qWorkFlowStatuss = qWorkFlowStatuss.Where(whereExpression);
            }
            return qWorkFlowStatuss;
        }

        public IQueryable<WorkFlowStatus> Find(string searchTerm, Expression<Func<WorkFlowStatus, bool>> whereExpression = null)
        {
            IQueryable<WorkFlowStatus> qWorkFlowStatuss = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qWorkFlowStatuss = qWorkFlowStatuss.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qWorkFlowStatuss;
        }

        public Task<IQueryable<WorkFlowStatus>> FindAsync(Expression<Func<WorkFlowStatus, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<WorkFlowStatus>> FindAsync(string searchTerm, Expression<Func<WorkFlowStatus, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public WorkFlowStatus Read(Guid id)
        {
            return db.WorkFlowStatus.FirstOrDefault(x => x.WorkFlowStatusK == id);
        }

        public async Task<WorkFlowStatus> ReadAsync(Guid id)
        {
            return await db.WorkFlowStatus.FindAsync(id);
        }

        public void Update(Guid id, WorkFlowStatus entity)
        {
            var WorkFlowStatus = Read(id);
            if (WorkFlowStatus != null)
            {
                WorkFlowStatus.Name = entity.Name;
                WorkFlowStatus.Description = entity.Description;
                WorkFlowStatus.StatusCode = entity.StatusCode;
                WorkFlowStatus.PreviousStatusK = entity.PreviousStatusK;
                WorkFlowStatus.IsDeleted = entity.IsDeleted;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, WorkFlowStatus entity)
        {
            var WorkFlowStatus = await ReadAsync(id);
            if (WorkFlowStatus != null)
            {
                WorkFlowStatus.Name = entity.Name;
                WorkFlowStatus.Description = entity.Description;
                WorkFlowStatus.StatusCode = entity.StatusCode;
                WorkFlowStatus.PreviousStatusK = entity.PreviousStatusK;
                WorkFlowStatus.IsDeleted = entity.IsDeleted;
                await db.SaveChangesAsync();
            }
        }
    }
}
