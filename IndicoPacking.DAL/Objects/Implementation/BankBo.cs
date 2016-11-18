/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Bank table in the database 
    /// </summary>
	public class BankBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _name;
		private string _accountNo;
		private string _branch;
		private string _number;
		private string _address;
		private string _city;
		private string _state;
		private string _postcode;
		private CountryBo _objCountry;
		private int? _country;
		private string _swiftCode;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public string AccountNo { get { return _accountNo; } set { _accountNo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("AccountNo"); }}}
		public string Branch { get { return _branch; } set { _branch = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Branch"); }}}
		public string Number { get { return _number; } set { _number = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Number"); }}}
		public string Address { get { return _address; } set { _address = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Address"); }}}
		public string City { get { return _city; } set { _city = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("City"); }}}
		public string State { get { return _state; } set { _state = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("State"); }}}
		public string Postcode { get { return _postcode; } set { _postcode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Postcode"); }}}
		public CountryBo ObjCountry
		{ 
			get 
			{ 
				return (Country.GetValueOrDefault()<1) ? null : (_objCountry ?? (_objCountry = Context.Unit.CountryRepository.Get(Country.GetValueOrDefault())));
			}
			set { _objCountry = value; Country = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Country"); }}
		}
		public int? Country { get { return _country; } set { _country = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Country"); }}}
		public string SwiftCode { get { return _swiftCode; } set { _swiftCode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SwiftCode"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsBank()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {Bank = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "Bank");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Name", Name},
				{"AccountNo", AccountNo},
				{"Branch", Branch},
				{"Number", Number},
				{"Address", Address},
				{"City", City},
				{"State", State},
				{"Postcode", Postcode},
				{"Country", Country},
				{"SwiftCode", SwiftCode}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as BankBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_name = entity._name;
			_accountNo = entity._accountNo;
			_branch = entity._branch;
			_number = entity._number;
			_address = entity._address;
			_city = entity._city;
			_state = entity._state;
			_postcode = entity._postcode;
			_country = entity._country;
			_objCountry = entity._objCountry;
			_swiftCode = entity._swiftCode;
		}

		#endregion

		#region Constructors
		
		public BankBo()
		{
			TableName = "Bank";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

