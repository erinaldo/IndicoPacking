using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class PortRepository : Repository<PortBo>, IPortRepository
    {
        public PortRepository(IDbContext context) : base(context)
        {
            TableName = "Port";
        }
    }
}
