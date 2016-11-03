using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class DistributorClientAddressRepository : Repository<DistributorClientAddressBo>, IDistributorClientAddressRepository
    {
        public DistributorClientAddressRepository(IDbContext context) : base(context)
        {
            TableName = "DistributorClientAddress";
        }
    }
}
