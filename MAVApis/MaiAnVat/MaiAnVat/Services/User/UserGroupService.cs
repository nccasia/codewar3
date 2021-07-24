using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly MaiAnVatContext db;
        public UserGroupService()
        {
            db = new MaiAnVatContext();
        }
        public UserGroup Create(UserGroup model)
        {
            model.UserGroupK = Guid.NewGuid();
            model.IsDeleted = false;
            db.UserGroup.Add(model);
            db.SaveChanges();
            return model;
        }

        public UserGroup Create()
        {
            throw new NotImplementedException();
        }

        public async Task<UserGroup> CreateAsync(UserGroup model)
        {
            model.UserGroupK = Guid.NewGuid();
            model.IsDeleted = false;
            db.UserGroup.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var UserGroup = Read(id);
            if (UserGroup != null)
            {
                UserGroup.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var UserGroup = await ReadAsync(id);
            if (UserGroup != null)
            {
                UserGroup.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<UserGroup> Find(Expression<Func<UserGroup, bool>> whereExpression = null)
        {
            IQueryable<UserGroup> qUserGroups = db.UserGroup.Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qUserGroups = qUserGroups.Where(whereExpression);
            }
            return qUserGroups;
        }

        public IQueryable<UserGroup> Find(string searchTerm, Expression<Func<UserGroup, bool>> whereExpression = null)
        {
            IQueryable<UserGroup> qUserGroups = Find(whereExpression);

            return qUserGroups;
        }

        public Task<IQueryable<UserGroup>> FindAsync(Expression<Func<UserGroup, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<UserGroup>> FindAsync(string searchTerm, Expression<Func<UserGroup, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public UserGroup Read(Guid id)
        {
            return db.UserGroup.FirstOrDefault(x => x.UserGroupK == id);
        }

        public async Task<UserGroup> ReadAsync(Guid id)
        {
            return await db.UserGroup.FindAsync(id);
        }

        public void Update(Guid id, UserGroup entity)
        {
            var UserGroup = Read(id);
            if (UserGroup != null)
            {
                UserGroup.IsDeleted = false;
                UserGroup.GroupFk = entity.GroupFk;
                UserGroup.UserFk = entity.UserFk;
                UserGroup.ModifiedByUserFk = entity.ModifiedByUserFk;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, UserGroup entity)
        {
            var UserGroup = await ReadAsync(id);
            if (UserGroup != null)
            {
                UserGroup.IsDeleted = false;
                UserGroup.GroupFk = entity.GroupFk;
                UserGroup.UserFk = entity.UserFk;
                UserGroup.ModifiedByUserFk = entity.ModifiedByUserFk;
                await db.SaveChangesAsync();
            }
        }
    }
}
