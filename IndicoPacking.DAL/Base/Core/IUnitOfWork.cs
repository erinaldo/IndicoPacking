using System;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Base.Core
{
    public interface IUnitOfWork : IDisposable
    {
		
		IBankRepository BankRepository { get; }
		ICartonRepository CartonRepository { get; }
		ICountryRepository CountryRepository { get; }
		IDistributorClientAddressRepository DistributorClientAddressRepository { get; }
		IInvoiceRepository InvoiceRepository { get; }
		IInvoiceStatusRepository InvoiceStatusRepository { get; }
		IOrderDeatilItemRepository OrderDeatilItemRepository { get; }
		IOrderDetailsFromIndicoRepository OrderDetailsFromIndicoRepository { get; }
		IPortRepository PortRepository { get; }
		IRoleRepository RoleRepository { get; }
		IShipmentRepository ShipmentRepository { get; }
		IShipmentDetailRepository ShipmentDetailRepository { get; }
		IShipmentDetailCartonRepository ShipmentDetailCartonRepository { get; }
		IShipmentModeRepository ShipmentModeRepository { get; }
		IUserRepository UserRepository { get; }
		IUserStatusRepository UserStatusRepository { get; }

        void Complete();
    }
}
