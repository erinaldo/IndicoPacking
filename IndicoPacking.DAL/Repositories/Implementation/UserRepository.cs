using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class UserRepository : Repository<UserBo>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
            TableName = "User";
        }
    }
}
