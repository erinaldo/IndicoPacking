
using System;

namespace IndicoPacking.CustomModels
{
    public class ShipmentDetailCartonModel
    {
        public int ShipmentDeatil { get; set; }
        public int IndicoOrderID { get; set; }
        public int IndicoOrderDetailID { get; set; }
        public string Heading { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ShipmentDetailCarton { get; set; }
        public int ID { get; set; }
        public int CartonNumber { get; set; }
        public string Client { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public string QtyString { get; set; }
        public string Pattern { get; set; }
        public string Distributor { get; set; }
        public string CustPO { get; set; }
        public string OrderType { get; set; }
    }
}
