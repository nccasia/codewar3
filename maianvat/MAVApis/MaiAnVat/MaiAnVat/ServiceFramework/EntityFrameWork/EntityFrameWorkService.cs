using MaiAnVat.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MaiAnVat.ServiceFramework
{
    /// <summary>
    /// Framework which provides an entity framework repository around the ICoreDBUnitOfWork pattern
    /// </summary>
    public abstract class EntityFrameworkService<TContext> : Framework.ServiceBase
        where TContext : class
    {
        protected TContext Context => context as TContext;

        /// <summary>
        /// Initializes a new Entity Framework repostory with the specified entity
        /// framework context.  If the context is not provided, a new one is created.
        /// </summary>
        /// <param name="uow">
        /// A Database unit of work context that contains the backing store for the repository.
        /// </param>
        protected EntityFrameworkService([RequestScope] TContext uow)
            : base()
        {
            context = uow;
        }

        public virtual void SaveChanges()
        {
            (Context as DbContext).SaveChanges();
        }
    }
}
