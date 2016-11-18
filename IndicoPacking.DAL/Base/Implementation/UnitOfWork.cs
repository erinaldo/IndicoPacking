/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Repositories.Implementation;
using IndicoPacking.DAL.Objects.SPs;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Views;

namespace IndicoPacking.DAL.Base.Implementation
{

    public class UnitOfWork : IUnitOfWork
    {
		#region Private fields
        
		private readonly IDbContext _context;
		
		private BankRepository _bankRepository;
		private CartonRepository _cartonRepository;
		private CountryRepository _countryRepository;
		private DistributorClientAddressRepository _distributorClientAddressRepository;
		private InvoiceRepository _invoiceRepository;
		private InvoiceStatusRepository _invoiceStatusRepository;
		private OrderDeatilItemRepository _orderDeatilItemRepository;
		private OrderDetailsFromIndicoRepository _orderDetailsFromIndicoRepository;
		private PortRepository _portRepository;
		private RoleRepository _roleRepository;
		private ShipmentRepository _shipmentRepository;
		private ShipmentDetailRepository _shipmentDetailRepository;
		private ShipmentDetailCartonRepository _shipmentDetailCartonRepository;
		private ShipmentModeRepository _shipmentModeRepository;
		private UserRepository _userRepository;
		private UserStatusRepository _userStatusRepository;

		#endregion
		
