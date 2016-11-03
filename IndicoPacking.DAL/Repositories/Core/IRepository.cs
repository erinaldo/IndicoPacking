using System;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Core;

namespace IndicoPacking.DAL.Repositories.Core
{
    public interface IRepository<T> where T : IEntity
    {
        int Count { get; }
        T Get(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Find(Func<T, bool> predicate);
        int? Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void DeleteRange(IEnumerable<T> ids);
        IEnumerable<T> Where(object values);
    }
}
