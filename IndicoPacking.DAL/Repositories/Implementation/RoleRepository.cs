using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class RoleRepository : Repository<RoleBo>, IRoleRepository
    {
        public RoleRepository(IDbContext context) : base(context)
        {
            TableName = "Role";
        }
    }
}
