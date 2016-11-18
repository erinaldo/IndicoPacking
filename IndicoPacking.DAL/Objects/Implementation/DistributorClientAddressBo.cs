/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the DistributorClientAddress table in the database 
    /// </summary>
	public class DistributorClientAddressBo : Entity
	{
		#region Fields
		
		private int _iD;
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
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Address { get { return _address; } set { _address = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Address"); }}}
		public string Suburb { get { return _suburb; } set { _suburb = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Suburb"); }}}
		public string PostCode { get { return _postCode; } set { _postCode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PostCode"); }}}
		public CountryBo ObjCountry
		{ 
			get 
			{ 
				return (Country<1) ? null : (_objCountry ?? (_objCountry = Context.Unit.CountryRepository.Get(Country)));
			}
			set { _objCountry = value; Country = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Country"); }}
		}
		public int Country { get { return _country; } set { _country = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Country"); }}}
		public string ContactName { get { return _contactName; } set { _contactName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ContactName"); }}}
		public string ContactPhone { get { return _contactPhone; } set { _contactPhone = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ContactPhone"); }}}
		public string CompanyName { get { return _companyName; } set { _companyName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("CompanyName"); }}}
		public string State { get { return _state; } set { _state = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("State"); }}}
		public PortBo ObjPort
		{ 
			get 
			{ 
				return (Port.GetValueOrDefault()<1) ? null : (_objPort ?? (_objPort = Context.Unit.PortRepository.Get(Port.GetValueOrDefault())));
			}
			set { _objPort = value; Port = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Port"); }}
		}
		public int? Port { get { return _port; } set { _port = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Port"); }}}
		public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("EmailAddress"); }}}
		public int? AddressType { get { return _addressType; } set { _addressType = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("AddressType"); }}}
		public bool IsAdelaideWarehouse { get { return _isAdelaideWarehouse; } set { _isAdelaideWarehouse = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IsAdelaideWarehouse"); }}}
		public int? IndicoDistributorClientAddressId { get { return _indicoDistributorClientAddressId; } set { _indicoDistributorClientAddressId = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoDistributorClientAddressId"); }}}
		public string DistributorName { get { return _distributorName; } set { _distributorName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DistributorName"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsDistributorClientAddress()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {DistributorClientAddress = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "DistributorClientAddress");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
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

		internal override void Copy(Entity en)
		{
			var entity = en as DistributorClientAddressBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_address = entity._address;
			_suburb = entity._suburb;
			_postCode = entity._postCode;
			_country = entity._country;
			_objCountry = entity._objCountry;
			_contactName = entity._contactName;
			_contactPhone = entity._contactPhone;
			_companyName = entity._companyName;
			_state = entity._state;
			_port = entity._port;
			_objPort = entity._objPort;
			_emailAddress = entity._emailAddress;
			_addressType = entity._addressType;
			_isAdelaideWarehouse = entity._isAdelaideWarehouse;
			_indicoDistributorClientAddressId = entity._indicoDistributorClientAddressId;
			_distributorName = entity._distributorName;
		}

		#endregion

		#region Constructors
		
		public DistributorClientAddressBo()
		{
			TableName = "DistributorClientAddress";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

