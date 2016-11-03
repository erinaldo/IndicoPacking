using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class CountryRepository : Repository<CountryBo>, ICountryRepository
    {
        public CountryRepository(IDbContext context) : base(context)
        {
            TableName = "Country";
        }
    }
}
