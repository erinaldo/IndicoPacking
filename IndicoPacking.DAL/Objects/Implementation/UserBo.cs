using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class UserBo : Entity
	{
		#region Fields
		
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
		
		public UserStatusBo ObjStatus
		{ 
			get 
			{ 
				return (Status<1) ? null : (_objStatus ?? (_objStatus = _context.Unit.UserStatusRepository.Get(Status)));
			}
			set { _objStatus = value; Status = value.ID; NotifyPropertyChanged("Status");}
		}
		public int Status { get { return _status; } set { _status = value; NotifyPropertyChanged("Status");}}
		public RoleBo ObjRole
		{ 
			get 
			{ 
				return (Role<1) ? null : (_objRole ?? (_objRole = _context.Unit.RoleRepository.Get(Role)));
			}
			set { _objRole = value; Role = value.ID; NotifyPropertyChanged("Role");}
		}
		public int Role { get { return _role; } set { _role = value; NotifyPropertyChanged("Role");}}
		public string Username { get { return _username; } set { _username = value; NotifyPropertyChanged("Username");}}
		public string Password { get { return _password; } set { _password = value; NotifyPropertyChanged("Password");}}
		public string PasswordSalt { get { return _passwordSalt; } set { _passwordSalt = value; NotifyPropertyChanged("PasswordSalt");}}
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; NotifyPropertyChanged("EmailAddress");}}
		public string MobileTelephoneNumber { get { return _mobileTelephoneNumber; } set { _mobileTelephoneNumber = value; NotifyPropertyChanged("MobileTelephoneNumber");}}
		public string OfficeTelephoneNumber { get { return _officeTelephoneNumber; } set { _officeTelephoneNumber = value; NotifyPropertyChanged("OfficeTelephoneNumber");}}
		public DateTime? DateLastLogin { get { return _dateLastLogin; } set { _dateLastLogin = value; NotifyPropertyChanged("DateLastLogin");}}
		public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; NotifyPropertyChanged("CreatedDate");}}
		public bool GenderMale { get { return _genderMale; } set { _genderMale = value; NotifyPropertyChanged("GenderMale");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsUser => _context.Unit.InvoiceRepository.Where(new {User = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
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

		#endregion
	}
} 

