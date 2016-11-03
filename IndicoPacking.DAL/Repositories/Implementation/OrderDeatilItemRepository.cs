using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class OrderDeatilItemRepository : Repository<OrderDeatilItemBo>, IOrderDeatilItemRepository
    {
        public OrderDeatilItemRepository(IDbContext context) : base(context)
        {
            TableName = "OrderDeatilItem";
        }
    }
}
