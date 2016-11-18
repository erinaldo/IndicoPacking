/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    /// <summary>
    /// the base class for all Repositories . this contains generic functionalities of all repositories
    /// like Get,Add etc..
    /// </summary>
    /// <typeparam name="T">the type of the Entity</typeparam>
	public abstract class Repository<T> where T : Entity
    {
		#region Protected Fields

        protected IDbContext Context;

		#endregion

		#region Protected properties

		protected string TableName {get;set;}
		protected string PrimaryKeyName {get;set;}

		#endregion

		#region Public properties

        public int Count => Context.Count(TableName);

		#endregion

        #region Protected Constructors

		protected Repository(IDbContext context)
        {
            Context = context;
        }

		#endregion

		#region Public Methods

        public virtual T Get(int id)
        {
            return Context.Get<T>(id,TableName,PrimaryKeyName);
        }

        public List<T> Get()
        {
            return Context.Get<T>(TableName);
        }

        public T Add(T entity)
        {
            return Context.Add(entity,TableName,entity.GetColumns()) as T;
        }

		public void Delete(T entity)
        {
            Context.Delete<T>(entity,TableName, PrimaryKeyName);
        }

        public virtual void Delete(int id)
        {
            Context.Delete<T>(id,TableName, PrimaryKeyName);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            Context.DeleteRange<T>(entities,TableName, PrimaryKeyName);
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            return Context.Find(predicate,TableName);
        }

        public List<T> Where(object propertyValues)
        {
            return Context.Where<T>(propertyValues,TableName);
        }

		public List<T> GetPage(int pageNumber,int pageSize)
		{
			return Context.SelectPage<T>(TableName,pageSize,pageNumber,PrimaryKeyName);
		}

		#endregion
    }
}
