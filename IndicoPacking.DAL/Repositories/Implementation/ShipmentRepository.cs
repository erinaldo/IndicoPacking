using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class ShipmentRepository : Repository<ShipmentBo>, IShipmentRepository
    {
        public ShipmentRepository(IDbContext context) : base(context)
        {
            TableName = "Shipment";
        }
    }
}
