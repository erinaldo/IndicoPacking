using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class CartonBo : Entity
	{
		#region Fields
		
		private string _name;
		private int _qty;
		private string _description;

		#endregion

		#region Properties
		
		public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name");}}
		public int Qty { get { return _qty; } set { _qty = value; NotifyPropertyChanged("Qty");}}
		public string Description { get { return _description; } set { _description = value; NotifyPropertyChanged("Description");}}

		
		public List<ShipmentDetailCartonBo> ShipmentDetailCartonsWhereThisIsCarton => _context.Unit.ShipmentDetailCartonRepository.Where(new {Carton = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Name", Name},
				{"Qty", Qty},
				{"Description", Description}
			};
        }

		#endregion
	}
} 

