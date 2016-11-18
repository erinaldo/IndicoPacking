/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;

namespace IndicoPacking.DAL.Objects.SPs
{
    public class SPC_GetPackingListDetailsBo
    {
        #region Properties

		
		public Int32 ShipmentDeatil { get; set; }
		public Int32 IndicoOrderID { get; set; }
		public Int32 IndicoOrderDetailID { get; set; }
		public string Heading { get; set; }
		public DateTime DeliveryDate { get; set; }
		public string InvoiceNumber { get; set; }
		public Int32 ShipmentDetailCarton { get; set; }
		public Int32 ID { get; set; }
		public Int32 CartonNumber { get; set; }
		public string Client { get; set; }
		public string PurchaseOrderNumber { get; set; }
		public string Product { get; set; }
		public Int32 Quantity { get; set; }
		public string QtyString { get; set; }
		public string Pattern { get; set; }
		public string Distributor { get; set; }
		public string CustPO { get; set; }
		public string OrderType { get; set; }

        #endregion
    }
}