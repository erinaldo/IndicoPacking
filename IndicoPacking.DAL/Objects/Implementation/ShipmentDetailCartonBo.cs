using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class ShipmentDetailCartonBo : Entity
	{
		#region Fields
		
		private ShipmentDetailBo _objShipmentDetail;
		private int _shipmentDetail;
		private CartonBo _objCarton;
		private int _carton;
		private int _number;

		#endregion

		#region Properties
		
		public ShipmentDetailBo ObjShipmentDetail
		{ 
			get 
			{ 
				return (ShipmentDetail<1) ? null : (_objShipmentDetail ?? (_objShipmentDetail = _context.Unit.ShipmentDetailRepository.Get(ShipmentDetail)));
			}
			set { _objShipmentDetail = value; ShipmentDetail = value.ID; NotifyPropertyChanged("ShipmentDetail");}
		}
		public int ShipmentDetail { get { return _shipmentDetail; } set { _shipmentDetail = value; NotifyPropertyChanged("ShipmentDetail");}}
		public CartonBo ObjCarton
		{ 
			get 
			{ 
				return (Carton<1) ? null : (_objCarton ?? (_objCarton = _context.Unit.CartonRepository.Get(Carton)));
			}
			set { _objCarton = value; Carton = value.ID; NotifyPropertyChanged("Carton");}
		}
		public int Carton { get { return _carton; } set { _carton = value; NotifyPropertyChanged("Carton");}}
		public int Number { get { return _number; } set { _number = value; NotifyPropertyChanged("Number");}}

		
		public List<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsShipmentDetailCarton => _context.Unit.OrderDeatilItemRepository.Where(new {ShipmentDetailCarton = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ShipmentDetail", ShipmentDetail},
				{"Carton", Carton},
				{"Number", Number}
			};
        }

		#endregion
	}
} 

