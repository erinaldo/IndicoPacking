using System;
using System.Collections.Generic;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Core;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    /// <summary>
    /// the base class for all Repositories . this contains generic functionalities of all repositories
    /// like Get,Add etc..
    /// </summary>
    /// <typeparam name="T">the type of the Entity</typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected IDbContext Context;

		protected string TableName {get;set;}

        public int Count => Context.Count(TableName);

        protected Repository(IDbContext context)
        {
            Context = context;
        }

        public T Get(int id)
        {
            return Context.Get<T>(id,TableName);
        }

        public IEnumerable<T> Get()
        {
            return Context.Get<T>(TableName);
        }

        public int? Add(T entity)
        {
            return Context.Add(entity,TableName,entity.GetColumns());
        }

		public void Delete(T entity)
        {
            Context.Delete<T>(entity,TableName);
        }

        public void Delete(int id)
        {
            Context.Delete<T>(id,TableName);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            Context.DeleteRange<T>(entities,TableName);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Context.Find(predicate,TableName);
        }

        public IEnumerable<T> Where(object propertyValues)
        {
            return Context.Where<T>(propertyValues,TableName);
        }

    }
}
