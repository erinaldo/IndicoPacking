using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class BankRepository : Repository<BankBo>, IBankRepository
    {
        public BankRepository(IDbContext context) : base(context)
        {
            TableName = "Bank";
        }
    }
}
