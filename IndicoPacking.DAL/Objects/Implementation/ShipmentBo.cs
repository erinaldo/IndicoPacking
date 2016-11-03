using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class ShipmentBo : Entity
	{
		#region Fields
		
		private int _weekNo;
		private DateTime _weekendDate;
		private int? _indicoWeeklyProductionCapacityID;

		#endregion

		#region Properties
		
		public int WeekNo { get { return _weekNo; } set { _weekNo = value; NotifyPropertyChanged("WeekNo");}}
		public DateTime WeekendDate { get { return _weekendDate; } set { _weekendDate = value; NotifyPropertyChanged("WeekendDate");}}
		public int? IndicoWeeklyProductionCapacityID { get { return _indicoWeeklyProductionCapacityID; } set { _indicoWeeklyProductionCapacityID = value; NotifyPropertyChanged("IndicoWeeklyProductionCapacityID");}}

		
		public List<ShipmentDetailBo> ShipmentDetailsWhereThisIsShipment => _context.Unit.ShipmentDetailRepository.Where(new {Shipment = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"WeekNo", WeekNo},
				{"WeekendDate", WeekendDate},
				{"IndicoWeeklyProductionCapacityID", IndicoWeeklyProductionCapacityID}
			};
        }

		#endregion
	}
} 

