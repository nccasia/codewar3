using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaiAnVat.ServiceFramework
{
    public interface IService<TEntity> : IService<TEntity, int>
    where TEntity : class
    {
    }

    /// <summary>
    /// Interface that defines behaviour that a service must implement
    /// to be recognized as a service.
    /// </summary>
    /// <remarks>
    /// Must implement IDisposable, forcing service implementations to implement disposing functionality.
    /// </remarks>
    public interface IService<TEntity, in TKey>
        where TEntity : class
        where TKey : struct
    {
        #region Create
        /// <summary>
        /// Creates a single <typeparamref name="TEntity"/> and adds it to the 
        /// service's context.
        /// </summary>
        /// <returns>
        /// A <typeparamref name="TEntity"/> entity object that has been added 
        /// to the database may be assigned to.
        /// </returns>
        TEntity Create();
        Task<TEntity> CreateAsync(TEntity model);

        /// <summary>
        /// Creates a single <typeparamref name="TEntity"/> from the supplied
        /// model.
        /// </summary>
        /// <param name="model">
        /// A new <typeparamref name="TEntity"/> object to be added to the
        /// database context
        /// </param>
        /// <returns>
        /// The <typeparamref name="TEntity"/> model object which was added to
        /// the context.  The primary key for this object will be made available
        /// for use after the commit.  Add foreign related items using the 
        /// navigation properties for this object.
        /// </returns>
        TEntity Create(TEntity model);
        #endregion

        #region Read
        /// <summary>
        /// Reads a single <typeparamref name="TEntity"/> object from the database
        /// via its primary key.
        /// </summary>
        /// <param name="id">
        /// A key relating to the primary key of <typeparamref name="TEntity"/>.
        /// </param>
        /// <returns>
        /// A <typeparamref name="TEntity"/> object if one was found in the database by
        /// its primary key, null if none was found, or an Exception if there was an 
        /// error.
        /// </returns>
        TEntity Read(TKey id);
        Task<TEntity> ReadAsync(TKey id);

        #endregion

        #region Update
        /// <summary>
        /// Updates a single <typeparamref name="TEntity"/> object from the database via
        /// its primary key.
        /// </summary>
        /// <param name="id">
        /// A key relating to the primary key of the <typeparamref name="TEntity"/> that is
        /// to be deleted.
        /// </param>
        /// /// <param name="jsonObject">
        /// A JSON object to update properties on the specified entity.
        /// </param>
        void Update(TKey id, TEntity entity);
        Task UpdateAsync(TKey id, TEntity entity);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a single <typeparamref name="TEntity"/> object from the database via
        /// its primary key.  For objects that have an IsDeleted column, IsDeleted will
        /// be set in lieu of actual deletion in order to support recovery of deleted
        /// items.
        /// </summary>
        /// <param name="id">
        /// A key relating to the primary key of the <typeparamref name="TEntity"/> that is
        /// to be deleted.
        /// </param>
        void Delete(TKey id);
        Task DeleteAsync(TKey id);

        #endregion

        #region Search
        /// <summary>
        /// Finds a list of <typeparamref name="TEntity"/> objects in the database via the
        /// LINQ predicate specified.  Use the equivalent of .Where() to the <paramref name="whereExpression"/>
        /// parameter.
        /// </summary>
        /// <param name="whereExpression">
        /// A LINQ-style where expression to filter the list of <typeparamref name="TEntity"/>
        /// objects on in the database.
        /// </param>
        /// <returns>
        /// An IQueryable object of <typeparamref name="TEntities"/> containing a list of all
        /// the objects which match the supplied LINQ predicate.
        /// </returns>
        /// <remarks>
        /// This function must not return items that are marked as deleted.
        /// </remarks>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> whereExpression = null);
        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereExpression = null);

        /// <summary>
        /// Finds a list of <typeparamref name="TEntity"/> objects in the database via the
        /// search term string, and optionally the LINQ predicate specified.  Use the 
        /// equivalent of .Where() to the <paramref name="whereExpression"/>
        /// parameter.
        /// </summary>
        /// <param name="searchTerm">
        /// Used for string-based searching
        /// </param>
        /// <param name="whereExpression">
        /// A LINQ-style where expression to filter the list of <typeparamref name="TEntity"/>
        /// objects on in the database.
        /// </param>
        /// <returns>
        /// An IQueryable object of <typeparamref name="TEntities"/> containing a list of all
        /// the objects which match the supplied LINQ predicate.
        /// </returns>
        Task<IQueryable<TEntity>> FindAsync(string searchTerm, Expression<Func<TEntity, bool>> whereExpression = null);
        IQueryable<TEntity> Find(string searchTerm, Expression<Func<TEntity, bool>> whereExpression = null);
        #endregion
    }
}
