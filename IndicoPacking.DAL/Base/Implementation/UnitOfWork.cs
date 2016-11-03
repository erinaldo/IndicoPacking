using System;
using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Repositories.Core;
using IndicoPacking.DAL.Repositories.Implementation;
using IndicoPacking.DAL.Objects.SPs;
using System.Collections.Generic;
using IndicoPacking.DAL.Objects.Views;

namespace IndicoPacking.DAL.Base.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

		
		public IBankRepository BankRepository { get; }
		public ICartonRepository CartonRepository { get; }
		public ICountryRepository CountryRepository { get; }
		public IDistributorClientAddressRepository DistributorClientAddressRepository { get; }
		public IInvoiceRepository InvoiceRepository { get; }
		public IInvoiceStatusRepository InvoiceStatusRepository { get; }
		public IOrderDeatilItemRepository OrderDeatilItemRepository { get; }
		public IOrderDetailsFromIndicoRepository OrderDetailsFromIndicoRepository { get; }
		public IPortRepository PortRepository { get; }
		public IRoleRepository RoleRepository { get; }
		public IShipmentRepository ShipmentRepository { get; }
		public IShipmentDetailRepository ShipmentDetailRepository { get; }
		public IShipmentDetailCartonRepository ShipmentDetailCartonRepository { get; }
		public IShipmentModeRepository ShipmentModeRepository { get; }
		public IUserRepository UserRepository { get; }
		public IUserStatusRepository UserStatusRepository { get; }

		
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

		
		public List<GetItemsOfInvoiceForMyObBo> GetItemsOfInvoiceForMyOb(int @P_InvoiceID){ return _context.Query<GetItemsOfInvoiceForMyObBo>("EXEC [dbo].[GetItemsOfInvoiceForMyOb] '"+@P_InvoiceID+"'");}
		public List<SPC_GetDetailForPackingListBo> SPC_GetDetailForPackingList(int @P_ShipmentDetailId){ return _context.Query<SPC_GetDetailForPackingListBo>("EXEC [dbo].[SPC_GetDetailForPackingList] '"+@P_ShipmentDetailId+"'");}
		public List<SPC_GetOrderDetailsQuatityCountBo> SPC_GetOrderDetailsQuatityCount(int @WeekNo,DateTime @WeekEndDate){ return _context.Query<SPC_GetOrderDetailsQuatityCountBo>("EXEC [dbo].[SPC_GetOrderDetailsQuatityCount] '"+@WeekNo+"','"+@WeekEndDate+"'");}
		public List<SPC_GetPackingListDetailForGivenCartonsBo> SPC_GetPackingListDetailForGivenCartons(int @P_ShipmentDetailId,string @P_ShipmentDetailCartons){ return _context.Query<SPC_GetPackingListDetailForGivenCartonsBo>("EXEC [dbo].[SPC_GetPackingListDetailForGivenCartons] '"+@P_ShipmentDetailId+"','"+@P_ShipmentDetailCartons+"'");}
		public List<SPC_GetPackingListDetailsBo> SPC_GetPackingListDetails(int @P_ShipmentDetailId){ return _context.Query<SPC_GetPackingListDetailsBo>("EXEC [dbo].[SPC_GetPackingListDetails] '"+@P_ShipmentDetailId+"'");}
		public List<SPC_SynchroniseOrdersBo> SPC_SynchroniseOrders(int @WeekNo,DateTime @WeekEndDate){ return _context.Query<SPC_SynchroniseOrdersBo>("EXEC [dbo].[SPC_SynchroniseOrders] '"+@WeekNo+"','"+@WeekEndDate+"'");}
		public List<SPC_SynchronizeOrderDetailsBo> SPC_SynchronizeOrderDetails(){ return _context.Query<SPC_SynchronizeOrderDetailsBo>("EXEC [dbo].[SPC_SynchronizeOrderDetails] ");}

        public UnitOfWork()
        {
            _context = new IndicoPackingContext();
			_context.Unit = this;
			
			BankRepository = new BankRepository(_context);
			CartonRepository = new CartonRepository(_context);
			CountryRepository = new CountryRepository(_context);
			DistributorClientAddressRepository = new DistributorClientAddressRepository(_context);
			InvoiceRepository = new InvoiceRepository(_context);
			InvoiceStatusRepository = new InvoiceStatusRepository(_context);
			OrderDeatilItemRepository = new OrderDeatilItemRepository(_context);
			OrderDetailsFromIndicoRepository = new OrderDetailsFromIndicoRepository(_context);
			PortRepository = new PortRepository(_context);
			RoleRepository = new RoleRepository(_context);
			ShipmentRepository = new ShipmentRepository(_context);
			ShipmentDetailRepository = new ShipmentDetailRepository(_context);
			ShipmentDetailCartonRepository = new ShipmentDetailCartonRepository(_context);
			ShipmentModeRepository = new ShipmentModeRepository(_context);
			UserRepository = new UserRepository(_context);
			UserStatusRepository = new UserStatusRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}