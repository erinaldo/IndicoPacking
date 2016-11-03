using System;
using System.Collections.Generic;
using System.Linq;

namespace IndicoPacking.DAL.Objects.Implementation
{
	public class InvoiceBo : Entity
	{
		#region Fields
		
		private ShipmentDetailBo _objShipmentDetail;
		private int _shipmentDetail;
		private string _factoryInvoiceNumber;
		private DateTime _factoryInvoiceDate;
		private string _aWBNumber;
		private string _indimanInvoiceNumber;
		private DateTime? _indimanInvoiceDate;
		private DateTime _createdDate;
		private DateTime _modifiedDate;
		private UserBo _objLastModifiedBy;
		private int _lastModifiedBy;
		private InvoiceStatusBo _objStatus;
		private int _status;
		private DateTime _shipmentDate;
		private DistributorClientAddressBo _objShipTo;
		private int _shipTo;
		private DistributorClientAddressBo _objBillTo;
		private int? _billTo;
		private ShipmentModeBo _objShipmentMode;
		private int _shipmentMode;
		private PortBo _objPort;
		private int _port;
		private BankBo _objBank;
		private int _bank;

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
		public string FactoryInvoiceNumber { get { return _factoryInvoiceNumber; } set { _factoryInvoiceNumber = value; NotifyPropertyChanged("FactoryInvoiceNumber");}}
		public DateTime FactoryInvoiceDate { get { return _factoryInvoiceDate; } set { _factoryInvoiceDate = value; NotifyPropertyChanged("FactoryInvoiceDate");}}
		public string AWBNumber { get { return _aWBNumber; } set { _aWBNumber = value; NotifyPropertyChanged("AWBNumber");}}
		public string IndimanInvoiceNumber { get { return _indimanInvoiceNumber; } set { _indimanInvoiceNumber = value; NotifyPropertyChanged("IndimanInvoiceNumber");}}
		public DateTime? IndimanInvoiceDate { get { return _indimanInvoiceDate; } set { _indimanInvoiceDate = value; NotifyPropertyChanged("IndimanInvoiceDate");}}
		public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value; NotifyPropertyChanged("CreatedDate");}}
		public DateTime ModifiedDate { get { return _modifiedDate; } set { _modifiedDate = value; NotifyPropertyChanged("ModifiedDate");}}
		public UserBo ObjLastModifiedBy
		{ 
			get 
			{ 
				return (LastModifiedBy<1) ? null : (_objLastModifiedBy ?? (_objLastModifiedBy = _context.Unit.UserRepository.Get(LastModifiedBy)));
			}
			set { _objLastModifiedBy = value; LastModifiedBy = value.ID; NotifyPropertyChanged("LastModifiedBy");}
		}
		public int LastModifiedBy { get { return _lastModifiedBy; } set { _lastModifiedBy = value; NotifyPropertyChanged("LastModifiedBy");}}
		public InvoiceStatusBo ObjStatus
		{ 
			get 
			{ 
				return (Status<1) ? null : (_objStatus ?? (_objStatus = _context.Unit.InvoiceStatusRepository.Get(Status)));
			}
			set { _objStatus = value; Status = value.ID; NotifyPropertyChanged("Status");}
		}
		public int Status { get { return _status; } set { _status = value; NotifyPropertyChanged("Status");}}
		public DateTime ShipmentDate { get { return _shipmentDate; } set { _shipmentDate = value; NotifyPropertyChanged("ShipmentDate");}}
		public DistributorClientAddressBo ObjShipTo
		{ 
			get 
			{ 
				return (ShipTo<1) ? null : (_objShipTo ?? (_objShipTo = _context.Unit.DistributorClientAddressRepository.Get(ShipTo)));
			}
			set { _objShipTo = value; ShipTo = value.ID; NotifyPropertyChanged("ShipTo");}
		}
		public int ShipTo { get { return _shipTo; } set { _shipTo = value; NotifyPropertyChanged("ShipTo");}}
		public DistributorClientAddressBo ObjBillTo
		{ 
			get 
			{ 
				return (BillTo.GetValueOrDefault()<1) ? null : (_objBillTo ?? (_objBillTo = _context.Unit.DistributorClientAddressRepository.Get(BillTo.GetValueOrDefault())));
			}
			set { _objBillTo = value; BillTo = value.ID; NotifyPropertyChanged("BillTo");}
		}
		public int? BillTo { get { return _billTo; } set { _billTo = value; NotifyPropertyChanged("BillTo");}}
		public ShipmentModeBo ObjShipmentMode
		{ 
			get 
			{ 
				return (ShipmentMode<1) ? null : (_objShipmentMode ?? (_objShipmentMode = _context.Unit.ShipmentModeRepository.Get(ShipmentMode)));
			}
			set { _objShipmentMode = value; ShipmentMode = value.ID; NotifyPropertyChanged("ShipmentMode");}
		}
		public int ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value; NotifyPropertyChanged("ShipmentMode");}}
		public PortBo ObjPort
		{ 
			get 
			{ 
				return (Port<1) ? null : (_objPort ?? (_objPort = _context.Unit.PortRepository.Get(Port)));
			}
			set { _objPort = value; Port = value.ID; NotifyPropertyChanged("Port");}
		}
		public int Port { get { return _port; } set { _port = value; NotifyPropertyChanged("Port");}}
		public BankBo ObjBank
		{ 
			get 
			{ 
				return (Bank<1) ? null : (_objBank ?? (_objBank = _context.Unit.BankRepository.Get(Bank)));
			}
			set { _objBank = value; Bank = value.ID; NotifyPropertyChanged("Bank");}
		}
		public int Bank { get { return _bank; } set { _bank = value; NotifyPropertyChanged("Bank");}}

		
		public List<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsInvoice => _context.Unit.OrderDeatilItemRepository.Where(new {Invoice = ID}).ToList();

		#endregion

		#region Methods

		public override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ShipmentDetail", ShipmentDetail},
				{"FactoryInvoiceNumber", FactoryInvoiceNumber},
				{"FactoryInvoiceDate", FactoryInvoiceDate},
				{"AWBNumber", AWBNumber},
				{"IndimanInvoiceNumber", IndimanInvoiceNumber},
				{"IndimanInvoiceDate", IndimanInvoiceDate},
				{"CreatedDate", CreatedDate},
				{"ModifiedDate", ModifiedDate},
				{"LastModifiedBy", LastModifiedBy},
				{"Status", Status},
				{"ShipmentDate", ShipmentDate},
				{"ShipTo", ShipTo},
				{"BillTo", BillTo},
				{"ShipmentMode", ShipmentMode},
				{"Port", Port},
				{"Bank", Bank}
			};
        }

		#endregion
	}
} 

