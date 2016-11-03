using System.ComponentModel;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Core;
using Dapper;
using System.Collections.Generic;

namespace IndicoPacking.DAL.Objects.Implementation
{
    public class Entity:IEntity
    {
        public int ID { get; set; }
        public IDbContext _context { get; set; }

        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

		#region Methods

		public virtual Dictionary<string, object> GetColumns()
        {
            return null;
        }

		#endregion
    }
}
