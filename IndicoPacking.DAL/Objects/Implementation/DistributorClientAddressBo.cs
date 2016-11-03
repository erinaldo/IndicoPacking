using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class DistributorClientAddressBo : Entity
	{
		#region Fields
		
		private string _address;
		private string _suburb;
		private string _postCode;
		private CountryBo _objCountry;
		private int _country;
		private string _contactName;
		private string _contactPhone;
		private string _companyName;
		private string _state;
		private PortBo _objPort;
		private int? _port;
		private string _emailAddress;
		private int? _addressType;
		private bool _isAdelaideWarehouse;
		private int? _indicoDistributorClientAddressId;
		private string _distributorName;

		#endregion

		#region Properties
		
		public string Address { get { return _address; } set { _address = value; NotifyPropertyChanged("Address");}}
		public string Suburb { get { return _suburb; } set { _suburb = value; NotifyPropertyChanged("Suburb");}}
		public string PostCode { get { return _postCode; } set { _postCode = value; NotifyPropertyChanged("PostCode");}}
		public CountryBo ObjCountry
		{ 
			get 
			{ 
				return (Country<1) ? null : (_objCountry ?? (_objCountry = _context.Unit.CountryRepository.Get(Country)));
			}
			set { _objCountry = value; Country = value.ID; NotifyPropertyChanged("Country");}
		}
		public int Country { get { return _country; } set { _country = value; NotifyPropertyChanged("Country");}}
		public string ContactName { get { return _contactName; } set { _contactName = value; NotifyPropertyChanged("ContactName");}}
		public string ContactPhone { get { return _contactPhone; } set { _contactPhone = value; NotifyPropertyChanged("ContactPhone");}}
		public string CompanyName { get { return _companyName; } set { _companyName = value; NotifyPropertyChanged("CompanyName");}}
		public string State { get { return _state; } set { _state = value; NotifyPropertyChanged("State");}}
		public PortBo ObjPort
		{ 
			get 
			{ 
				return (Port.GetValueOrDefault()<1) ? null : (_objPort ?? (_objPort = _context.Unit.PortRepository.Get(Port.GetValueOrDefault())));
			}
			set { _objPort = value; Port = value.ID; NotifyPropertyChanged("Port");}
		}
		public int? Port { get { return _port; } set { _port = value; NotifyPropertyChanged("Port");}}
		public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; NotifyPropertyChanged("EmailAddress");}}
		public int? AddressType { get { return _addressType; } set { _addressType = value; NotifyPropertyChanged("AddressType");}}
		public bool IsAdelaideWarehouse { get { return _isAdelaideWarehouse; } set { _isAdelaideWarehouse = value; NotifyPropertyChanged("IsAdelaideWarehouse");}}
		public int? IndicoDistributorClientAddressId { get { return _indicoDistributorClientAddressId; } set { _indicoDistributorClientAddressId = value; NotifyPropertyChanged("IndicoDistributorClientAddressId");}}
		public string DistributorName { get { return _distributorName; } set { _distributorName = value; NotifyPropertyChanged("DistributorName");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsDistributorClientAddress => _context.Unit.InvoiceRepository.Where(new {DistributorClientAddress = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Address", Address},
				{"Suburb", Suburb},
				{"PostCode", PostCode},
				{"Country", Country},
				{"ContactName", ContactName},
				{"ContactPhone", ContactPhone},
				{"CompanyName", CompanyName},
				{"State", State},
				{"Port", Port},
				{"EmailAddress", EmailAddress},
				{"AddressType", AddressType},
				{"IsAdelaideWarehouse", IsAdelaideWarehouse},
				{"IndicoDistributorClientAddressId", IndicoDistributorClientAddressId},
				{"DistributorName", DistributorName}
			};
        }

		#endregion
	}
} 

