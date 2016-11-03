using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class ShipmentDetailCartonRepository : Repository<ShipmentDetailCartonBo>, IShipmentDetailCartonRepository
    {
        public ShipmentDetailCartonRepository(IDbContext context) : base(context)
        {
            TableName = "ShipmentDetailCarton";
        }
    }
}
