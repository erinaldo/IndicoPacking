/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the User table in the database 
    /// </summary>
	public class UserBo : Entity
	{
		#region Fields
		
		private int _iD;
		private UserStatusBo _objStatus;
		private int _status;
		private RoleBo _objRole;
		private int _role;
		private string _username;
		private string _password;
		private string _passwordSalt;
		private string _name;
		private string _emailAddress;
		private string _mobileTelephoneNumber;
		private string _officeTelephoneNumber;
		private DateTime? _dateLastLogin;
		private DateTime _createdDate;
		private bool _genderMale;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public UserStatusBo ObjStatus
		{ 
			get 
			{ 
				return (Status<1) ? null : (_objStatus ?? (_objStatus = Context.Unit.UserStatusRepository.Get(Status)));
			}
			set { _objStatus = value; Status = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}
		}
		public int Status { get { return _status; } set { _status = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}}
		public RoleBo ObjRole
		{ 
			get 
			{ 
				return (Role<1) ? null : (_objRole ?? (_objRole = Context.Unit.RoleRepository.Get(Role)));
			}
			set { _objRole = value; Role = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Role"); }}
		}
		public int Role { get { return _role; } set { _role = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Role"); }}}
		public string Username { get { return _username; } set { _username = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Username"); }}}
		public string Password { get { return _password; } set { _password = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Password"); }}}
		public string PasswordSalt { get { return _passwordSalt; } set { _passwordSalt = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PasswordSalt"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("EmailAddress"); }}}
		public string MobileTelephoneNumber { get { return _mobileTelephoneNumber; } set { _mobileTelephoneNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("MobileTelephoneNumber"); }}}
		public string OfficeTelephoneNumber { get { return _officeTelephoneNumber; } set { _officeTelephoneNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OfficeTelephoneNumber"); }}}
		public DateTime? DateLastLogin { get { return _dateLastLogin; } set { _dateLastLogin = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DateLastLogin"); }}}
		public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("CreatedDate"); }}}
		public bool GenderMale { get { return _genderMale; } set { _genderMale = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("GenderMale"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsUser()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {User = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "User");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Status", Status},
				{"Role", Role},
				{"Username", Username},
				{"Password", Password},
				{"PasswordSalt", PasswordSalt},
				{"Name", Name},
				{"EmailAddress", EmailAddress},
				{"MobileTelephoneNumber", MobileTelephoneNumber},
				{"OfficeTelephoneNumber", OfficeTelephoneNumber},
				{"DateLastLogin", DateLastLogin},
				{"CreatedDate", CreatedDate},
				{"GenderMale", GenderMale}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as UserBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_status = entity._status;
			_objStatus = entity._objStatus;
			_role = entity._role;
			_objRole = entity._objRole;
			_username = entity._username;
			_password = entity._password;
			_passwordSalt = entity._passwordSalt;
			_name = entity._name;
			_emailAddress = entity._emailAddress;
			_mobileTelephoneNumber = entity._mobileTelephoneNumber;
			_officeTelephoneNumber = entity._officeTelephoneNumber;
			_dateLastLogin = entity._dateLastLogin;
			_createdDate = entity._createdDate;
			_genderMale = entity._genderMale;
		}

		#endregion

		#region Constructors
		
		public UserBo()
		{
			TableName = "User";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

