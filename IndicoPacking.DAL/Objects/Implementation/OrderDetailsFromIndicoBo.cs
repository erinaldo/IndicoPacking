using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class OrderDetailsFromIndicoBo : Entity
	{
		#region Fields
		
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
		
		public int? OrderID { get { return _orderID; } set { _orderID = value; NotifyPropertyChanged("OrderID");}}
		public int? OrderDetailID { get { return _orderDetailID; } set { _orderDetailID = value; NotifyPropertyChanged("OrderDetailID");}}
		public DateTime? OrderShipmentDate { get { return _orderShipmentDate; } set { _orderShipmentDate = value; NotifyPropertyChanged("OrderShipmentDate");}}
		public DateTime? OrderDetailShipmentDate { get { return _orderDetailShipmentDate; } set { _orderDetailShipmentDate = value; NotifyPropertyChanged("OrderDetailShipmentDate");}}
		public string OrderType { get { return _orderType; } set { _orderType = value; NotifyPropertyChanged("OrderType");}}
		public string Distributor { get { return _distributor; } set { _distributor = value; NotifyPropertyChanged("Distributor");}}
		public string Client { get { return _client; } set { _client = value; NotifyPropertyChanged("Client");}}
		public string PurchaseOrder { get { return _purchaseOrder; } set { _purchaseOrder = value; NotifyPropertyChanged("PurchaseOrder");}}
		public string NamePrefix { get { return _namePrefix; } set { _namePrefix = value; NotifyPropertyChanged("NamePrefix");}}
		public string Pattern { get { return _pattern; } set { _pattern = value; NotifyPropertyChanged("Pattern");}}
		public string Fabric { get { return _fabric; } set { _fabric = value; NotifyPropertyChanged("Fabric");}}
		public string Material { get { return _material; } set { _material = value; NotifyPropertyChanged("Material");}}
		public string Gender { get { return _gender; } set { _gender = value; NotifyPropertyChanged("Gender");}}
		public string AgeGroup { get { return _ageGroup; } set { _ageGroup = value; NotifyPropertyChanged("AgeGroup");}}
		public string SleeveShape { get { return _sleeveShape; } set { _sleeveShape = value; NotifyPropertyChanged("SleeveShape");}}
		public string SleeveLength { get { return _sleeveLength; } set { _sleeveLength = value; NotifyPropertyChanged("SleeveLength");}}
		public string ItemSubGroup { get { return _itemSubGroup; } set { _itemSubGroup = value; NotifyPropertyChanged("ItemSubGroup");}}
		public string SizeName { get { return _sizeName; } set { _sizeName = value; NotifyPropertyChanged("SizeName");}}
		public int? Quantity { get { return _quantity; } set { _quantity = value; NotifyPropertyChanged("Quantity");}}
		public int? SequenceNumber { get { return _sequenceNumber; } set { _sequenceNumber = value; NotifyPropertyChanged("SequenceNumber");}}
		public string Status { get { return _status; } set { _status = value; NotifyPropertyChanged("Status");}}
		public int? PrintedCount { get { return _printedCount; } set { _printedCount = value; NotifyPropertyChanged("PrintedCount");}}
		public string PatternImagePath { get { return _patternImagePath; } set { _patternImagePath = value; NotifyPropertyChanged("PatternImagePath");}}
		public string VLImagePath { get { return _vLImagePath; } set { _vLImagePath = value; NotifyPropertyChanged("VLImagePath");}}
		public string Number { get { return _number; } set { _number = value; NotifyPropertyChanged("Number");}}
		public decimal? OtherCharges { get { return _otherCharges; } set { _otherCharges = value; NotifyPropertyChanged("OtherCharges");}}
		public string Notes { get { return _notes; } set { _notes = value; NotifyPropertyChanged("Notes");}}
		public string PatternInvoiceNotes { get { return _patternInvoiceNotes; } set { _patternInvoiceNotes = value; NotifyPropertyChanged("PatternInvoiceNotes");}}
		public string ProductNotes { get { return _productNotes; } set { _productNotes = value; NotifyPropertyChanged("ProductNotes");}}
		public decimal? JKFOBCostSheetPrice { get { return _jKFOBCostSheetPrice; } set { _jKFOBCostSheetPrice = value; NotifyPropertyChanged("JKFOBCostSheetPrice");}}
		public decimal? IndimanCIFCostSheetPrice { get { return _indimanCIFCostSheetPrice; } set { _indimanCIFCostSheetPrice = value; NotifyPropertyChanged("IndimanCIFCostSheetPrice");}}
		public string HSCode { get { return _hSCode; } set { _hSCode = value; NotifyPropertyChanged("HSCode");}}
		public string ItemName { get { return _itemName; } set { _itemName = value; NotifyPropertyChanged("ItemName");}}
		public string PurchaseOrderNo { get { return _purchaseOrderNo; } set { _purchaseOrderNo = value; NotifyPropertyChanged("PurchaseOrderNo");}}
		public int? DistributorClientAddressID { get { return _distributorClientAddressID; } set { _distributorClientAddressID = value; NotifyPropertyChanged("DistributorClientAddressID");}}
		public string DistributorClientAddressName { get { return _distributorClientAddressName; } set { _distributorClientAddressName = value; NotifyPropertyChanged("DistributorClientAddressName");}}
		public string DestinationPort { get { return _destinationPort; } set { _destinationPort = value; NotifyPropertyChanged("DestinationPort");}}
		public string ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value; NotifyPropertyChanged("ShipmentMode");}}
		public string PaymentMethod { get { return _paymentMethod; } set { _paymentMethod = value; NotifyPropertyChanged("PaymentMethod");}}
		public int? WeekNumber { get { return _weekNumber; } set { _weekNumber = value; NotifyPropertyChanged("WeekNumber");}}
		public DateTime? WeekEndDate { get { return _weekEndDate; } set { _weekEndDate = value; NotifyPropertyChanged("WeekEndDate");}}
		public string JobName { get { return _jobName; } set { _jobName = value; NotifyPropertyChanged("JobName");}}

		

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
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

		#endregion
	}
} 

