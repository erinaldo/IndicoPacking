using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class CartonRepository : Repository<CartonBo>, ICartonRepository
    {
        public CartonRepository(IDbContext context) : base(context)
        {
            TableName = "Carton";
        }
    }
}
