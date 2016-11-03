using System;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Core;
using IndicoPacking.DAL.Objects.Implementation;

namespace IndicoPacking.DAL.Base.Core
{
    public interface IDbContext : IDisposable
    {
        void SaveChanges();
        T Get<T>(int id,string tableName) where T : class, IEntity;
        IEnumerable<T> Get<T>(string tableName) where T : class, IEntity;
        int? Add(IEntity entity,string tableName,Dictionary<string,object> columns);
        void DeleteRange<T>(IEnumerable<IEntity> entities, string tableName) where T: class,IEntity;
        IEnumerable<T> Find<T>(Func<T, bool> predicate, string tableName) where T : class, IEntity;
		IEnumerable<T> Where<T>(object values,string tableName) where T : class, IEntity;
        int Count(string tableName);
		void Delete<T>(IEntity entity,string tableName) where T: class,IEntity;
        void Delete<T>(int id,string tableName) where T: class,IEntity;
		List<T> QueryView<T>(string viewname, object where = null) where T : class;
		List<T> Query<T>(string sql) where T : class;
		IUnitOfWork Unit { get; set;}
    }
}
