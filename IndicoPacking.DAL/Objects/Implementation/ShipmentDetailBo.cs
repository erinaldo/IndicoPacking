/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the ShipmentDetail table in the database 
    /// </summary>
	public class ShipmentDetailBo : Entity
	{
		#region Fields
		
		private int _iD;
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
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public ShipmentBo ObjShipment
		{ 
			get 
			{ 
				return (Shipment<1) ? null : (_objShipment ?? (_objShipment = Context.Unit.ShipmentRepository.Get(Shipment)));
			}
			set { _objShipment = value; Shipment = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Shipment"); }}
		}
		public int Shipment { get { return _shipment; } set { _shipment = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Shipment"); }}}
		public int IndicoDistributorClientAddress { get { return _indicoDistributorClientAddress; } set { _indicoDistributorClientAddress = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoDistributorClientAddress"); }}}
		public string ShipTo { get { return _shipTo; } set { _shipTo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipTo"); }}}
		public string Port { get { return _port; } set { _port = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Port"); }}}
		public string ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentMode"); }}}
		public string PriceTerm { get { return _priceTerm; } set { _priceTerm = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PriceTerm"); }}}
		public DateTime ETD { get { return _eTD; } set { _eTD = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ETD"); }}}
		public int Qty { get { return _qty; } set { _qty = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Qty"); }}}
		public int QuantityFilled { get { return _quantityFilled; } set { _quantityFilled = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("QuantityFilled"); }}}
		public int QuantityYetToBeFilled { get { return _quantityYetToBeFilled; } set { _quantityYetToBeFilled = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("QuantityYetToBeFilled"); }}}
		public string InvoiceNo { get { return _invoiceNo; } set { _invoiceNo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("InvoiceNo"); }}}

		#endregion

		#region Methods
				
		public BoCollection<InvoiceBo> InvoicesWhereThisIsShipmentDetail()
		{
			 List<InvoiceBo> list;
			 try { list = Context.Unit.InvoiceRepository.Where(new {ShipmentDetail = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<InvoiceBo>(this, list, "ShipmentDetail");
		}

		
		public BoCollection<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsShipmentDetail()
		{
			 List<OrderDeatilItemBo> list;
			 try { list = Context.Unit.OrderDeatilItemRepository.Where(new {ShipmentDetail = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<OrderDeatilItemBo>(this, list, "ShipmentDetail");
		}

		
		public BoCollection<ShipmentDetailCartonBo> ShipmentDetailCartonsWhereThisIsShipmentDetail()
		{
			 List<ShipmentDetailCartonBo> list;
			 try { list = Context.Unit.ShipmentDetailCartonRepository.Where(new {ShipmentDetail = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<ShipmentDetailCartonBo>(this, list, "ShipmentDetail");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
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

		internal override void Copy(Entity en)
		{
			var entity = en as ShipmentDetailBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_shipment = entity._shipment;
			_objShipment = entity._objShipment;
			_indicoDistributorClientAddress = entity._indicoDistributorClientAddress;
			_shipTo = entity._shipTo;
			_port = entity._port;
			_shipmentMode = entity._shipmentMode;
			_priceTerm = entity._priceTerm;
			_eTD = entity._eTD;
			_qty = entity._qty;
			_quantityFilled = entity._quantityFilled;
			_quantityYetToBeFilled = entity._quantityYetToBeFilled;
			_invoiceNo = entity._invoiceNo;
		}

		#endregion

		#region Constructors
		
		public ShipmentDetailBo()
		{
			TableName = "ShipmentDetail";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

