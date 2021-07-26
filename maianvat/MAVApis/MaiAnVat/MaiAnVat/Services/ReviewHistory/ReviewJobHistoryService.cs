using MaiAnVat.Models;
using MaiAnVat.ServiceFramework.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class ReviewJobHistoryService : IReviewJobHistoryService
    {
        private readonly MaiAnVatContext db;
        public ReviewJobHistoryService()
        {
            db = new MaiAnVatContext();
        }
        public ReviewJobHistory Create(ReviewJobHistory model)
        {
            model.ReviewJobHistoryK = Guid.NewGuid();
            db.ReviewJobHistory.Add(model);
            db.SaveChanges();
            return model;
        }

        public ReviewJobHistory Create()
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewJobHistory> CreateAsync(ReviewJobHistory model)
        {
            model.ReviewJobHistoryK = Guid.NewGuid();
            db.ReviewJobHistory.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var ReviewJobHistory = Read(id);
            if (ReviewJobHistory != null)
            {
                ReviewJobHistory.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var ReviewJobHistory = await ReadAsync(id);
            if (ReviewJobHistory != null)
            {
                ReviewJobHistory.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<ReviewJobHistory> Find(Expression<Func<ReviewJobHistory, bool>> whereExpression = null)
        {
            IQueryable<ReviewJobHistory> qReviewJobHistorys = db.ReviewJobHistory.Include(x => x.RejectedReason).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qReviewJobHistorys = qReviewJobHistorys.Where(whereExpression);
            }
            return qReviewJobHistorys;
        }

        public IQueryable<ReviewJobHistory> Find(string searchTerm, Expression<Func<ReviewJobHistory, bool>> whereExpression = null)
        {
            IQueryable<ReviewJobHistory> qReviewJobHistorys = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qReviewJobHistorys = qReviewJobHistorys.Where(i => i.ReviewBy.Equals(searchTerm));
            }
            return qReviewJobHistorys;
        }

        public Task<IQueryable<ReviewJobHistory>> FindAsync(Expression<Func<ReviewJobHistory, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<ReviewJobHistory>> FindAsync(string searchTerm, Expression<Func<ReviewJobHistory, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public ReviewJobHistory Read(Guid id)
        {
            return db.ReviewJobHistory.FirstOrDefault(x => x.ReviewJobHistoryK == id);
        }

        public async Task<ReviewJobHistory> ReadAsync(Guid id)
        {
            return await db.ReviewJobHistory.FindAsync(id);
        }

        public void Update(Guid id, ReviewJobHistory entity)
        {
            var ReviewJobHistory = Read(id);
            if (ReviewJobHistory != null)
            {
                ReviewJobHistory.ModifiedByUserFk = entity.ModifiedByUserFk;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, ReviewJobHistory entity)
        {
            var ReviewJobHistory = await ReadAsync(id);
            if (ReviewJobHistory != null)
            {
                ReviewJobHistory.ModifiedByUserFk = entity.ModifiedByUserFk;
                await db.SaveChangesAsync();
            }
        }
    }
}
