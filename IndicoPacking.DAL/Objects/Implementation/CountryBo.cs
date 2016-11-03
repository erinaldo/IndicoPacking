using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class CountryBo : Entity
	{
		#region Fields
		
		private string _iso2;
		private string _iso3;
		private int _isoCountryNumber;
		private int? _dialingPrefix;
		private string _name;
		private string _shortName;
		private bool _hasLocationData;

		#endregion

		#region Properties
		
		public string Iso2 { get { return _iso2; } set { _iso2 = value; NotifyPropertyChanged("Iso2");}}
		public string Iso3 { get { return _iso3; } set { _iso3 = value; NotifyPropertyChanged("Iso3");}}
		public int IsoCountryNumber { get { return _isoCountryNumber; } set { _isoCountryNumber = value; NotifyPropertyChanged("IsoCountryNumber");}}
		public int? DialingPrefix { get { return _dialingPrefix; } set { _dialingPrefix = value; NotifyPropertyChanged("DialingPrefix");}}
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public string ShortName { get { return _shortName; } set { _shortName = value; NotifyPropertyChanged("ShortName");}}
		public bool HasLocationData { get { return _hasLocationData; } set { _hasLocationData = value; NotifyPropertyChanged("HasLocationData");}}

		
		public List<BankBo> BanksWhereThisIsCountry => _context.Unit.BankRepository.Where(new {Country = ID}).ToList();
		public List<DistributorClientAddressBo> DistributorClientAddresssWhereThisIsCountry => _context.Unit.DistributorClientAddressRepository.Where(new {Country = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Iso2", Iso2},
				{"Iso3", Iso3},
				{"IsoCountryNumber", IsoCountryNumber},
				{"DialingPrefix", DialingPrefix},
				{"Name", Name},
				{"ShortName", ShortName},
				{"HasLocationData", HasLocationData}
			};
        }

		#endregion
	}
} 

