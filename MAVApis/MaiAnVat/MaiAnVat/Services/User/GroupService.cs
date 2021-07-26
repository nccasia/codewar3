using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class GroupService : IGroupService
    {
        private readonly MaiAnVatContext db;
        public GroupService()
        {
            db = new MaiAnVatContext();
        }
        public Group Create(Group model)
        {
            model.GroupK = Guid.NewGuid();
            model.IsDeleted = false;
            db.Group.Add(model);
            db.SaveChanges();
            return model;
        }

        public Group Create()
        {
            throw new NotImplementedException();
        }

        public async Task<Group> CreateAsync(Group model)
        {
            model.GroupK = Guid.NewGuid();
            model.IsDeleted = false;
            db.Group.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var Group = Read(id);
            if (Group != null)
            {
                Group.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await ReadAsync(id);
            if (group != null)
            {
                group.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Group> Find(Expression<Func<Group, bool>> whereExpression = null)
        {
            IQueryable<Group> qGroups = db.Group.Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qGroups = qGroups.Where(whereExpression);
            }
            return qGroups;
        }

        public IQueryable<Group> Find(string searchTerm, Expression<Func<Group, bool>> whereExpression = null)
        {
            IQueryable<Group> qGroups = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qGroups = qGroups.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qGroups;
        }

        public Task<IQueryable<Group>> FindAsync(Expression<Func<Group, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Group>> FindAsync(string searchTerm, Expression<Func<Group, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Group Read(Guid id)
        {
            return db.Group.FirstOrDefault(x => x.GroupK == id);
        }

        public async Task<Group> ReadAsync(Guid id)
        {
            return await db.Group.FindAsync(id);
        }

        public void Update(Guid id, Group entity)
        {
            var group = Read(id);
            if (group != null)
            {
                group.IsDeleted = false;
                group.Status = entity.Status;
                group.Name = entity.Name;
                group.Description = entity.Description;
                group.ModifiedByUserFk = entity.ModifiedByUserFk;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, Group entity)
        {
            var group = await ReadAsync(id);
            if (group != null)
            {
                group.IsDeleted = false;
                group.Status = entity.Status;
                group.Name = entity.Name;
                group.Description = entity.Description;
                group.ModifiedByUserFk = entity.ModifiedByUserFk;
                await db.SaveChangesAsync();
            }
        }
    }
}
