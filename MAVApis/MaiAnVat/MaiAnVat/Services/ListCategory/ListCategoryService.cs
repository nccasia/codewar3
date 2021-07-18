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
    public class ListCategoryService : IListCategoryService
    {
        private readonly MaiAnVatContext db;
        public ListCategoryService()
        {
            db = new MaiAnVatContext();
        }
        public ListCategory Create(ListCategory model)
        {
            model.ListCategoryK = Guid.NewGuid();
            db.ListCategory.Add(model);
            db.SaveChanges();
            return model;
        }

        public ListCategory Create()
        {
            throw new NotImplementedException();
        }

        public async Task<ListCategory> CreateAsync(ListCategory model)
        {
            model.ListCategoryK = Guid.NewGuid();
            db.ListCategory.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var ListCategory = Read(id);
            if (ListCategory != null)
            {
                ListCategory.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var ListCategory = await ReadAsync(id);
            if (ListCategory != null)
            {
                ListCategory.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<ListCategory> Find(Expression<Func<ListCategory, bool>> whereExpression = null)
        {
            IQueryable<ListCategory> qListCategorys = db.ListCategory.Include(x => x.Job).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qListCategorys = qListCategorys.Where(whereExpression);
            }
            return qListCategorys;
        }

        public IQueryable<ListCategory> Find(string searchTerm, Expression<Func<ListCategory, bool>> whereExpression = null)
        {
            IQueryable<ListCategory> qListCategorys = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qListCategorys = qListCategorys.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qListCategorys;
        }

        public Task<IQueryable<ListCategory>> FindAsync(Expression<Func<ListCategory, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<ListCategory>> FindAsync(string searchTerm, Expression<Func<ListCategory, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public ListCategory Read(Guid id)
        {
            return db.ListCategory.FirstOrDefault(x => x.ListCategoryK == id);
        }

        public async Task<ListCategory> ReadAsync(Guid id)
        {
            return await db.ListCategory.FindAsync(id);
        }

        public void Update(Guid id, ListCategory entity)
        {
            var ListCategory = Read(id);
            if (ListCategory != null)
            {
                ListCategory.Name = entity.Name;
                ListCategory.Description = entity.Description;
                ListCategory.CategoryFk = entity.CategoryFk;
                ListCategory.IsDisabled = entity.IsDisabled;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, ListCategory entity)
        {
            var ListCategory = await ReadAsync(id);
            if (ListCategory != null)
            {
                ListCategory.Name = entity.Name;
                ListCategory.Description = entity.Description;
                ListCategory.CategoryFk = entity.CategoryFk;
                ListCategory.IsDisabled = entity.IsDisabled;
                await db.SaveChangesAsync();
            }
        }
    }
}
