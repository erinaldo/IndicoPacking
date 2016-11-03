using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class ShipmentDetailRepository : Repository<ShipmentDetailBo>, IShipmentDetailRepository
    {
        public ShipmentDetailRepository(IDbContext context) : base(context)
        {
            TableName = "ShipmentDetail";
        }
    }
}
