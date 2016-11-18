/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the ShipmentDetailCarton table in the database 
    /// </summary>
	public class ShipmentDetailCartonBo : Entity
	{
		#region Fields
		
		private int _iD;
		private ShipmentDetailBo _objShipmentDetail;
		private int _shipmentDetail;
		private CartonBo _objCarton;
		private int _carton;
		private int _number;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public ShipmentDetailBo ObjShipmentDetail
		{ 
			get 
			{ 
				return (ShipmentDetail<1) ? null : (_objShipmentDetail ?? (_objShipmentDetail = Context.Unit.ShipmentDetailRepository.Get(ShipmentDetail)));
			}
			set { _objShipmentDetail = value; ShipmentDetail = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDetail"); }}
		}
		public int ShipmentDetail { get { return _shipmentDetail; } set { _shipmentDetail = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDetail"); }}}
		public CartonBo ObjCarton
		{ 
			get 
			{ 
				return (Carton<1) ? null : (_objCarton ?? (_objCarton = Context.Unit.CartonRepository.Get(Carton)));
			}
			set { _objCarton = value; Carton = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Carton"); }}
		}
		public int Carton { get { return _carton; } set { _carton = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Carton"); }}}
		public int Number { get { return _number; } set { _number = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Number"); }}}

		#endregion

		#region Methods
				
		public BoCollection<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsShipmentDetailCarton()
		{
			 List<OrderDeatilItemBo> list;
			 try { list = Context.Unit.OrderDeatilItemRepository.Where(new {ShipmentDetailCarton = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<OrderDeatilItemBo>(this, list, "ShipmentDetailCarton");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"ShipmentDetail", ShipmentDetail},
				{"Carton", Carton},
				{"Number", Number}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as ShipmentDetailCartonBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_shipmentDetail = entity._shipmentDetail;
			_objShipmentDetail = entity._objShipmentDetail;
			_carton = entity._carton;
			_objCarton = entity._objCarton;
			_number = entity._number;
		}

		#endregion

		#region Constructors
		
		public ShipmentDetailCartonBo()
		{
			TableName = "ShipmentDetailCarton";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

