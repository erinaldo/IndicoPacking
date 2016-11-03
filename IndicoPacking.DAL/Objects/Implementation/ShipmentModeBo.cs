using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class ShipmentModeBo : Entity
	{
		#region Fields
		
		private string _name;
		private string _description;
		private int? _indicoShipmentModeId;

		#endregion

		#region Properties
		
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description");}}
		public int? IndicoShipmentModeId { get { return _indicoShipmentModeId; } set { _indicoShipmentModeId = value; NotifyPropertyChanged("IndicoShipmentModeId");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsShipmentMode => _context.Unit.InvoiceRepository.Where(new {ShipmentMode = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Name", Name},
				{"Description", Description},
				{"IndicoShipmentModeId", IndicoShipmentModeId}
			};
        }

		#endregion
	}
} 

