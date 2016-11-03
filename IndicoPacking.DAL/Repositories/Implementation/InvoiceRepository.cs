using IndicoPacking.DAL.Base.Core;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.DAL.Repositories.Core;

namespace IndicoPacking.DAL.Repositories.Implementation
{
    public class InvoiceRepository : Repository<InvoiceBo>, IInvoiceRepository
    {
        public InvoiceRepository(IDbContext context) : base(context)
        {
            TableName = "Invoice";
        }
    }
}
