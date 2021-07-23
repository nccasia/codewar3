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
    public class UserService : IUserService
    {
        private readonly MaiAnVatContext db;
        public UserService()
        {
            db = new MaiAnVatContext();
        }
        public User Create(User model)
        {
            db.User.Add(model);
            db.SaveChanges();
            return model;
        }

        public User Create()
        {
            throw new NotImplementedException();
        }

        public async Task<User> CreateAsync(User model)
        {
            db.User.Add(model);
            await db.SaveChangesAsync();
            return model;
        }

        public void Delete(int id)
        {
            var user = Read(id);
            if (user != null)
            {
                user.IsDeleted = true;
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var Job = await ReadAsync(id);
            if (Job != null)
            {
                Job.IsDeleted = true;
                await db.SaveChangesAsync();
            }
        }

        public IQueryable<User> Find(Expression<Func<User, bool>> whereExpression = null)
        {
            IQueryable<User> qUsers = db.User.Where(i => i.IsDeleted == false);
            if (whereExpression != null)
            {
                qUsers = qUsers.Where(whereExpression);
            }
            return qUsers;
        }

        public IQueryable<User> Find(string searchTerm, Expression<Func<User, bool>> whereExpression = null)
        {
            IQueryable<User> qUsers = Find(whereExpression);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                qUsers = qUsers.Where(i => i.UserName.Contains(searchTerm) || i.FirstName.Contains(searchTerm) || i.LastName.Contains(searchTerm) || i.Email.Contains(searchTerm) || i.PhoneNumber.Contains(searchTerm));
            }
            return qUsers;
        }

        public Task<IQueryable<User>> FindAsync(Expression<Func<User, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }
        public Task<IQueryable<User>> FindAsync(string searchTerm, Expression<Func<User, bool>> whereExpression = null)
        {
            throw new NotImplementedException();
        }

        public User Read(int id)
        {
            return db.User.FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> ReadAsync(int id)
        {
            return await db.User.FindAsync(id);
        }

        public void Update(int id, User entity)
        {
            var user = Read(id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.FirstName = entity.FirstName;
                user.LockoutEnabled = entity.LockoutEnabled;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;
                user.MobileNumber = entity.MobileNumber;
                user.Status = entity.Status;
                user.DateOfBirth = entity.DateOfBirth;
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(int id, User entity)
        {
            var user = await ReadAsync(id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.FirstName = entity.FirstName;
                user.LockoutEnabled = entity.LockoutEnabled;
                user.Email = entity.Email;
                user.PhoneNumber = entity.PhoneNumber;
                user.MobileNumber = entity.MobileNumber;
                user.Status = entity.Status;
                user.DateOfBirth = entity.DateOfBirth;
                await db.SaveChangesAsync();
            }
        }
    }
}
