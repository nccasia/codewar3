using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MaiAnVatContext db;
        public CategoryService()
        {
            db = new MaiAnVatContext();
        }
        public Category Create(Category model)
        {
            model.CategoryK = Guid.NewGuid();
            db.Category.Add(model);
            db.SaveChanges();
            return model;
        }

        public Category Create()
        {
            throw new NotImplementedException();
        }

        public async Task<Category> CreateAsync(Category model)
        {
            model.CategoryK = Guid.NewGuid();
            db.Category.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(Guid id)
        {
            var Category = Read(id);
            if (Category != null)
            {
                Category.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var Category = await ReadAsync(id);
            if (Category != null)
            {
                Category.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Category> Find(Expression<Func<Category, bool>> whereExpression = null)
        {
            IQueryable<Category> qCategorys = db.Category.Include(x => x.CategoryClassification).Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qCategorys = qCategorys.Where(whereExpression);
            }
            return qCategorys;
        }

        public IQueryable<Category> Find(string searchTerm, Expression<Func<Category, bool>> whereExpression = null)
        {
            IQueryable<Category> qCategorys = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qCategorys = qCategorys.Where(i => i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }
            return qCategorys;
        }

        public Task<IQueryable<Category>> FindAsync(Expression<Func<Category, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<Category>> FindAsync(string searchTerm, Expression<Func<Category, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public Category Read(Guid id)
        {
            return db.Category.FirstOrDefault(x => x.CategoryK == id);
        }

        public async Task<Category> ReadAsync(Guid id)
        {
            return await db.Category.FindAsync(id);
        }

        public void Update(Guid id, Category entity)
        {
            var Category = Read(id);
            if (Category != null)
            {
                Category.Name = entity.Name;
                Category.Description = entity.Description;
                Category.IsDeleted = entity.IsDeleted;
                Category.IsDisabled = entity.IsDisabled;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Guid id, Category entity)
        {
            var Category = await ReadAsync(id);
            if (Category != null)
            {
                Category.Name = entity.Name;
                Category.Description = entity.Description;
                Category.IsDisabled = entity.IsDisabled;
                Category.IsDeleted = entity.IsDeleted;
                await db.SaveChangesAsync();
            }
        }
    }
}
