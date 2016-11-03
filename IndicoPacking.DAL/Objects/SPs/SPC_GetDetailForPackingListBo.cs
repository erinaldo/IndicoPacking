using System;

namespace IndicoPacking.DAL.Objects.SPs
{
    public class SPC_GetDetailForPackingListBo
    {
        #region Properties

		
		public string Heading { get; set; }
		public DateTime DeliveryDate { get; set; }
		public string InvoiceNumber { get; set; }
		public Int32 ShipmentDetailCarton { get; set; }
		public Int32 ID { get; set; }
		public Int32 Number { get; set; }
		public string Client { get; set; }
		public string PurchaseOrder { get; set; }
		public string VisualLayout { get; set; }
		public string Pattern { get; set; }
		public string Distributor { get; set; }
		public string PurchaseOrderNo { get; set; }
		public string OrderType { get; set; }

        #endregion
    }
}