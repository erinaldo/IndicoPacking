using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class OrderDetailsFromIndicoRepository : Repository<OrderDetailsFromIndicoBo>, IOrderDetailsFromIndicoRepository
    {
        public OrderDetailsFromIndicoRepository(IDbContext context) : base(context)
        {
            TableName = "OrderDetailsFromIndico";
        }
    }
}
