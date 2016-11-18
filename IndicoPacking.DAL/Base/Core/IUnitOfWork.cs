/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;
using IndicoPacking.DAL.Repositories.Implementation;

namespace IndicoPacking.DAL.Base.Core
{
    public interface IUnitOfWork : IDisposable
    {
		#region Repositories

		
		BankRepository BankRepository { get; }
		CartonRepository CartonRepository { get; }
		CountryRepository CountryRepository { get; }
		DistributorClientAddressRepository DistributorClientAddressRepository { get; }
		InvoiceRepository InvoiceRepository { get; }
		InvoiceStatusRepository InvoiceStatusRepository { get; }
		OrderDeatilItemRepository OrderDeatilItemRepository { get; }
		OrderDetailsFromIndicoRepository OrderDetailsFromIndicoRepository { get; }
		PortRepository PortRepository { get; }
		RoleRepository RoleRepository { get; }
		ShipmentRepository ShipmentRepository { get; }
		ShipmentDetailRepository ShipmentDetailRepository { get; }
		ShipmentDetailCartonRepository ShipmentDetailCartonRepository { get; }
		ShipmentModeRepository ShipmentModeRepository { get; }
		UserRepository UserRepository { get; }
		UserStatusRepository UserStatusRepository { get; }

		#endregion

		#region Methods

        void Complete();

		#endregion
    }
}
