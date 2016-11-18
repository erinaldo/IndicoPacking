/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the OrderDeatilItem table in the database 
    /// </summary>
	public class OrderDeatilItemBo : Entity
	{
		#region Fields
		
		private int _iD;
		private ShipmentDetailBo _objShipmentDeatil;
		private int _shipmentDeatil;
		private int _indicoOrderID;
		private int _indicoOrderDetailID;
		private ShipmentDetailCartonBo _objShipmentDetailCarton;
		private int? _shipmentDetailCarton;
		private string _orderType;
		private string _distributor;
		private string _client;
		private string _purchaseOrder;
		private string _visualLayout;
		private int _orderNumber;
		private string _pattern;
		private string _fabric;
		private string _material;
		private string _gender;
		private string _ageGroup;
		private string _sleeveShape;
		private string _sleeveLength;
		private string _itemSubGroup;
		private string _paymentMethod;
		private string _sizeDesc;
		private int? _sizeQty;
		private int? _sizeSrno;
		private string _status;
		private bool _isPolybagScanned;
		private int? _printedCount;
		private string _patternImage;
		private string _vLImage;
		private string _patternNumber;
		private DateTime? _dateScanned;
		private InvoiceBo _objInvoice;
		private int? _invoice;
		private decimal? _factoryPrice;
		private decimal? _indimanPrice;
		private decimal? _otherCharges;
		private string _notes;
		private string _patternInvoiceNotes;
		private string _productNotes;
		private decimal? _jKFOBCostSheetPrice;
		private decimal? _indimanCIFCostSheetPrice;
		private string _hSCode;
		private string _itemName;
		private string _purchaseOrderNo;
		private string _jobName;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public ShipmentDetailBo ObjShipmentDeatil
		{ 
			get 
			{ 
				return (ShipmentDeatil<1) ? null : (_objShipmentDeatil ?? (_objShipmentDeatil = Context.Unit.ShipmentDetailRepository.Get(ShipmentDeatil)));
			}
			set { _objShipmentDeatil = value; ShipmentDeatil = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDeatil"); }}
		}
		public int ShipmentDeatil { get { return _shipmentDeatil; } set { _shipmentDeatil = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDeatil"); }}}
		public int IndicoOrderID { get { return _indicoOrderID; } set { _indicoOrderID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoOrderID"); }}}
		public int IndicoOrderDetailID { get { return _indicoOrderDetailID; } set { _indicoOrderDetailID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndicoOrderDetailID"); }}}
		public ShipmentDetailCartonBo ObjShipmentDetailCarton
		{ 
			get 
			{ 
				return (ShipmentDetailCarton.GetValueOrDefault()<1) ? null : (_objShipmentDetailCarton ?? (_objShipmentDetailCarton = Context.Unit.ShipmentDetailCartonRepository.Get(ShipmentDetailCarton.GetValueOrDefault())));
			}
			set { _objShipmentDetailCarton = value; ShipmentDetailCarton = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDetailCarton"); }}
		}
		public int? ShipmentDetailCarton { get { return _shipmentDetailCarton; } set { _shipmentDetailCarton = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDetailCarton"); }}}
		public string OrderType { get { return _orderType; } set { _orderType = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderType"); }}}
		public string Distributor { get { return _distributor; } set { _distributor = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Distributor"); }}}
		public string Client { get { return _client; } set { _client = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Client"); }}}
		public string PurchaseOrder { get { return _purchaseOrder; } set { _purchaseOrder = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PurchaseOrder"); }}}
		public string VisualLayout { get { return _visualLayout; } set { _visualLayout = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("VisualLayout"); }}}
		public int OrderNumber { get { return _orderNumber; } set { _orderNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderNumber"); }}}
		public string Pattern { get { return _pattern; } set { _pattern = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Pattern"); }}}
		public string Fabric { get { return _fabric; } set { _fabric = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Fabric"); }}}
		public string Material { get { return _material; } set { _material = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Material"); }}}
		public string Gender { get { return _gender; } set { _gender = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Gender"); }}}
		public string AgeGroup { get { return _ageGroup; } set { _ageGroup = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("AgeGroup"); }}}
		public string SleeveShape { get { return _sleeveShape; } set { _sleeveShape = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SleeveShape"); }}}
		public string SleeveLength { get { return _sleeveLength; } set { _sleeveLength = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SleeveLength"); }}}
		public string ItemSubGroup { get { return _itemSubGroup; } set { _itemSubGroup = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ItemSubGroup"); }}}
		public string PaymentMethod { get { return _paymentMethod; } set { _paymentMethod = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PaymentMethod"); }}}
		public string SizeDesc { get { return _sizeDesc; } set { _sizeDesc = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SizeDesc"); }}}
		public int? SizeQty { get { return _sizeQty; } set { _sizeQty = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SizeQty"); }}}
		public int? SizeSrno { get { return _sizeSrno; } set { _sizeSrno = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SizeSrno"); }}}
		public string Status { get { return _status; } set { _status = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}}
		public bool IsPolybagScanned { get { return _isPolybagScanned; } set { _isPolybagScanned = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IsPolybagScanned"); }}}
		public int? PrintedCount { get { return _printedCount; } set { _printedCount = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PrintedCount"); }}}
		public string PatternImage { get { return _patternImage; } set { _patternImage = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PatternImage"); }}}
		public string VLImage { get { return _vLImage; } set { _vLImage = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("VLImage"); }}}
		public string PatternNumber { get { return _patternNumber; } set { _patternNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PatternNumber"); }}}
		public DateTime? DateScanned { get { return _dateScanned; } set { _dateScanned = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DateScanned"); }}}
		public InvoiceBo ObjInvoice
		{ 
			get 
			{ 
				return (Invoice.GetValueOrDefault()<1) ? null : (_objInvoice ?? (_objInvoice = Context.Unit.InvoiceRepository.Get(Invoice.GetValueOrDefault())));
			}
			set { _objInvoice = value; Invoice = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Invoice"); }}
		}
		public int? Invoice { get { return _invoice; } set { _invoice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Invoice"); }}}
		public decimal? FactoryPrice { get { return _factoryPrice; } set { _factoryPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("FactoryPrice"); }}}
		public decimal? IndimanPrice { get { return _indimanPrice; } set { _indimanPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndimanPrice"); }}}
		public decimal? OtherCharges { get { return _otherCharges; } set { _otherCharges = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OtherCharges"); }}}
		public string Notes { get { return _notes; } set { _notes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Notes"); }}}
		public string PatternInvoiceNotes { get { return _patternInvoiceNotes; } set { _patternInvoiceNotes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PatternInvoiceNotes"); }}}
		public string ProductNotes { get { return _productNotes; } set { _productNotes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ProductNotes"); }}}
		public decimal? JKFOBCostSheetPrice { get { return _jKFOBCostSheetPrice; } set { _jKFOBCostSheetPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("JKFOBCostSheetPrice"); }}}
		public decimal? IndimanCIFCostSheetPrice { get { return _indimanCIFCostSheetPrice; } set { _indimanCIFCostSheetPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndimanCIFCostSheetPrice"); }}}
		public string HSCode { get { return _hSCode; } set { _hSCode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("HSCode"); }}}
		public string ItemName { get { return _itemName; } set { _itemName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ItemName"); }}}
		public string PurchaseOrderNo { get { return _purchaseOrderNo; } set { _purchaseOrderNo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PurchaseOrderNo"); }}}
		public string JobName { get { return _jobName; } set { _jobName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("JobName"); }}}

		#endregion

		#region Methods
		

		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
				{"ShipmentDeatil", ShipmentDeatil},
				{"IndicoOrderID", IndicoOrderID},
				{"IndicoOrderDetailID", IndicoOrderDetailID},
				{"ShipmentDetailCarton", ShipmentDetailCarton},
				{"OrderType", OrderType},
				{"Distributor", Distributor},
				{"Client", Client},
				{"PurchaseOrder", PurchaseOrder},
				{"VisualLayout", VisualLayout},
				{"OrderNumber", OrderNumber},
				{"Pattern", Pattern},
				{"Fabric", Fabric},
				{"Material", Material},
				{"Gender", Gender},
				{"AgeGroup", AgeGroup},
				{"SleeveShape", SleeveShape},
				{"SleeveLength", SleeveLength},
				{"ItemSubGroup", ItemSubGroup},
				{"PaymentMethod", PaymentMethod},
				{"SizeDesc", SizeDesc},
				{"SizeQty", SizeQty},
				{"SizeSrno", SizeSrno},
				{"Status", Status},
				{"IsPolybagScanned", IsPolybagScanned},
				{"PrintedCount", PrintedCount},
				{"PatternImage", PatternImage},
				{"VLImage", VLImage},
				{"PatternNumber", PatternNumber},
				{"DateScanned", DateScanned},
				{"Invoice", Invoice},
				{"FactoryPrice", FactoryPrice},
				{"IndimanPrice", IndimanPrice},
				{"OtherCharges", OtherCharges},
				{"Notes", Notes},
				{"PatternInvoiceNotes", PatternInvoiceNotes},
				{"ProductNotes", ProductNotes},
				{"JKFOBCostSheetPrice", JKFOBCostSheetPrice},
				{"IndimanCIFCostSheetPrice", IndimanCIFCostSheetPrice},
				{"HSCode", HSCode},
				{"ItemName", ItemName},
				{"PurchaseOrderNo", PurchaseOrderNo},
				{"JobName", JobName}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as OrderDeatilItemBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_shipmentDeatil = entity._shipmentDeatil;
			_objShipmentDeatil = entity._objShipmentDeatil;
			_indicoOrderID = entity._indicoOrderID;
			_indicoOrderDetailID = entity._indicoOrderDetailID;
			_shipmentDetailCarton = entity._shipmentDetailCarton;
			_objShipmentDetailCarton = entity._objShipmentDetailCarton;
			_orderType = entity._orderType;
			_distributor = entity._distributor;
			_client = entity._client;
			_purchaseOrder = entity._purchaseOrder;
			_visualLayout = entity._visualLayout;
			_orderNumber = entity._orderNumber;
			_pattern = entity._pattern;
			_fabric = entity._fabric;
			_material = entity._material;
			_gender = entity._gender;
			_ageGroup = entity._ageGroup;
			_sleeveShape = entity._sleeveShape;
			_sleeveLength = entity._sleeveLength;
			_itemSubGroup = entity._itemSubGroup;
			_paymentMethod = entity._paymentMethod;
			_sizeDesc = entity._sizeDesc;
			_sizeQty = entity._sizeQty;
			_sizeSrno = entity._sizeSrno;
			_status = entity._status;
			_isPolybagScanned = entity._isPolybagScanned;
			_printedCount = entity._printedCount;
			_patternImage = entity._patternImage;
			_vLImage = entity._vLImage;
			_patternNumber = entity._patternNumber;
			_dateScanned = entity._dateScanned;
			_invoice = entity._invoice;
			_objInvoice = entity._objInvoice;
			_factoryPrice = entity._factoryPrice;
			_indimanPrice = entity._indimanPrice;
			_otherCharges = entity._otherCharges;
			_notes = entity._notes;
			_patternInvoiceNotes = entity._patternInvoiceNotes;
			_productNotes = entity._productNotes;
			_jKFOBCostSheetPrice = entity._jKFOBCostSheetPrice;
			_indimanCIFCostSheetPrice = entity._indimanCIFCostSheetPrice;
			_hSCode = entity._hSCode;
			_itemName = entity._itemName;
			_purchaseOrderNo = entity._purchaseOrderNo;
			_jobName = entity._jobName;
		}

		#endregion

		#region Constructors
		
		public OrderDeatilItemBo()
		{
			TableName = "OrderDeatilItem";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

