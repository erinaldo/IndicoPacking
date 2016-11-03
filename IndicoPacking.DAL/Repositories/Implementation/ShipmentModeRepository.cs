using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class ShipmentModeRepository : Repository<ShipmentModeBo>, IShipmentModeRepository
    {
        public ShipmentModeRepository(IDbContext context) : base(context)
        {
            TableName = "ShipmentMode";
        }
    }
}
