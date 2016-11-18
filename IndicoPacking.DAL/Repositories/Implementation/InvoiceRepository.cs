/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class InvoiceRepository : Repository<InvoiceBo>, IInvoiceRepository
    {
		#region Internal constructors

        internal InvoiceRepository(IDbContext context) : base(context)
        {
            TableName = "[dbo].[Invoice]";
			PrimaryKeyName = "ID";
        }

		#endregion

		#region Public methods

	    

		#endregion
    }
}
