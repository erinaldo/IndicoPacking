/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the UserStatus table in the database 
    /// </summary>
	public class UserStatusBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _key;
		private string _name;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Key { get { return _key; } set { _key = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Key"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}

		#endregion

		#region Methods
				
		public BoCollection<UserBo> UsersWhereThisIsUserStatus()
		{
			 List<UserBo> list;
			 try { list = Context.Unit.UserRepository.Where(new {UserStatus = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<UserBo>(this, list, "UserStatus");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Key", Key},
				{"Name", Name}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as UserStatusBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_key = entity._key;
			_name = entity._name;
		}

		#endregion

		#region Constructors
		
		public UserStatusBo()
		{
			TableName = "UserStatus";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

