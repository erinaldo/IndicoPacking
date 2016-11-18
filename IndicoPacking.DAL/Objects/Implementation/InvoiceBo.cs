/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using System.Collections.Generic;
using System.Linq;
using IndicoPacking.DAL.Base.Implementation;

namespace IndicoPacking.DAL.Objects.Implementation
{
    /// <summary>
    /// This class represents the Invoice table in the database 
    /// </summary>
	public class InvoiceBo : Entity
	{
		#region Fields
		
		private int _iD;
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
		public string FactoryInvoiceNumber { get { return _factoryInvoiceNumber; } set { _factoryInvoiceNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("FactoryInvoiceNumber"); }}}
		public DateTime FactoryInvoiceDate { get { return _factoryInvoiceDate; } set { _factoryInvoiceDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("FactoryInvoiceDate"); }}}
		public string AWBNumber { get { return _aWBNumber; } set { _aWBNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("AWBNumber"); }}}
		public string IndimanInvoiceNumber { get { return _indimanInvoiceNumber; } set { _indimanInvoiceNumber = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndimanInvoiceNumber"); }}}
		public DateTime? IndimanInvoiceDate { get { return _indimanInvoiceDate; } set { _indimanInvoiceDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("IndimanInvoiceDate"); }}}
		public DateTime CreatedDate { get { return _createdDate; } set { _createdDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("CreatedDate"); }}}
		public DateTime ModifiedDate { get { return _modifiedDate; } set { _modifiedDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ModifiedDate"); }}}
		public UserBo ObjLastModifiedBy
		{ 
			get 
			{ 
				return (LastModifiedBy<1) ? null : (_objLastModifiedBy ?? (_objLastModifiedBy = Context.Unit.UserRepository.Get(LastModifiedBy)));
			}
			set { _objLastModifiedBy = value; LastModifiedBy = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("LastModifiedBy"); }}
		}
		public int LastModifiedBy { get { return _lastModifiedBy; } set { _lastModifiedBy = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("LastModifiedBy"); }}}
		public InvoiceStatusBo ObjStatus
		{ 
			get 
			{ 
				return (Status<1) ? null : (_objStatus ?? (_objStatus = Context.Unit.InvoiceStatusRepository.Get(Status)));
			}
			set { _objStatus = value; Status = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}
		}
		public int Status { get { return _status; } set { _status = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Status"); }}}
		public DateTime ShipmentDate { get { return _shipmentDate; } set { _shipmentDate = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentDate"); }}}
		public DistributorClientAddressBo ObjShipTo
		{ 
			get 
			{ 
				return (ShipTo<1) ? null : (_objShipTo ?? (_objShipTo = Context.Unit.DistributorClientAddressRepository.Get(ShipTo)));
			}
			set { _objShipTo = value; ShipTo = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipTo"); }}
		}
		public int ShipTo { get { return _shipTo; } set { _shipTo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipTo"); }}}
		public DistributorClientAddressBo ObjBillTo
		{ 
			get 
			{ 
				return (BillTo.GetValueOrDefault()<1) ? null : (_objBillTo ?? (_objBillTo = Context.Unit.DistributorClientAddressRepository.Get(BillTo.GetValueOrDefault())));
			}
			set { _objBillTo = value; BillTo = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("BillTo"); }}
		}
		public int? BillTo { get { return _billTo; } set { _billTo = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("BillTo"); }}}
		public ShipmentModeBo ObjShipmentMode
		{ 
			get 
			{ 
				return (ShipmentMode<1) ? null : (_objShipmentMode ?? (_objShipmentMode = Context.Unit.ShipmentModeRepository.Get(ShipmentMode)));
			}
			set { _objShipmentMode = value; ShipmentMode = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentMode"); }}
		}
		public int ShipmentMode { get { return _shipmentMode; } set { _shipmentMode = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("ShipmentMode"); }}}
		public PortBo ObjPort
		{ 
			get 
			{ 
				return (Port<1) ? null : (_objPort ?? (_objPort = Context.Unit.PortRepository.Get(Port)));
			}
			set { _objPort = value; Port = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Port"); }}
		}
		public int Port { get { return _port; } set { _port = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Port"); }}}
		public BankBo ObjBank
		{ 
			get 
			{ 
				return (Bank<1) ? null : (_objBank ?? (_objBank = Context.Unit.BankRepository.Get(Bank)));
			}
			set { _objBank = value; Bank = value.ID; if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Bank"); }}
		}
		public int Bank { get { return _bank; } set { _bank = value;  if(SouldNotifyPropertyChanges){ NotifyPropertyChanged("Bank"); }}}

		#endregion

		#region Methods
				
		public BoCollection<OrderDeatilItemBo> OrderDeatilItemsWhereThisIsInvoice()
		{
			 List<OrderDeatilItemBo> list;
			 try { list = Context.Unit.OrderDeatilItemRepository.Where(new {Invoice = ID}); }catch(Exception){ list = null; } 
			 return Context == null ? null : new BoCollection<OrderDeatilItemBo>(this, list, "Invoice");
		}



		#endregion

		#region Internal Methods

		internal override Dictionary<string, object> GetColumns()
        {
            return new Dictionary<string, object> 
			{
				{"ID", ID},
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

		internal override void Copy(Entity en)
		{
			var entity = en as InvoiceBo;
			if(entity == null)
				return;
			
			BusinessObjectState = entity.BusinessObjectState;
			_iD = entity._iD;
			_shipmentDetail = entity._shipmentDetail;
			_objShipmentDetail = entity._objShipmentDetail;
			_factoryInvoiceNumber = entity._factoryInvoiceNumber;
			_factoryInvoiceDate = entity._factoryInvoiceDate;
			_aWBNumber = entity._aWBNumber;
			_indimanInvoiceNumber = entity._indimanInvoiceNumber;
			_indimanInvoiceDate = entity._indimanInvoiceDate;
			_createdDate = entity._createdDate;
			_modifiedDate = entity._modifiedDate;
			_lastModifiedBy = entity._lastModifiedBy;
			_objLastModifiedBy = entity._objLastModifiedBy;
			_status = entity._status;
			_objStatus = entity._objStatus;
			_shipmentDate = entity._shipmentDate;
			_shipTo = entity._shipTo;
			_objShipTo = entity._objShipTo;
			_billTo = entity._billTo;
			_objBillTo = entity._objBillTo;
			_shipmentMode = entity._shipmentMode;
			_objShipmentMode = entity._objShipmentMode;
			_port = entity._port;
			_objPort = entity._objPort;
			_bank = entity._bank;
			_objBank = entity._objBank;
		}

		#endregion

		#region Constructors
		
		public InvoiceBo()
		{
			TableName = "Invoice";
			PrimaryKeyName = "ID";
		}

		#endregion
	}
} 

