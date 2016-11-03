using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class InvoiceStatusRepository : Repository<InvoiceStatusBo>, IInvoiceStatusRepository
    {
        public InvoiceStatusRepository(IDbContext context) : base(context)
        {
            TableName = "InvoiceStatus";
        }
    }
}
