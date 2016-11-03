using System;

namespace IndicoPacking.DAL.Objects.SPs
{
    public class GetItemsOfInvoiceForMyObBo
    {
        #region Properties

		
		public string VisualLayout { get; set; }
		public Int32 Quantity { get; set; }
		public Decimal Price { get; set; }
		public Decimal Total { get; set; }
		public string JobName { get; set; }

        #endregion
    }
}