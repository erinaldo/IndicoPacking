using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class InvoiceStatusBo : Entity
	{
		#region Fields
		
		private string _key;
		private string _name;

		#endregion

		#region Properties
		
		public string Key { get { return _key; } set { _key = value; NotifyPropertyChanged("Key");}}
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsInvoiceStatus => _context.Unit.InvoiceRepository.Where(new {InvoiceStatus = ID}).ToList();

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

