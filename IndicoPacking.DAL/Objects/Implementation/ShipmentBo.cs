/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Shipment table in the database 
    /// </summary>
	public class ShipmentBo : Entity
	{
		#region Fields
		
		private int _iD;
		private int _weekNo;
		private DateTime _weekendDate;
		private int? _indicoWeeklyProductionCapacityID;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public int WeekNo { get { return _weekNo; } set { _weekNo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("WeekNo"); }}}
		public DateTime WeekendDate { get { return _weekendDate; } set { _weekendDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("WeekendDate"); }}}
		public int? IndicoWeeklyProductionCapacityID { get { return _indicoWeeklyProductionCapacityID; } set { _indicoWeeklyProductionCapacityID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoWeeklyProductionCapacityID"); }}}

		#endregion

		#region Methods
				
		public BoCollection<ShipmentDetailBo> ShipmentDetailsWhereThisIsShipment()
		{
			 List<ShipmentDetailBo> list;
			 try { list = Context.Unit.ShipmentDetailRepository.Where(new {Shipment = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<ShipmentDetailBo>(this, list, "Shipment");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"WeekNo", WeekNo},
				{"WeekendDate", WeekendDate},
				{"IndicoWeeklyProductionCapacityID", IndicoWeeklyProductionCapacityID}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as ShipmentBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_weekNo = entity._weekNo;
			_weekendDate = entity._weekendDate;
			_indicoWeeklyProductionCapacityID = entity._indicoWeeklyProductionCapacityID;
		}

		#endregion

		#region Constructors
		
		public ShipmentBo()
		{
			TableName = "Shipment";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

