using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class UserStatusRepository : Repository<UserStatusBo>, IUserStatusRepository
    {
        public UserStatusRepository(IDbContext context) : base(context)
        {
            TableName = "UserStatus";
        }
    }
}
