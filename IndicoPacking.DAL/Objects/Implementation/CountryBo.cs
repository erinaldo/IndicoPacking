/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Country table in the database 
    /// </summary>
	public class CountryBo : Entity
	{
		#region Fields
		
		private int _iD;
		private string _iso2;
		private string _iso3;
		private int _isoCountryNumber;
		private int? _dialingPrefix;
		private string _name;
		private string _shortName;
		private bool _hasLocationData;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public string Iso2 { get { return _iso2; } set { _iso2 = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Iso2"); }}}
		public string Iso3 { get { return _iso3; } set { _iso3 = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Iso3"); }}}
		public int IsoCountryNumber { get { return _isoCountryNumber; } set { _isoCountryNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IsoCountryNumber"); }}}
		public int? DialingPrefix { get { return _dialingPrefix; } set { _dialingPrefix = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DialingPrefix"); }}}
		public string Name { get { return _name; } set { _name = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Name"); }}}
		public string ShortName { get { return _shortName; } set { _shortName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShortName"); }}}
		public bool HasLocationData { get { return _hasLocationData; } set { _hasLocationData = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("HasLocationData"); }}}

		#endregion

		#region Methods
				
		public BoCollection<BankBo> BanksWhereThisIsCountry()
		{
			 List<BankBo> list;
			 try { list = Context.Unit.BankRepository.Where(new {Country = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<BankBo>(this, list, "Country");
		}

		
		public BoCollection<DistributorClientAddressBo> DistributorClientAddresssWhereThisIsCountry()
		{
			 List<DistributorClientAddressBo> list;
			 try { list = Context.Unit.DistributorClientAddressRepository.Where(new {Country = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<DistributorClientAddressBo>(this, list, "Country");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"Iso2", Iso2},
				{"Iso3", Iso3},
				{"IsoCountryNumber", IsoCountryNumber},
				{"DialingPrefix", DialingPrefix},
				{"Name", Name},
				{"ShortName", ShortName},
				{"HasLocationData", HasLocationData}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as CountryBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_iso2 = entity._iso2;
			_iso3 = entity._iso3;
			_isoCountryNumber = entity._isoCountryNumber;
			_dialingPrefix = entity._dialingPrefix;
			_name = entity._name;
			_shortName = entity._shortName;
			_hasLocationData = entity._hasLocationData;
		}

		#endregion

		#region Constructors
		
		public CountryBo()
		{
			TableName = "Country";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

