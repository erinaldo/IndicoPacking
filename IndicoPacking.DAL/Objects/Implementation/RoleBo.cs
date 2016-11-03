using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class RoleBo : Entity
	{
		#region Fields
		
		private string _key;
		private string _name;

		#endregion

		#region Properties
		
		public string Key { get { return _key; } set { _key = value; NotifyPropertyChanged("Key");}}
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}

		
		public List<UserBo> UsersWhereThisIsRole => _context.Unit.UserRepository.Where(new {Role = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Key", Key},
				{"Name", Name}
			};
        }

		#endregion
	}
} 

