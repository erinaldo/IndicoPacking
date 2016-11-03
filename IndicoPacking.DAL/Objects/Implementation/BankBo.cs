using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class BankBo : Entity
	{
		#region Fields
		
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
		
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public string AccountNo { get { return _accountNo; } set { _accountNo = value; NotifyPropertyChanged("AccountNo");}}
		public string Branch { get { return _branch; } set { _branch = value; NotifyPropertyChanged("Branch");}}
		public string Number { get { return _number; } set { _number = value; NotifyPropertyChanged("Number");}}
		public string Address { get { return _address; } set { _address = value; NotifyPropertyChanged("Address");}}
		public string City { get { return _city; } set { _city = value; NotifyPropertyChanged("City");}}
		public string State { get { return _state; } set { _state = value; NotifyPropertyChanged("State");}}
		public string Postcode { get { return _postcode; } set { _postcode = value; NotifyPropertyChanged("Postcode");}}
		public CountryBo ObjCountry
		{ 
			get 
			{ 
				return (Country.GetValueOrDefault()<1) ? null : (_objCountry ?? (_objCountry = _context.Unit.CountryRepository.Get(Country.GetValueOrDefault())));
			}
			set { _objCountry = value; Country = value.ID; NotifyPropertyChanged("Country");}
		}
		public int? Country { get { return _country; } set { _country = value; NotifyPropertyChanged("Country");}}
		public string SwiftCode { get { return _swiftCode; } set { _swiftCode = value; NotifyPropertyChanged("SwiftCode");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsBank => _context.Unit.InvoiceRepository.Where(new {Bank = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
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

		#endregion
	}
} 

