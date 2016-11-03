using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class PortBo : Entity
	{
		#region Fields
		
		private string _name;
		private string _description;
		private int? _indicoPortId;

		#endregion

		#region Properties
		
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description");}}
		public int? IndicoPortId { get { return _indicoPortId; } set { _indicoPortId = value; NotifyPropertyChanged("IndicoPortId");}}

		
		public List<DistributorClientAddressBo> DistributorClientAddresssWhereThisIsPort => _context.Unit.DistributorClientAddressRepository.Where(new {Port = ID}).ToList();
		public List<InvoiceBo> InvoicesWhereThisIsPort => _context.Unit.InvoiceRepository.Where(new {Port = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Name", Name},
				{"Description", Description},
				{"IndicoPortId", IndicoPortId}
			};
        }

		#endregion
	}
} 

