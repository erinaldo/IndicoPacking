/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class DistributorClientAddressRepository : Repository<DistributorClientAddressBo>, IDistributorClientAddressRepository
    {
		#region Internal constructors

        internal DistributorClientAddressRepository(IDbContext context) : base(context)
        {
            TableName = "[dbo].[DistributorClientAddress]";
			PrimaryKeyName = "ID";
        }

		#endregion

		#region Public methods

	    

		#endregion
    }
}