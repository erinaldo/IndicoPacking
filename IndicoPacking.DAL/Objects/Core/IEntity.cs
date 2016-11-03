using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using IndicoPacking.DAL.Base.Core;

namespace IndicoPacking.DAL.Objects.Core
{
    public interface IEntity : INotifyPropertyChanged
    {
        IDbContext _context { get; set; }
        int ID { get; set; }
		Dictionary<string, object> GetColumns();
    }
}
