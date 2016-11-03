using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class ShipmentDetailBo : Entity
	{
		#region Fields
		
		private ShipmentBo _objShipment;
		private int _shipment;
		private int _indicoDistributorClientAddress;
		private string _shipTo;
		private string _port;
		private string _shipmentMode;
		private string _priceTerm;
		private DateTime _eTD;
		private int _qty;
		private int _quantityFilled;
		private int _quantityYetToBeFilled;
		private string _invoiceNo;

		#endregion

		#region Properties
		
		public ShipmentBo ObjShipment
		{ 
			get 
			{ 
				return (Shipment<1) ? null : (_objShipment ?? (_objShipment = _context.Unit.ShipmentRepository.Get(Shipment)));
			}
			set { _objShipment = value; Shipment = value.ID; NotifyPropertyChanged("Shipment");}
		}
		public int Shipment { get { return _shipment; } set { _shipment = value; NotifyPropertyChanged("Shipment");}}
		public int IndicoDistributorClientAddress { get { return _indicoDistributorClientAddress; } set { _indicoDistributorClientAddress = value; NotifyPropertyChanged("IndicoDistributorClientAddress");}}
		public string ShipTo { get { return _shipTo; } set { _shipTo = value; NotifyPropertyChanged("ShipTo");}}
		public string Port { get { return _port; } set { _port = value; NotifyPropertyChanged("Port");}}
		public string ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value; NotifyPropertyChanged("ShipmentMode");}}
		public string PriceTerm { get { return _priceTerm; } set { _priceTerm = value; NotifyPropertyChanged("PriceTerm");}}
		public DateTime ETD { get { return _eTD; } set { _eTD = value; NotifyPropertyChanged("ETD");}}
		public int Qty { get { return _qty; } set { _qty = value; NotifyPropertyChanged("Qty");}}
		public int QuantityFilled { get { return _quantityFilled; } set { _quantityFilled = value; NotifyPropertyChanged("QuantityFilled");}}
		public int QuantityYetToBeFilled { get { return _quantityYetToBeFilled; } set { _quantityYetToBeFilled = value; NotifyPropertyChanged("QuantityYetToBeFilled");}}
		public string InvoiceNo { get { return _invoiceNo; } set { _invoiceNo = value; NotifyPropertyChanged("InvoiceNo");}}

		
		public List<InvoiceBo> InvoicesWhereThisIsShipmentDetail => _context.Unit.InvoiceRepository.Where(new {ShipmentDetail = ID}).ToList();
		public List<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsShipmentDetail => _context.Unit.OrderDeatilItemRepository.Where(new {ShipmentDetail = ID}).ToList();
		public List<ShipmentDetailCartonBo> ShipmentDetailCartonsWhereThisIsShipmentDetail => _context.Unit.ShipmentDetailCartonRepository.Where(new {ShipmentDetail = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"Shipment", Shipment},
				{"IndicoDistributorClientAddress", IndicoDistributorClientAddress},
				{"ShipTo", ShipTo},
				{"Port", Port},
				{"ShipmentMode", ShipmentMode},
				{"PriceTerm", PriceTerm},
				{"ETD", ETD},
				{"Qty", Qty},
				{"QuantityFilled", QuantityFilled},
				{"QuantityYetToBeFilled", QuantityYetToBeFilled},
				{"InvoiceNo", InvoiceNo}
			};
        }

		#endregion
	}
} 

