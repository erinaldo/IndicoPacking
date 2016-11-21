/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System.ComponentModel;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using Dapper;
using System.Collections.Generic;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public enum EntityState
    {
        Added = 1,
        New = 2,
        Deleted =3,
        Retrived = 4
    }

    public abstract class Entity
    {
		#region Protected Properties

        internal string TableName { get; set; }

        protected bool SouldNotifyPropertyChanges { get { return BusinessObjectState == EntityState.Retrived; } }

        #endregion

        #region Internal constructor

        internal Entity()
        {
            BusinessObjectState = EntityState.New;
        }

        #endregion

        #region Internal Properties

        internal object PrimaryKey { get; set; }

		internal string PrimaryKeyName { get; set; }

        internal IDbContext Context { get; set; }

        internal EntityState BusinessObjectState { get; set; }

		#endregion

        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

		#region Internal Methods

		internal virtual Dictionary<string, object> GetColumns()
        {
            return null;
        }

		internal virtual void Copy(Entity en)
		{
			return;
		}

		internal bool AreSame(Entity en)
		{
			if(PrimaryKey == null && en.PrimaryKey == null)
				return Equals(en);
			return TableName.Equals(en.TableName) && PrimaryKey.Equals(en.PrimaryKey);
		}

		#endregion
    }
}
