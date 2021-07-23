using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly MaiAnVatContext db;
        public PermissionService()
        {
            db = new MaiAnVatContext();
        }
        public Permission Create()
        {
            throw new NotImplementedException();
        }

        public Permission Create(Permission model)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> CreateAsync(Permission model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> Find(Expression<Func<Permission, bool>> whereExpression = null)
        {
            IQueryable<Permission> qGroups = db.Permission.Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qGroups = qGroups.Where(whereExpression);
            }
            return qGroups;
        }

        public IQueryable<Permission> Find(string searchTerm, Expression<Func<Permission, bool>> whereExpression = null)
        {
            IQueryable<Permission> qGroups = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qGroups = qGroups.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qGroups;
        }

        public Task<IQueryable<Permission>> FindAsync(Expression<Func<Permission, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Permission>> FindAsync(string searchTerm, Expression<Func<Permission, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Permission Read(Guid id)
        {
            return db.Permission.FirstOrDefault(x => x.PermissionK == id);
        }

        public async Task<Permission> ReadAsync(Guid id)
        {
            return await db.Permission.FindAsync(id);
        }


        public void Update(Guid id, Permission entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, Permission entity)
        {
            throw new NotImplementedException();
        }
    }
}
