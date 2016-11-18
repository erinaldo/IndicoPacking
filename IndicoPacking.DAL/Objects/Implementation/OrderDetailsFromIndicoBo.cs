/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the OrderDetailsFromIndico table in the database 
    /// </summary>
	public class OrderDetailsFromIndicoBo : Entity
	{
		#region Fields
		
		private int _iD;
		private int? _orderID;
		private int? _orderDetailID;
		private DateTime? _orderShipmentDate;
		private DateTime? _orderDetailShipmentDate;
		private string _orderType;
		private string _distributor;
		private string _client;
		private string _purchaseOrder;
		private string _namePrefix;
		private string _pattern;
		private string _fabric;
		private string _material;
		private string _gender;
		private string _ageGroup;
		private string _sleeveShape;
		private string _sleeveLength;
		private string _itemSubGroup;
		private string _sizeName;
		private int? _quantity;
		private int? _sequenceNumber;
		private string _status;
		private int? _printedCount;
		private string _patternImagePath;
		private string _vLImagePath;
		private string _number;
		private decimal? _otherCharges;
		private string _notes;
		private string _patternInvoiceNotes;
		private string _productNotes;
		private decimal? _jKFOBCostSheetPrice;
		private decimal? _indimanCIFCostSheetPrice;
		private string _hSCode;
		private string _itemName;
		private string _purchaseOrderNo;
		private int? _distributorClientAddressID;
		private string _distributorClientAddressName;
		private string _destinationPort;
		private string _shipmentMode;
		private string _paymentMethod;
		private int? _weekNumber;
		private DateTime? _weekEndDate;
		private string _jobName;

		#endregion

		#region Properties
		
		public int ID { get { return _iD; } set { _iD = value; PrimaryKey = value; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ID"); }}}
		public int? OrderID { get { return _orderID; } set { _orderID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderID"); }}}
		public int? OrderDetailID { get { return _orderDetailID; } set { _orderDetailID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderDetailID"); }}}
		public DateTime? OrderShipmentDate { get { return _orderShipmentDate; } set { _orderShipmentDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderShipmentDate"); }}}
		public DateTime? OrderDetailShipmentDate { get { return _orderDetailShipmentDate; } set { _orderDetailShipmentDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderDetailShipmentDate"); }}}
		public string OrderType { get { return _orderType; } set { _orderType = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OrderType"); }}}
		public string Distributor { get { return _distributor; } set { _distributor = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Distributor"); }}}
		public string Client { get { return _client; } set { _client = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Client"); }}}
		public string PurchaseOrder { get { return _purchaseOrder; } set { _purchaseOrder = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PurchaseOrder"); }}}
		public string NamePrefix { get { return _namePrefix; } set { _namePrefix = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("NamePrefix"); }}}
		public string Pattern { get { return _pattern; } set { _pattern = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Pattern"); }}}
		public string Fabric { get { return _fabric; } set { _fabric = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Fabric"); }}}
		public string Material { get { return _material; } set { _material = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Material"); }}}
		public string Gender { get { return _gender; } set { _gender = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Gender"); }}}
		public string AgeGroup { get { return _ageGroup; } set { _ageGroup = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("AgeGroup"); }}}
		public string SleeveShape { get { return _sleeveShape; } set { _sleeveShape = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SleeveShape"); }}}
		public string SleeveLength { get { return _sleeveLength; } set { _sleeveLength = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SleeveLength"); }}}
		public string ItemSubGroup { get { return _itemSubGroup; } set { _itemSubGroup = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ItemSubGroup"); }}}
		public string SizeName { get { return _sizeName; } set { _sizeName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SizeName"); }}}
		public int? Quantity { get { return _quantity; } set { _quantity = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Quantity"); }}}
		public int? SequenceNumber { get { return _sequenceNumber; } set { _sequenceNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("SequenceNumber"); }}}
		public string Status { get { return _status; } set { _status = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}}
		public int? PrintedCount { get { return _printedCount; } set { _printedCount = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PrintedCount"); }}}
		public string PatternImagePath { get { return _patternImagePath; } set { _patternImagePath = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PatternImagePath"); }}}
		public string VLImagePath { get { return _vLImagePath; } set { _vLImagePath = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("VLImagePath"); }}}
		public string Number { get { return _number; } set { _number = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Number"); }}}
		public decimal? OtherCharges { get { return _otherCharges; } set { _otherCharges = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("OtherCharges"); }}}
		public string Notes { get { return _notes; } set { _notes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Notes"); }}}
		public string PatternInvoiceNotes { get { return _patternInvoiceNotes; } set { _patternInvoiceNotes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PatternInvoiceNotes"); }}}
		public string ProductNotes { get { return _productNotes; } set { _productNotes = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ProductNotes"); }}}
		public decimal? JKFOBCostSheetPrice { get { return _jKFOBCostSheetPrice; } set { _jKFOBCostSheetPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("JKFOBCostSheetPrice"); }}}
		public decimal? IndimanCIFCostSheetPrice { get { return _indimanCIFCostSheetPrice; } set { _indimanCIFCostSheetPrice = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndimanCIFCostSheetPrice"); }}}
		public string HSCode { get { return _hSCode; } set { _hSCode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("HSCode"); }}}
		public string ItemName { get { return _itemName; } set { _itemName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ItemName"); }}}
		public string PurchaseOrderNo { get { return _purchaseOrderNo; } set { _purchaseOrderNo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PurchaseOrderNo"); }}}
		public int? DistributorClientAddressID { get { return _distributorClientAddressID; } set { _distributorClientAddressID = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DistributorClientAddressID"); }}}
		public string DistributorClientAddressName { get { return _distributorClientAddressName; } set { _distributorClientAddressName = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DistributorClientAddressName"); }}}
		public string DestinationPort { get { return _destinationPort; } set { _destinationPort = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("DestinationPort"); }}}
		public string ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentMode"); }}}
		public string PaymentMethod { get { return _paymentMethod; } set { _paymentMethod = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("PaymentMethod"); }}}
		public int? WeekNumber { get { return _weekNumber; } set { _weekNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("WeekNumber"); }}}
		public DateTime? WeekEndDate { get { return _weekEndDate; } set { _weekEndDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("WeekEndDate"); }}}
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
				{"OrderID", OrderID},
				{"OrderDetailID", OrderDetailID},
				{"OrderShipmentDate", OrderShipmentDate},
				{"OrderDetailShipmentDate", OrderDetailShipmentDate},
				{"OrderType", OrderType},
				{"Distributor", Distributor},
				{"Client", Client},
				{"PurchaseOrder", PurchaseOrder},
				{"NamePrefix", NamePrefix},
				{"Pattern", Pattern},
				{"Fabric", Fabric},
				{"Material", Material},
				{"Gender", Gender},
				{"AgeGroup", AgeGroup},
				{"SleeveShape", SleeveShape},
				{"SleeveLength", SleeveLength},
				{"ItemSubGroup", ItemSubGroup},
				{"SizeName", SizeName},
				{"Quantity", Quantity},
				{"SequenceNumber", SequenceNumber},
				{"Status", Status},
				{"PrintedCount", PrintedCount},
				{"PatternImagePath", PatternImagePath},
				{"VLImagePath", VLImagePath},
				{"Number", Number},
				{"OtherCharges", OtherCharges},
				{"Notes", Notes},
				{"PatternInvoiceNotes", PatternInvoiceNotes},
				{"ProductNotes", ProductNotes},
				{"JKFOBCostSheetPrice", JKFOBCostSheetPrice},
				{"IndimanCIFCostSheetPrice", IndimanCIFCostSheetPrice},
				{"HSCode", HSCode},
				{"ItemName", ItemName},
				{"PurchaseOrderNo", PurchaseOrderNo},
				{"DistributorClientAddressID", DistributorClientAddressID},
				{"DistributorClientAddressName", DistributorClientAddressName},
				{"DestinationPort", DestinationPort},
				{"ShipmentMode", ShipmentMode},
				{"PaymentMethod", PaymentMethod},
				{"WeekNumber", WeekNumber},
				{"WeekEndDate", WeekEndDate},
				{"JobName", JobName}
			};
        }

		internal override void Copy(Entity en)
		{
			var entity = en as OrderDetailsFromIndicoBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_orderID = entity._orderID;
			_orderDetailID = entity._orderDetailID;
			_orderShipmentDate = entity._orderShipmentDate;
			_orderDetailShipmentDate = entity._orderDetailShipmentDate;
			_orderType = entity._orderType;
			_distributor = entity._distributor;
			_client = entity._client;
			_purchaseOrder = entity._purchaseOrder;
			_namePrefix = entity._namePrefix;
			_pattern = entity._pattern;
			_fabric = entity._fabric;
			_material = entity._material;
			_gender = entity._gender;
			_ageGroup = entity._ageGroup;
			_sleeveShape = entity._sleeveShape;
			_sleeveLength = entity._sleeveLength;
			_itemSubGroup = entity._itemSubGroup;
			_sizeName = entity._sizeName;
			_quantity = entity._quantity;
			_sequenceNumber = entity._sequenceNumber;
			_status = entity._status;
			_printedCount = entity._printedCount;
			_patternImagePath = entity._patternImagePath;
			_vLImagePath = entity._vLImagePath;
			_number = entity._number;
			_otherCharges = entity._otherCharges;
			_notes = entity._notes;
			_patternInvoiceNotes = entity._patternInvoiceNotes;
			_productNotes = entity._productNotes;
			_jKFOBCostSheetPrice = entity._jKFOBCostSheetPrice;
			_indimanCIFCostSheetPrice = entity._indimanCIFCostSheetPrice;
			_hSCode = entity._hSCode;
			_itemName = entity._itemName;
			_purchaseOrderNo = entity._purchaseOrderNo;
			_distributorClientAddressID = entity._distributorClientAddressID;
			_distributorClientAddressName = entity._distributorClientAddressName;
			_destinationPort = entity._destinationPort;
			_shipmentMode = entity._shipmentMode;
			_paymentMethod = entity._paymentMethod;
			_weekNumber = entity._weekNumber;
			_weekEndDate = entity._weekEndDate;
			_jobName = entity._jobName;
		}

		#endregion

		#region Constructors
		
		public OrderDetailsFromIndicoBo()
		{
			TableName = "OrderDetailsFromIndico";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