		#region Public Properties
		
		
		public BankRepository BankRepository => _bankRepository ?? (_bankRepository = new BankRepository(_context));
		public CartonRepository CartonRepository => _cartonRepository ?? (_cartonRepository = new CartonRepository(_context));
		public CountryRepository CountryRepository => _countryRepository ?? (_countryRepository = new CountryRepository(_context));
		public DistributorClientAddressRepository DistributorClientAddressRepository => _distributorClientAddressRepository ?? (_distributorClientAddressRepository = new DistributorClientAddressRepository(_context));
		public InvoiceRepository InvoiceRepository => _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(_context));
		public InvoiceStatusRepository InvoiceStatusRepository => _invoiceStatusRepository ?? (_invoiceStatusRepository = new InvoiceStatusRepository(_context));
		public OrderDeatilItemRepository OrderDeatilItemRepository => _orderDeatilItemRepository ?? (_orderDeatilItemRepository = new OrderDeatilItemRepository(_context));
		public OrderDetailsFromIndicoRepository OrderDetailsFromIndicoRepository => _orderDetailsFromIndicoRepository ?? (_orderDetailsFromIndicoRepository = new OrderDetailsFromIndicoRepository(_context));
		public PortRepository PortRepository => _portRepository ?? (_portRepository = new PortRepository(_context));
		public RoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(_context));
		public ShipmentRepository ShipmentRepository => _shipmentRepository ?? (_shipmentRepository = new ShipmentRepository(_context));
		public ShipmentDetailRepository ShipmentDetailRepository => _shipmentDetailRepository ?? (_shipmentDetailRepository = new ShipmentDetailRepository(_context));
		public ShipmentDetailCartonRepository ShipmentDetailCartonRepository => _shipmentDetailCartonRepository ?? (_shipmentDetailCartonRepository = new ShipmentDetailCartonRepository(_context));
		public ShipmentModeRepository ShipmentModeRepository => _shipmentModeRepository ?? (_shipmentModeRepository = new ShipmentModeRepository(_context));
		public UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));
		public UserStatusRepository UserStatusRepository => _userStatusRepository ?? (_userStatusRepository = new UserStatusRepository(_context));

		#endregion

		#region Public Methods

		#region Views

		
		public List<GetCartonLabelInfoBo> GetCartonLabelInfo(object where=null){return _context.QueryView<GetCartonLabelInfoBo>("GetCartonLabelInfo",where);}
		public List<GetInvoiceOrderDetailItemsWithQuatityBreakdownBo> GetInvoiceOrderDetailItemsWithQuatityBreakdown(object where=null){return _context.QueryView<GetInvoiceOrderDetailItemsWithQuatityBreakdownBo>("GetInvoiceOrderDetailItemsWithQuatityBreakdown",where);}
		public List<GetInvoiceOrderDetailItemsWithQuatityGroupByForFactoryBo> GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory(object where=null){return _context.QueryView<GetInvoiceOrderDetailItemsWithQuatityGroupByForFactoryBo>("GetInvoiceOrderDetailItemsWithQuatityGroupByForFactory",where);}
		public List<GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimanBo> GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman(object where=null){return _context.QueryView<GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimanBo>("GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman",where);}
		public List<GetOrderDetaildForGivenWeekViewBo> GetOrderDetaildForGivenWeekView(object where=null){return _context.QueryView<GetOrderDetaildForGivenWeekViewBo>("GetOrderDetaildForGivenWeekView",where);}
		public List<GetShipmentKeysViewBo> GetShipmentKeysView(object where=null){return _context.QueryView<GetShipmentKeysViewBo>("GetShipmentKeysView",where);}
		public List<GetWeeklyAddressDetailsBo> GetWeeklyAddressDetails(object where=null){return _context.QueryView<GetWeeklyAddressDetailsBo>("GetWeeklyAddressDetails",where);}
		public List<GetWeeklyAddressDetailsByDistributorBo> GetWeeklyAddressDetailsByDistributor(object where=null){return _context.QueryView<GetWeeklyAddressDetailsByDistributorBo>("GetWeeklyAddressDetailsByDistributor",where);}
		public List<GetWeeklyAddressDetailsByDistributorForIndimanBo> GetWeeklyAddressDetailsByDistributorForIndiman(object where=null){return _context.QueryView<GetWeeklyAddressDetailsByDistributorForIndimanBo>("GetWeeklyAddressDetailsByDistributorForIndiman",where);}
		public List<GetWeeklyAddressDetailsByHSCodeBo> GetWeeklyAddressDetailsByHSCode(object where=null){return _context.QueryView<GetWeeklyAddressDetailsByHSCodeBo>("GetWeeklyAddressDetailsByHSCode",where);}
		public List<InvoiceDetailsViewBo> InvoiceDetailsView(object where=null){return _context.QueryView<InvoiceDetailsViewBo>("InvoiceDetailsView",where);}
		public List<InvoiceHeaderDetailsViewBo> InvoiceHeaderDetailsView(object where=null){return _context.QueryView<InvoiceHeaderDetailsViewBo>("InvoiceHeaderDetailsView",where);}
		public List<UserDetailsViewBo> UserDetailsView(object where=null){return _context.QueryView<UserDetailsViewBo>("UserDetailsView",where);}

		#endregion

		#region Stored Procedures

		
		public List<GetItemsOfInvoiceForMyObBo> GetItemsOfInvoiceForMyOb(int @P_InvoiceID){ return _context.Query<GetItemsOfInvoiceForMyObBo>("EXEC GetItemsOfInvoiceForMyOb '"+@P_InvoiceID+"'");}
		public List<SPC_GetDetailForPackingListBo> SPC_GetDetailForPackingList(int @P_ShipmentDetailId){ return _context.Query<SPC_GetDetailForPackingListBo>("EXEC SPC_GetDetailForPackingList '"+@P_ShipmentDetailId+"'");}
		public List<SPC_GetOrderDetailsQuatityCountBo> SPC_GetOrderDetailsQuatityCount(int @WeekNo,DateTime @WeekEndDate){ return _context.Query<SPC_GetOrderDetailsQuatityCountBo>("EXEC SPC_GetOrderDetailsQuatityCount '"+@WeekNo+"','"+@WeekEndDate+"'");}
		public List<SPC_GetPackingListDetailForGivenCartonsBo> SPC_GetPackingListDetailForGivenCartons(int @P_ShipmentDetailId,string @P_ShipmentDetailCartons){ return _context.Query<SPC_GetPackingListDetailForGivenCartonsBo>("EXEC SPC_GetPackingListDetailForGivenCartons '"+@P_ShipmentDetailId+"','"+@P_ShipmentDetailCartons+"'");}
		public List<SPC_GetPackingListDetailsBo> SPC_GetPackingListDetails(int @P_ShipmentDetailId){ return _context.Query<SPC_GetPackingListDetailsBo>("EXEC SPC_GetPackingListDetails '"+@P_ShipmentDetailId+"'");}
		public List<SPC_SynchroniseOrdersBo> SPC_SynchroniseOrders(int @WeekNo,DateTime @WeekEndDate){ return _context.Query<SPC_SynchroniseOrdersBo>("EXEC SPC_SynchroniseOrders '"+@WeekNo+"','"+@WeekEndDate+"'");}
		public List<SPC_SynchronizeOrderDetailsBo> SPC_SynchronizeOrderDetails(){ return _context.Query<SPC_SynchronizeOrderDetailsBo>("EXEC SPC_SynchronizeOrderDetails ");}

		#endregion

		#endregion

        #region Public constructors

		public UnitOfWork()
        {
            _context = new IndicoPackingContext();
			_context.Unit = this;
        }

		#endregion

		#region Public methods

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

		#endregion
    }
}